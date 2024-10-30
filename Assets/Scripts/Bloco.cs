using System;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    public Action<Bloco> blocoDestruido;

    private void Start()
    {
        GerenciadorDeJogo.mudouDeEstado += MudouEstado;
    }

    public void DefineCor(Color cor)
    {
        sprite.color = cor;
    }

    public void Destroi()
    {
        GerenciadorDeEfeitos.instancia.InstanciaEfeito(GerenciadorDeEfeitos.Efeitos.BlcoDestroi, transform.position, Quaternion.identity);
        blocoDestruido?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        blocoDestruido = null;
    }

    void MudouEstado(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        if(novoEstado == GerenciadorDeJogo.EstadosDeJogo.Derrota ||
            novoEstado == GerenciadorDeJogo.EstadosDeJogo.Vitoria)
        {
            blocoDestruido = null;
            GerenciadorDeJogo.mudouDeEstado -= MudouEstado;
        }
    }
}
