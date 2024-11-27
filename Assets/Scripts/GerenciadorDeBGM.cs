using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class GerenciadorDeBGM : MonoBehaviour
{
    static GerenciadorDeBGM instancia;
    float pitchAtual = 1;
    float pitchAnterior = 1;
    [SerializeField] AudioMixer mixer;
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
        Instanciador.blocoDestruido += AoDestruirBloco;
        StartCoroutine(AtualizaPitch());
    }

    private void OnDisable()
    {
        GerenciadorDeJogo.mudouDeEstado -= AoMudarDeEstado;
        FadeDeTela.mudouDeCena -= AoMudarDeCena;
        MenuInGame.pausouOuDespausou -= AoPausarOuDespausar;
        Instanciador.blocoDestruido += AoDestruirBloco;
    }

    void AoMudarDeCena(string nomeDaCena)
    {
        if (nomeDaCena.Contains("Jogo"))
            inGame.TransitionTo(1);
        else if (nomeDaCena.Contains("MenuInicial") || nomeDaCena.Contains("SplashScreen"))
            menu.TransitionTo(1);
        pitchAtual = 1;
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
                pitchAtual = 1;
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Vitoria:
                vitoria.TransitionTo(1.0f);
                pitchAtual = 1;
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Intro:
                inGame.TransitionTo(1.0f);
                pitchAtual = 1;
                break;
            case GerenciadorDeJogo.EstadosDeJogo.Pause:
                break;
            case GerenciadorDeJogo.EstadosDeJogo.PerdeuBolinha:
                break;
            default:
                break;
        }
    }

    void AoDestruirBloco(float progresso)
    {
        progresso = Mathf.FloorToInt(progresso * 10) * 0.1f;
        pitchAtual = 1 + Mathf.Lerp(0, 1.0f, progresso);
    }

    private IEnumerator AtualizaPitch()
    {
        while(true)
        {
            mixer.GetFloat("BGM_Pitch", out pitchAnterior);
            mixer.SetFloat("BGM_Pitch", Mathf.Lerp(pitchAnterior, pitchAtual, Time.deltaTime));
            yield return 0;
        }
    }
}
