using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDeJogo : MonoBehaviour
{
    public static Action<EstadosDeJogo> mudouDeEstado;
    [Flags] public enum EstadosDeJogo
    {
        Aguardando = 1,
        EmJogo = 2,
        Derrota = 4,
        Vitoria = 8,
        Intro = 16
    }
    public static EstadosDeJogo estadoAtual
    { 
        get; private set;
    }
    [SerializeField] EstadosDeJogo estadoAtual_readOnly;

    private void Awake()
    {
        AtualizaEstado(EstadosDeJogo.Intro, true);
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
            case EstadosDeJogo.Vitoria:
                estadoAtual = novoEstado;
                break;
            case EstadosDeJogo.Derrota:
                if(atualizacaoForcada)
                    estadoAtual = novoEstado;
                break;
            default:
                estadoAtual = novoEstado;
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
