using System;
using System.Collections.Generic;
using UnityEngine;

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

    public static void AtualizaEstado(EstadosDeJogo novoEstado)
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
                break;
            default:
                break;
        }
        mudouDeEstado?.Invoke(estadoAtual);
    }
    
}
