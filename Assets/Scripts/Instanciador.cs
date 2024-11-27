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
    int blocosTotais;
    WaitForSeconds esperaEntreInstancias;
    /// <summary>
    /// Informa o progresso atual do jogo. Quanto mais blocos destruidos, mais perto de 1;
    /// </summary>
    public static Action<float> blocoDestruido;

    IEnumerator Start()
    {
        Transform tr = transform;
        numLinhas = linhas.Length;
        blocosTotais = numLinhas * colunas;
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
                    1 + Mathf.Lerp(0, 1.0f, (((linhaAtual * colunas) + colunaAtual) /(float) blocosTotais)));
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
        {
            Debug.Log($"progresso: {1-(blocosAtuais / (float)blocosTotais)}");
            blocoDestruido?.Invoke(1-(blocosAtuais / (float)blocosTotais));
        }
    }
}
