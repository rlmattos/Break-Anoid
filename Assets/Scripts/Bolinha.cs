using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolinha : MonoBehaviour
{
    [SerializeField] float velocidade = 5;
    Vector2 velAtual;
    Rigidbody2D rb;
    Vector2 temposDosRepatimentos;
    float intervaloDeRebatimento = 0.1f;
    Vector3 posOriginal;
    [SerializeField] Transform trPalheta;

    public static Action perdeuVida;


    private void OnEnable()
    {
        Vida.gameOver += GameOver;
    }

    private void OnDisable()
    {
        Vida.gameOver -= GameOver;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Reseta();
    }

    private void Dispara()
    {
        transform.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.simulated = true;
        velAtual = (Vector2.up + Vector2.right).normalized * velocidade;
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.EmJogo);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && GerenciadorDeJogo.estadoAtual == GerenciadorDeJogo.EstadosDeJogo.Aguardando)
            Dispara();        
    }

    private void FixedUpdate()
    {
        if (GerenciadorDeJogo.estadoAtual != GerenciadorDeJogo.EstadosDeJogo.EmJogo)
        {
            rb.bodyType = RigidbodyType2D.Static;
            return;
        }

        rb.velocity = velAtual;
        if (rb.position.y < 0)
        {
            GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Aguardando);
            Reseta();
            perdeuVida?.Invoke();
        }
    }

    void Reseta()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;
        velAtual = Vector3.zero;
        transform.position = trPalheta.position + Vector3.up * 0.75f;
        transform.SetParent(trPalheta);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 pontoColisao = collision.contacts[0].point;
        Vector2 distanciaColisao = pontoColisao - rb.position;
        distanciaColisao.x = Mathf.Abs(distanciaColisao.x);
        distanciaColisao.y = Mathf.Abs(distanciaColisao.y);
        if (distanciaColisao.x > distanciaColisao.y)
        {
            if (Time.time - temposDosRepatimentos.x > intervaloDeRebatimento)
                Rebate(true);
        }
        else
        {
            if (Time.time - temposDosRepatimentos.y > intervaloDeRebatimento)
                Rebate(false);
        }

        Bloco bloco = collision.gameObject.GetComponentInParent<Bloco>();
        if (bloco)
            bloco.Destroi();
    }

    void Rebate(bool horizontal)
    {
        if (horizontal)
        {
            velAtual.x *= -1;
            temposDosRepatimentos.x = Time.time;
        }
        else
        {
            velAtual.y *= -1;
            temposDosRepatimentos.y = Time.time;
        }
    }

    void GameOver()
    {
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Derrota);
        rb.bodyType = RigidbodyType2D.Static;
        rb.simulated = false;
        transform.position = Vector3.up * -100;
    }
}
