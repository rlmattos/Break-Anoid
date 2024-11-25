using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolinha : MonoBehaviour
{
    Transform tr;
    [SerializeField] float velocidade = 5;
    [SerializeField] float raio = 0.25f;
    [SerializeField] LayerMask layersParaColidir;
    Vector2 velAtual;
    Rigidbody2D rb;
    Vector2 temposDosRepatimentos;
    [SerializeField] float intervaloDeRebatimento = 0.1f;
    Vector3 posOriginal;
    [SerializeField] Transform trPalheta;
    [SerializeField] float distanciaDaChecagem = 0.1f;
    [SerializeField] float intervaloEntreQuebras = 3;
    float contadorIntervaloEntreQuebras = 0;
    int blocosQuebradosEmSequencia;
    [SerializeField] CinemachineImpulseSource cameraShake;
    [SerializeField] Animator anim;

    public static Action perdeuVida;

    private void OnEnable()
    {
        Vida.gameOver += GameOver;
        GerenciadorDeJogo.mudouDeEstado += MudouDeEstado;
        MenuInGame.pausouOuDespausou += AoPausarOuDespausar;
    }

    private void OnDisable()
    {
        Vida.gameOver -= GameOver;
        GerenciadorDeJogo.mudouDeEstado -= MudouDeEstado;
        MenuInGame.pausouOuDespausou -= AoPausarOuDespausar;
    }

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        Reseta(false); 
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Intro);
    }

    private void Dispara()
    {
        tr.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.simulated = true;
        velAtual = (Vector2.up + Vector2.right).normalized * velocidade;
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.EmJogo);
    }

    private void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
            && GerenciadorDeJogo.estadoAtual == GerenciadorDeJogo.EstadosDeJogo.Aguardando)
            Dispara();

        if (contadorIntervaloEntreQuebras > 0)
            contadorIntervaloEntreQuebras -= Time.deltaTime;
        else
            blocosQuebradosEmSequencia = 0;
        
    }

    void AoPausarOuDespausar(bool pausou)
    {
        if (pausou)
            rb.bodyType = RigidbodyType2D.Kinematic;
        else
            rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void FixedUpdate()
    {
        if (GerenciadorDeJogo.estadoAtual != GerenciadorDeJogo.EstadosDeJogo.EmJogo)
        {
            return;
        }

        if (rb.position.y < -1)
        {
            GerenciadorDeEfeitos.instancia.InstanciaEfeito(GerenciadorDeEfeitos.Efeitos.BolinhaDestroi, tr.position, Quaternion.identity);
            GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.BolinhaPerde, 1, 1);
            GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.PerdeuBolinha);
            anim.Play("Oculta");
            Invoke("Reseta", 2);
            perdeuVida?.Invoke();
        }
        else
        {
            rb.velocity = velAtual;
            CalculaColisao();
        }

        tr.up = velAtual;
    }
    void Reseta()
    {
        Reseta(true);
    }
    void Reseta(bool comAnimacao = true)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;
        velAtual = Vector3.zero;
        tr.position = trPalheta.position;
        tr.rotation = Quaternion.identity;
        tr.SetParent(trPalheta);
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Aguardando);
        if(comAnimacao)
            anim.Play("Respawn");        
    }

    private void CalculaColisao()
    {
        Debug.DrawLine(rb.position, rb.position + velAtual.normalized * distanciaDaChecagem, Color.blue, 0.1f);
        RaycastHit2D[] contatos = Physics2D.CircleCastAll(rb.position, raio, velAtual.normalized, distanciaDaChecagem, layersParaColidir);
        if (contatos.Length == 0)
            return;
        Vector2 pontoColisao = contatos[0].point;
        Debug.DrawLine(rb.position, pontoColisao, Color.red, 0.1f);
        Vector2 distanciaColisao = pontoColisao - rb.position;
        distanciaColisao.x = Mathf.Abs(distanciaColisao.x);
        distanciaColisao.y = Mathf.Abs(distanciaColisao.y);
        if (distanciaColisao.x > distanciaColisao.y)
        {
            if (Time.time - temposDosRepatimentos.x > intervaloDeRebatimento)
                Rebate(true, pontoColisao, contatos[0].normal);
        }
        else
        {
            if (Time.time - temposDosRepatimentos.y > intervaloDeRebatimento)
                Rebate(false, pontoColisao, contatos[0].normal);
        }

        Bloco bloco = contatos[0].collider.gameObject.GetComponentInParent<Bloco>();
        if (bloco)
        {
            contadorIntervaloEntreQuebras = intervaloEntreQuebras;
            blocosQuebradosEmSequencia++;
            GerenciadorDeEfeitos.instancia.InstanciaEfeito(GerenciadorDeEfeitos.Efeitos.BlocoHit, pontoColisao, Quaternion.identity);
            GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.BlocoHit,1, 0.9f + (0.12f * blocosQuebradosEmSequencia));
            bloco.Destroi();
        }else
        {
            if (contatos[0].transform.name.Contains("Parede"))
                GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.ParedeHit, 1, 0.8f);
            else
            {
                Palheta palheta = contatos[0].collider.gameObject.GetComponentInParent<Palheta>();
                if(palheta)
                    palheta.Rebate();
                GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.PalhetaHit, 1, 1.1f);
            }
        }
    }

    void Rebate(bool horizontal, Vector3 pontoDeColisao, Vector3 normalDaColisao)
    {
        if (horizontal)
        {
            velAtual.x *= -1;
            temposDosRepatimentos.x = Time.time;
            cameraShake.GenerateImpulseAtPositionWithVelocity(rb.position, (Vector3.right * -velAtual.x).normalized * 0.05f);
        }
        else
        {
            velAtual.y *= -1;
            temposDosRepatimentos.y = Time.time;
            cameraShake.GenerateImpulseAtPositionWithVelocity(rb.position, (Vector3.up * -velAtual.y).normalized * 0.05f);
        }
        anim.Play("Hit");
        GerenciadorDeEfeitos.instancia.InstanciaEfeito(
            GerenciadorDeEfeitos.Efeitos.BolinhaHit,
            pontoDeColisao,
            Quaternion.LookRotation(normalDaColisao));
    }

    void GameOver()
    {
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Derrota);
        rb.bodyType = RigidbodyType2D.Static;
        rb.simulated = false;
        tr.position = Vector3.up * -100;
    }

    void MudouDeEstado(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        switch (novoEstado)
        {
            case GerenciadorDeJogo.EstadosDeJogo.Derrota:
            case GerenciadorDeJogo.EstadosDeJogo.Vitoria:
            case GerenciadorDeJogo.EstadosDeJogo.PerdeuBolinha:
                anim.Play("Oculta");
                break;
        }
    }
}
