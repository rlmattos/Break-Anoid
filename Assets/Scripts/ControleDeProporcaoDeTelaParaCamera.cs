using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeProporcaoDeTelaParaCamera : MonoBehaviour
{
    [SerializeField] bool _16x9;
    [SerializeField] bool _16x10;
    [SerializeField] bool _21x9;
    [SerializeField] bool _32x9;

    List<Resolution> resolucoesDoJogo = new List<Resolution>();
    void Start()
    {
        Resolution resolucaoAtual = Screen.currentResolution;
        Resolution[] resolucoesSuportadas = Screen.resolutions;
        Debug.Log($"{resolucoesSuportadas.Length} resolucoesSuportadas encontradas");
        float proporcaoDeTelaAtual = 0;

        for (int i = 0; i < resolucoesSuportadas.Length; i++)
        {
            resolucaoAtual = resolucoesSuportadas[i];
            proporcaoDeTelaAtual = resolucaoAtual.width / (float)resolucaoAtual.height;
            if (_16x10 && (proporcaoDeTelaAtual == 16 / 10f) ||
                _16x9 && (proporcaoDeTelaAtual == 16 / 9f) ||
                _21x9 && (proporcaoDeTelaAtual == 21 / 9f) ||
                _32x9 && (proporcaoDeTelaAtual == 32 / 9f))
            {
                Debug.Log($"Adicionando resolução {resolucaoAtual.width} x {resolucaoAtual.height}");
                resolucoesDoJogo.Add(resolucaoAtual);
            }
        }
        Resolution maiorResolucao = resolucoesDoJogo[resolucoesDoJogo.Count - 1];
        Debug.Log($"Definindo resolução para: {maiorResolucao.width} x {maiorResolucao.height}");
        Screen.SetResolution(maiorResolucao.width, maiorResolucao.height, Screen.fullScreen);
    }
}
