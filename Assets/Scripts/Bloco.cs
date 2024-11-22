using System;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites;
    public Action<Bloco> blocoDestruido;

    private void Start()
    {
        GerenciadorDeJogo.mudouDeEstado += MudouEstado;
    }

    public void DefineCor(Color cor)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            cor.a = sprites[i].color.a;
            sprites[i].color = cor;
        }
    }

    public void Destroi()
    {
        GerenciadorDeEfeitos.instancia.InstanciaEfeito(GerenciadorDeEfeitos.Efeitos.BlocoDestroi, transform.position, Quaternion.identity);
        blocoDestruido?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        blocoDestruido = null;
    }

    void MudouEstado(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        if (novoEstado == GerenciadorDeJogo.EstadosDeJogo.Derrota ||
            novoEstado == GerenciadorDeJogo.EstadosDeJogo.Vitoria)
        {
            blocoDestruido = null;
            GerenciadorDeJogo.mudouDeEstado -= MudouEstado;
        }
    }
}
