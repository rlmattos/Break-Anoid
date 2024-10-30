using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDeJogo : MonoBehaviour
{
    public static Action<EstadosDeJogo> mudouDeEstado;
    public enum EstadosDeJogo
    {
        Aguardando,
        EmJogo,
        Derrota,
        Vitoria
    }
    public static EstadosDeJogo estadoAtual
    { 
        get; private set;
    }
    [SerializeField] EstadosDeJogo estadoAtual_readOnly;

    private void Awake()
    {
        AtualizaEstado(EstadosDeJogo.Aguardando, true);
    }

    public static void AtualizaEstado(EstadosDeJogo novoEstado, bool atualizacaoForcada = false)
    {
        switch (estadoAtual)
        {
            case EstadosDeJogo.Aguardando:
                estadoAtual = novoEstado;
                break;
            case EstadosDeJogo.EmJogo:
                estadoAtual = novoEstado;
                break;
            case EstadosDeJogo.Derrota:
                if(atualizacaoForcada)
                    estadoAtual = novoEstado;
                break;
            default:
                break;
        }
        Debug.Log($"Novo estado: {estadoAtual}");
        mudouDeEstado?.Invoke(estadoAtual);
    }

    private void Update()
    {
        estadoAtual_readOnly = estadoAtual;
    }

    public static void ResetaJogo()
    {
        AtualizaEstado(EstadosDeJogo.Aguardando, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
