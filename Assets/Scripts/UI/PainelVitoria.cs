using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PainelVitoria : MonoBehaviour
{
    [SerializeField] Button botaoConfirmacao;
    [SerializeField] Animator painelAnim;
    private void Awake()
    {
        FechaPainel(true);
    }
    private void OnEnable()
    {
        botaoConfirmacao.onClick.AddListener(ClicouBotaoConfirmacao);
        GerenciadorDeJogo.mudouDeEstado += MudouEstadoDeJogo;
    }

    private void OnDisable()
    {
        botaoConfirmacao.onClick.RemoveListener(ClicouBotaoConfirmacao);
        GerenciadorDeJogo.mudouDeEstado -= MudouEstadoDeJogo;
    }

    void MudouEstadoDeJogo(GerenciadorDeJogo.EstadosDeJogo novoEstado)
    {
        if (novoEstado == GerenciadorDeJogo.EstadosDeJogo.Vitoria)
            AbrePainel();
    }

    void AbrePainel()
    {
        painelAnim.Play("Aparece");
        GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.UI_Vitoria, 1, 1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void FechaPainel(bool semAnimacao = false)
    {
        painelAnim.Play("Desaparece", 0, semAnimacao ? 1 : 0);
    }

    void ClicouBotaoConfirmacao()
    {
        FechaPainel();
        FadeDeTela.CarregaCena("MenuInicial", 1.5f);
    }
}
