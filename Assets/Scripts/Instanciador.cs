using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    [SerializeField] Bloco prefab;
    [SerializeField] int colunas = 5;
    [SerializeField] Vector2 espacamento = Vector2.one;
    [SerializeField] Color[] cores;
    [TextArea(6, 7)]
    [SerializeField] string[] padroes = new string[]
    {
        "_\n" +
        "_\n" +
        "_\n" +
        "_",

        "_ \n" +
        " _\n" +
        "_ \n" +
        " _",

        "_\n" +
        " \n" +
        "_\n" +
        " \n" +
        "_",

        "_ _\n" +
        "__  \n" +
        "_ _\n" +
        "__\n" +
        "_ _",

        "_\n" +
        "_  _\n" +
        "__ \n" +
        "_ _ \n" +
        "_",

        "_\n" +
        "_\n" +
        "_\n" +
        "_\n" +
        "_\n" +
        "_"

    };
    string[] linhas;
    int linhaAtual = 0;
    int colunaAtual = 0;
    [SerializeField] int padraoAtual = 0;
    int numLinhas;
    int blocosAtuais;
    int blocosTotais;
    WaitForSecondsRealtime esperaEntreInstancias;
    /// <summary>
    /// Informa o progresso atual do jogo. Quanto mais blocos destruidos, mais perto de 1;
    /// </summary>
    public static Action<float> blocoDestruido;
    public static Action terminouDeInstanciar;

    void Start()
    {
        StartCoroutine(InstanciaBlocos());
    }

    IEnumerator InstanciaBlocos()
    {
        Transform tr = transform;
        linhas = padroes[padraoAtual].Split("\n");
        numLinhas = linhas.Length;
        esperaEntreInstancias = new WaitForSecondsRealtime(0.05f);
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0;
        linhaAtual = 0;
        for (; linhaAtual < numLinhas; linhaAtual++)
        {
            colunaAtual = 0;
            for (; colunaAtual < colunas; colunaAtual++)
            {
                int indiceDoCaractereAtual = (int)Mathf.Repeat(colunaAtual, linhas[linhaAtual].Length);
                if (linhas[linhaAtual].Length < indiceDoCaractereAtual)
                    continue;
                char caractereAtual = linhas[linhaAtual][indiceDoCaractereAtual];
                if (!caractereAtual.Equals('_'))
                    continue;
                Bloco blocoAtual = Instantiate(prefab, tr.position + (Vector3.right * colunaAtual * espacamento.x) + (-Vector3.up * linhaAtual * espacamento.y), Quaternion.identity, tr);
                GerenciadorDeSFX.instancia.TocaSFX(
                    GerenciadorDeSFX.Efeitos.Bloco_Aparece,
                    1,
                    1 + Mathf.Lerp(0, 1.0f, (((linhaAtual * colunas) + colunaAtual) / (float)blocosTotais)));
                blocoAtual.DefineCor(cores[linhaAtual]);
                blocoAtual.blocoDestruido += BlocoDestruido;
                blocosAtuais++;
                yield return esperaEntreInstancias;
            }
        }
        blocosTotais = blocosAtuais;
        if(padraoAtual == 0)
            terminouDeInstanciar?.Invoke();
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
    }

    void BlocoDestruido(Bloco bloco)
    {
        blocosAtuais--;
        bloco.blocoDestruido -= BlocoDestruido;
        if (blocosAtuais <= 0)
        {
            padraoAtual++;
            if (padraoAtual >= padroes.Length)
                GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Vitoria);
            else
                StartCoroutine(InstanciaBlocos());
        }
        else
        {
            Debug.Log($"progresso: {1 - (blocosAtuais / (float)blocosTotais)}");
            blocoDestruido?.Invoke(1 - (blocosAtuais / (float)blocosTotais));
        }
    }
}
