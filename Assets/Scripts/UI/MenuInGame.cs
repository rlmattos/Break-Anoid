using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        if (GerenciadorDeJogo.estadoAtual == GerenciadorDeJogo.EstadosDeJogo.Intro ||
            GerenciadorDeJogo.estadoAtual == GerenciadorDeJogo.EstadosDeJogo.Vitoria ||
            GerenciadorDeJogo.estadoAtual == GerenciadorDeJogo.EstadosDeJogo.Derrota ||
            GerenciadorDeJogo.estadoAtual == 0)
            return;
        opcoesAbertas = !opcoesAbertas;
        animOpcoes.SetBool("Aberto", opcoesAbertas);
        Debug.Log($"Opcoes abertas {opcoesAbertas}");
        if (opcoesAbertas)
        {
            if(GerenciadorDeJogo.estadoAtual == GerenciadorDeJogo.EstadosDeJogo.Pause)
                GerenciadorDeJogo.RetornaAoEstadoAnterior();
            StopAllCoroutines();
            opcoesGroup.interactable = true;
            sliderVolume.Select();
            PausaJogo();
        }
        else
        {
            StopCoroutine("DespausaJogo");
            opcoesGroup.interactable = false;
            Debug.Log("Despausando");
            StartCoroutine(DespausaJogo());
        }
    }

    void PausaJogo()
    {
        Time.timeScale = 0;
        Debug.Log("Jogo pausado");
        pausouOuDespausou?.Invoke(true);
        GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Pause);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator DespausaJogo()
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        Debug.Log("Jogo DESpausado");        
        pausouOuDespausou?.Invoke(false);
        GerenciadorDeJogo.RetornaAoEstadoAnterior();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;        
    }

    public void ClicouTelaCheia(bool novoEstado)
    {
        Screen.fullScreen = novoEstado;
        GameSave.SalvaTelaCheia(novoEstado);
    }

    public void AlterouVolume(float novoVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Lerp(0.0001f, 1, sliderVolume.value / sliderVolume.maxValue)) * 20);
        GameSave.SalvaVolume(novoVolume);
    }
}
