using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosDeTempo : MonoBehaviour
{
    static bool executandoEfeito;
    private void OnEnable()
    {
        GerenciadorDeJogo.mudouDeEstado += AoMudarDeEstado;
        Instanciador.blocoDestruido += AoDestruirBloco;
    }

    private void OnDisable()
    {
        GerenciadorDeJogo.mudouDeEstado -= AoMudarDeEstado;
        Instanciador.blocoDestruido -= AoDestruirBloco;
    }

    private void Start()
    {
        executandoEfeito = false;
    }

    void AoMudarDeEstado(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        switch (novoEstado)
        {
            case GerenciadorDeJogo.EstadosDeJogo.Vitoria:
                StartCoroutine(SlowMotion(2.5f, 0.075f));
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Derrota:
                StartCoroutine(SlowMotion(2.5f, 0.075f));
                break;
        }
    }

    IEnumerator SlowMotion(float duracao, float novaVelocidade)
    {
        if (!executandoEfeito)
        {
            executandoEfeito = true;
            Time.timeScale = novaVelocidade;
            yield return new WaitForSecondsRealtime(duracao);
            Time.timeScale = 1;
            executandoEfeito = false;
        }
    }

    void AoDestruirBloco(float progresso)
    {
        StartCoroutine(FrameFreeze(Mathf.Lerp(0.01f, 0.1f, progresso)));
    }

    IEnumerator FrameFreeze(float duracao)
    {
        if (!executandoEfeito)
        {
            executandoEfeito = true;
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(duracao);
            Time.timeScale = 1;
            executandoEfeito = false;
        }
    }
}
