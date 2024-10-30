using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciaDeEfeito : MonoBehaviour
{
    [SerializeField] GerenciadorDeJogo.EstadosDeJogo estadosValidos;
    [SerializeField] GameObject componente;

    private void OnEnable()
    {
        GerenciadorDeJogo.mudouDeEstado += AtualizaEfeito;
    }

    private void OnDisable()
    {
        GerenciadorDeJogo.mudouDeEstado -= AtualizaEfeito;
    }

    void AtualizaEfeito(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        bool liga = estadosValidos.HasFlag(novoEstado);
        MudaEstadoDoEfeito(liga);
    }

    void MudaEstadoDoEfeito(bool ligado)
    {
        componente.SetActive(ligado);
    }
}
