using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GerenciadorDeBGM : MonoBehaviour
{
    static GerenciadorDeBGM instancia;
    [SerializeField] AudioMixerSnapshot inGame;
    [SerializeField] AudioMixerSnapshot pause;
    [SerializeField] AudioMixerSnapshot menu;
    [SerializeField] AudioMixerSnapshot vitoria;
    [SerializeField] AudioMixerSnapshot derrota;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        GerenciadorDeJogo.mudouDeEstado += AoMudarDeEstado;
        FadeDeTela.mudouDeCena += AoMudarDeCena;
        MenuInGame.pausouOuDespausou += AoPausarOuDespausar;
    }

    private void OnDisable()
    {
        GerenciadorDeJogo.mudouDeEstado -= AoMudarDeEstado;
        FadeDeTela.mudouDeCena -= AoMudarDeCena;
        MenuInGame.pausouOuDespausou -= AoPausarOuDespausar;
    }

    void AoMudarDeCena(string nomeDaCena)
    {
        if (nomeDaCena.Contains("Jogo"))
            inGame.TransitionTo(1);
        else if (nomeDaCena.Contains("MenuInicial") || nomeDaCena.Contains("SplashScreen"))
            menu.TransitionTo(1);
    }

    void AoPausarOuDespausar(bool pausou)
    {
        if (pausou)
            pause.TransitionTo(1f);
        else
            inGame.TransitionTo(1f);
    }

    void AoMudarDeEstado(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        switch (novoEstado)
        {
            case GerenciadorDeJogo.EstadosDeJogo.Aguardando:
                break;
            case GerenciadorDeJogo.EstadosDeJogo.EmJogo:
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Derrota:
                derrota.TransitionTo(1.0f);
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Vitoria:
                vitoria.TransitionTo(1.0f);
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Intro:
                inGame.TransitionTo(1.0f);
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Pause:
                break;
            case GerenciadorDeJogo.EstadosDeJogo.PerdeuBolinha:
                break;
            default:
                break;
        }
    }
}
