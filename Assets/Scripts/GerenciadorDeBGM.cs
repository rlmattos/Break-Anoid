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
        FadeDeTela.mudouDeCena += AoMudarDeCena;
        MenuInGame.pausouOuDespausou += AoPausarOuDespausar;
    }

    private void OnDisable()
    {
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
}
