using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] GameObject[] sprites;
    [SerializeField] int vidaMaxima = 3;
    int vidaAtual;

    public static Action gameOver;

    private void OnEnable()
    {
        Bolinha.perdeuVida += PerdeVida;
    }

    private void OnDisable()
    {
        Bolinha.perdeuVida -= PerdeVida;
    }

    void Start()
    {
        AtualizaVida(vidaMaxima, true);
    }

    public void AtualizaVida(int novaVida, bool atualizacaoForcada = false)
    {
        if (vidaAtual == 0 && !atualizacaoForcada)
        {
            gameOver?.Invoke();
            return;
        }
        vidaAtual = novaVida;
        for (int i = 0; i < vidaMaxima; i++)
        {
            sprites[i].gameObject.SetActive(vidaAtual > i);
        }

    }

    void PerdeVida()
    {
        AtualizaVida(vidaAtual - 1);
    }
}
