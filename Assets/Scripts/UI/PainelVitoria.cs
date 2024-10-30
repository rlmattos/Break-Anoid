using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PainelVitoria : MonoBehaviour
{
    [SerializeField] Button botaoConfirmacao;
    GameObject painel;
    private void Awake()
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
        if (novoEstado == GerenciadorDeJogo.EstadosDeJogo.Vitoria)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
