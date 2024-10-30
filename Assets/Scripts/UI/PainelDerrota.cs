using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainelDerrota : MonoBehaviour
{
    GameObject painel;
    private void Start()
    {
        painel = transform.GetChild(0).gameObject;
        painel.SetActive(false);
    }
    private void OnEnable()
    {
        GerenciadorDeJogo.mudouDeEstado += MudouEstadoDeJogo;
    }

    private void OnDisable()
    {
        GerenciadorDeJogo.mudouDeEstado -= MudouEstadoDeJogo;
    }

    void MudouEstadoDeJogo(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        if (novoEstado == GerenciadorDeJogo.EstadosDeJogo.Derrota)
            painel.SetActive(true);
    }
}