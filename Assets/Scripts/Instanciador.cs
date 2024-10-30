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

    void Start()
    {
        Transform tr = transform;
        numLinhas = linhas.Length;
        for (int linhaAtual = 0; linhaAtual < numLinhas; linhaAtual++)
        {
            for (int colunaAtual = 0; colunaAtual < colunas; colunaAtual++)
            {
                Bloco blocoAtual = Instantiate(prefab, tr.position + (Vector3.right * colunaAtual * espacamento.x) + (-Vector3.up * linhaAtual * espacamento.y), Quaternion.identity, tr);
                blocoAtual.DefineCor(linhas[linhaAtual]);
                blocoAtual.blocoDestruido += BlocoDestruido;
                blocosAtuais++;
            }
        }
    }

    void BlocoDestruido(Bloco bloco)
    {
        blocosAtuais--;
        bloco.blocoDestruido -= BlocoDestruido;
        if (blocosAtuais <= 0)
            GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Vitoria);
    }
}
