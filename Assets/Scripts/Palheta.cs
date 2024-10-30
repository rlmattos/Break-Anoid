using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palheta : MonoBehaviour
{
    [SerializeField] float velMovimento;
    float inputHorizontal;
    Vector2 velAtual;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GerenciadorDeJogo.estadoAtual.HasFlag(GerenciadorDeJogo.EstadosDeJogo.EmJogo) &&
            !GerenciadorDeJogo.estadoAtual.HasFlag(GerenciadorDeJogo.EstadosDeJogo.Aguardando))
        {
            inputHorizontal = 0;
            return;
        }

        LeInput();
    }

    private void LeInput()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        AplicaMovimento();
    }

    private void AplicaMovimento()
    {
        velAtual.x = inputHorizontal * velMovimento;
        rb.velocity = velAtual;
    }

    public void TerminouAnimacao()
    {
        GetComponent<Animator>().enabled = false;
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Aguardando);
    }
}
