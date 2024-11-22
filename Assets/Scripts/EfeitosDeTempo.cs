using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosDeTempo : MonoBehaviour
{
    static bool executandoEfeito;
    private void OnEnable()
    {
        GerenciadorDeJogo.mudouDeEstado += AoMudarDeEstado;
    }

    private void OnDisable()
    {
        GerenciadorDeJogo.mudouDeEstado -= AoMudarDeEstado;
    }

    void AoMudarDeEstado(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        switch (novoEstado)
        {
            case GerenciadorDeJogo.EstadosDeJogo.Vitoria:
                StartCoroutine(SlowMotion(2.5f, 0.075f));
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Derrota:
                StartCoroutine(SlowMotion(2, 0.1f));
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
}
