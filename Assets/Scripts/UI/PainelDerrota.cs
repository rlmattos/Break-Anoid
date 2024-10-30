using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PainelDerrota : MonoBehaviour
{
    GameObject painel;
    [SerializeField] Button botaoConfirmacao;
    private void Start()
    {
        painel = transform.GetChild(0).gameObject;
        FechaPainel();
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
        if (novoEstado == GerenciadorDeJogo.EstadosDeJogo.Derrota)
            AbrePainel();
    }
    void AbrePainel()
    {
        painel.SetActive(true);
    }

    void FechaPainel()
    {
        painel.SetActive(false);
    }
    void ClicouBotaoConfirmacao()
    {
        FechaPainel();
        GerenciadorDeJogo.ResetaJogo();
    }
}
