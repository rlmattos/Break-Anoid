using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palheta : MonoBehaviour
{
    [SerializeField] float velMovimento;
    float inputHorizontal;
    Vector2 velAtual;
    public Vector2 VelAtual => velAtual;
    Rigidbody2D rb;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Instanciador.terminouDeInstanciar += AnimaIntro;
    }

    private void OnDisable()
    {
        Instanciador.terminouDeInstanciar -= AnimaIntro;
    }

    void Update()
    {
        if (!GerenciadorDeJogo.estadoAtual.HasFlag(GerenciadorDeJogo.EstadosDeJogo.EmJogo) &&
            !GerenciadorDeJogo.estadoAtual.HasFlag(GerenciadorDeJogo.EstadosDeJogo.Aguardando))
        {
            inputHorizontal = 0;
            GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.PalhetaMove, 0, 1f);
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
        if (velAtual.x > 0)
            GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.PalhetaMove, 1, 1.05f);
        else if (velAtual.x < 0)
            GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.PalhetaMove, 1, 1f);
        else
            GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.PalhetaMove, 0, 1f);
    }

    public void TerminouAnimacao()
    {
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Aguardando);
    }

    public void Rebate()
    {
        anim.Play("Rebate");
    }

    void AnimaIntro()
    {
        anim.Play("PalhetaIntro");
    }
}
