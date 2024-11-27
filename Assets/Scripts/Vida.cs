using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] Animator[] sprites;
    [SerializeField] int vidaMaxima = 3;
    public static int VidaMaxima;
    int vidaAtual;
    WaitForSeconds intervaloDeAtualizacao;

    public static Action gameOver;
    public static Action<float> atualizouVida;

    private void OnEnable()
    {
        Bolinha.perdeuVida += PerdeVida;
    }

    private void OnDisable()
    {
        Bolinha.perdeuVida -= PerdeVida;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2.5f);
        intervaloDeAtualizacao = new WaitForSeconds(0.15f);
        StartCoroutine(AtualizaVida(vidaMaxima, true));
        VidaMaxima = vidaMaxima;
    }

    public IEnumerator AtualizaVida(int novaVida, bool atualizacaoForcada = false)
    {
        if (!(vidaAtual == 0 && !atualizacaoForcada))
        {
            vidaAtual = novaVida;
            for (int i = 0; i < vidaMaxima; i++)
            {
                if (vidaAtual <= i)
                {
                    sprites[i].Play("Desaparece");
                    yield return intervaloDeAtualizacao;
                }
                else if (!sprites[i].GetCurrentAnimatorStateInfo(0).IsName("Aparece"))
                {
                    sprites[i].Play("Aparece");
                    yield return intervaloDeAtualizacao;
                }
            }
        }
        else
        {
            gameOver?.Invoke();
        }
        atualizouVida?.Invoke(vidaAtual);
    }

    void PerdeVida()
    {
        StartCoroutine(AtualizaVida(vidaAtual - 1));
    }
}
