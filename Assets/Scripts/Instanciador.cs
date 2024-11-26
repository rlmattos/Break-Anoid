using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    [SerializeField] Bloco prefab;
    [SerializeField] int colunas = 5;
    [SerializeField] Vector2 espacamento = Vector2.one;
    [SerializeField] Color[] linhas;
    int numLinhas;
    int blocosAtuais;
    WaitForSeconds esperaEntreInstancias;
    public static Action blocoDestruido;

    IEnumerator Start()
    {
        Transform tr = transform;
        numLinhas = linhas.Length;
        int numDeBlocos = numLinhas * colunas;
        esperaEntreInstancias = new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(0.5f);
        for (int linhaAtual = 0; linhaAtual < numLinhas; linhaAtual++)
        {
            for (int colunaAtual = 0; colunaAtual < colunas; colunaAtual++)
            {
                Bloco blocoAtual = Instantiate(prefab, tr.position + (Vector3.right * colunaAtual * espacamento.x) + (-Vector3.up * linhaAtual * espacamento.y), Quaternion.identity, tr);
                GerenciadorDeSFX.instancia.TocaSFX(
                    GerenciadorDeSFX.Efeitos.Bloco_Aparece,
                    1,
                    1 + Mathf.Lerp(0, 1.0f, (((linhaAtual * colunas) + colunaAtual) /(float) numDeBlocos)));
                blocoAtual.DefineCor(linhas[linhaAtual]);
                blocoAtual.blocoDestruido += BlocoDestruido;
                blocosAtuais++;
                yield return esperaEntreInstancias;
            }
        }
    }

    void BlocoDestruido(Bloco bloco)
    {
        blocosAtuais--;
        bloco.blocoDestruido -= BlocoDestruido;
        if (blocosAtuais <= 0)
            GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Vitoria);
        else
            blocoDestruido?.Invoke();
    }
}
