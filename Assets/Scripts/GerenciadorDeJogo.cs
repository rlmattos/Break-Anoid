using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDeJogo : MonoBehaviour
{
    public static Action<EstadosDeJogo> mudouDeEstado;
    [Flags]
    public enum EstadosDeJogo
    {
        Aguardando = 1,
        EmJogo = 2,
        Derrota = 4,
        Vitoria = 8,
        Intro = 16,
        Pause = 32
    }
    public static EstadosDeJogo estadoAtual
    {
        get; private set;
    }
    [SerializeField] EstadosDeJogo estadoAtual_readOnly;
    public static EstadosDeJogo estadoAnterior
    {
        get; private set;
    }
    [SerializeField] EstadosDeJogo estadoAnterior_readOnly;

    private void Awake()
    {
        AtualizaEstado(EstadosDeJogo.Intro, true);
    }

    public static void RetornaAoEstadoAnterior()
    {
        EstadosDeJogo estadoTemporario = estadoAtual;
        estadoAtual = estadoAnterior;
        estadoAnterior = estadoTemporario;
        mudouDeEstado?.Invoke(estadoAtual);
    }

    public static void AtualizaEstado(EstadosDeJogo novoEstado, bool atualizacaoForcada = false)
    {
        switch (estadoAtual)
        {
            case EstadosDeJogo.Aguardando:
                estadoAnterior = estadoAtual;
                estadoAtual = novoEstado;
                break;
            case EstadosDeJogo.EmJogo:
                estadoAnterior = estadoAtual;
                estadoAtual = novoEstado;
                break;
            case EstadosDeJogo.Vitoria:
                estadoAnterior = estadoAtual;
                estadoAtual = novoEstado;
                break;
            case EstadosDeJogo.Derrota:
                if (atualizacaoForcada)
                {
                    estadoAtual = novoEstado;
                    estadoAnterior = estadoAtual;
                }
                break;
            default:
                estadoAnterior = estadoAtual;
                estadoAtual = novoEstado;
                break;
        }
        Debug.Log($"Novo estado: {estadoAtual}");
        mudouDeEstado?.Invoke(estadoAtual);
    }

    private void Update()
    {
        estadoAtual_readOnly = estadoAtual;
        estadoAnterior_readOnly = estadoAnterior;
    }

    public static void ResetaJogo()
    {
        AtualizaEstado(EstadosDeJogo.Aguardando, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
