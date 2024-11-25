using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class MenuInGame : MonoBehaviour
{
    [SerializeField] Button botaoFechaOpcoes;
    [SerializeField] Button botaoMenuInicial;
    [SerializeField] Toggle toggleTelaCheia;
    [SerializeField] Slider sliderVolume;
    [SerializeField] CanvasGroup opcoesGroup;

    [SerializeField] AudioMixer mixer;
    [SerializeField] Animator animOpcoes;
    bool opcoesAbertas;

    public static Action<bool> pausouOuDespausou;

    private void OnEnable()
    {
        botaoFechaOpcoes.onClick.AddListener(ClicouOpcoes);
        botaoMenuInicial.onClick.AddListener(ClicouMenuInicial);
        toggleTelaCheia.onValueChanged.AddListener(ClicouTelaCheia);
        sliderVolume.onValueChanged.AddListener(AlterouVolume);
    }
    private void OnDisable()
    {
        botaoFechaOpcoes.onClick.RemoveListener(ClicouOpcoes);
        botaoMenuInicial.onClick.RemoveListener(ClicouMenuInicial);
        toggleTelaCheia.onValueChanged.RemoveListener(ClicouTelaCheia);
        sliderVolume.onValueChanged.RemoveListener(AlterouVolume);
    }
    private void Start()
    {
        animOpcoes.Play("Desaparece", 0, 1);
        opcoesAbertas = false;

        toggleTelaCheia.SetIsOnWithoutNotify(GameSave.CarregaTelaCheia(true));
        ClicouTelaCheia(toggleTelaCheia.isOn);
        sliderVolume.SetValueWithoutNotify(GameSave.CarregaVolume(0.8f));
        AlterouVolume(sliderVolume.value);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.Keypad7))
        {
            ClicouOpcoes();
        }
    }
    public void ClicouMenuInicial()
    {
        opcoesGroup.interactable = false;
        FadeDeTela.CarregaCena("MenuInicial", 1);
        StartCoroutine(DespausaJogo());
    }

    public void ClicouOpcoes()
    {
        opcoesAbertas = !opcoesAbertas;
        animOpcoes.SetBool("Aberto", opcoesAbertas);
        Debug.Log($"Opcoes abertas {opcoesAbertas}");
        if (opcoesAbertas)
        {
            opcoesGroup.interactable = true;
            sliderVolume.Select();
            PausaJogo();
        }
        else
        {
            opcoesGroup.interactable = false;
            Debug.Log("Despausando");
            StartCoroutine(DespausaJogo());
        }
    }

    void PausaJogo()
    {
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Pause);
        Time.timeScale = 0;
        Debug.Log("Jogo pausado");
        pausouOuDespausou?.Invoke(true);
    }

    IEnumerator DespausaJogo()
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        Debug.Log("Jogo DESpausado");
        GerenciadorDeJogo.RetornaAoEstadoAnterior();
        pausouOuDespausou?.Invoke(false);
    }

    public void ClicouTelaCheia(bool novoEstado)
    {
        Screen.fullScreen = novoEstado;
        GameSave.SalvaTelaCheia(novoEstado);
    }

    public void AlterouVolume(float novoVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(novoVolume) * 20);
        GameSave.SalvaVolume(novoVolume);
    }
}
