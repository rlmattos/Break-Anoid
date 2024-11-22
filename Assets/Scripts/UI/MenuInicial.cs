using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] Button botaoIniciar;
    [SerializeField] Button botaoAbreOpcoes;
    [SerializeField] Button botaoFechaOpcoes;
    [SerializeField] Button botaoSair;
    [SerializeField] Toggle toggleTelaCheia;
    [SerializeField] Slider sliderVolume;
    [SerializeField] CanvasGroup menuInicialGroup;
    [SerializeField] CanvasGroup opcoesGroup;

    [SerializeField] AudioMixer mixer;
    [SerializeField] Animator animOpcoes;
    bool opcoesAbertas;

    private void OnEnable()
    {
        botaoIniciar.onClick.AddListener(ClicouJogar);
        botaoAbreOpcoes.onClick.AddListener(ClicouOpcoes);
        botaoFechaOpcoes.onClick.AddListener(ClicouOpcoes);
        botaoSair.onClick.AddListener(ClicouSair);
        toggleTelaCheia.onValueChanged.AddListener(ClicouTelaCheia);
        sliderVolume.onValueChanged.AddListener(AlterouVolume);
    }
    private void OnDisable()
    {
        botaoIniciar.onClick.RemoveListener(ClicouJogar);
        botaoAbreOpcoes.onClick.RemoveListener(ClicouOpcoes);
        botaoFechaOpcoes.onClick.RemoveListener(ClicouOpcoes);
        botaoSair.onClick.RemoveListener(ClicouSair);
        toggleTelaCheia.onValueChanged.RemoveListener(ClicouTelaCheia);
        sliderVolume.onValueChanged.RemoveListener(AlterouVolume);
    }

    private void Start()
    {
        animOpcoes.Play("Desaparece", 0, 1);
        opcoesAbertas = false;
        botaoIniciar.Select();
        toggleTelaCheia.SetIsOnWithoutNotify(GameSave.CarregaTelaCheia(true));
        ClicouTelaCheia(toggleTelaCheia.isOn);
        sliderVolume.SetValueWithoutNotify(GameSave.CarregaVolume(0.8f));
        AlterouVolume(sliderVolume.value);
    }

    public void ClicouJogar()
    {
        FadeDeTela.CarregaCena("Jogo", 1);
        menuInicialGroup.interactable = false;
        opcoesGroup.interactable = false;
    }

    public void ClicouSair()
    {
        FadeDeTela.AplicaFade(Color.clear, Color.black, 1);
        Invoke("FechaJogo", 1.5f);
        menuInicialGroup.interactable = false;
        opcoesGroup.interactable = false;
    }

    void FechaJogo()
    {
        Application.Quit();
    }

    public void ClicouOpcoes()
    {
        opcoesAbertas = !opcoesAbertas;
        animOpcoes.SetBool("Aberto", opcoesAbertas);
        if (opcoesAbertas)
        {
            menuInicialGroup.interactable = false;
            opcoesGroup.interactable = true;
            sliderVolume.Select();
        }
        else
        {
            menuInicialGroup.interactable = true;
            opcoesGroup.interactable = false;
            botaoAbreOpcoes.Select();
        }
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
