using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    // Start is called before the first frame update
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
                blocosAtuais++;
            }
        }
    }
}
