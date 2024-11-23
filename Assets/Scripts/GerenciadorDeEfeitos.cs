using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeEfeitos : MonoBehaviour
{
    public static GerenciadorDeEfeitos instancia;
    [SerializeField] bool ligado = true;
    [SerializeField] Efeito_SO[] databaseEfeitos;
    int quantidadeEfeitos = -1;

    public enum Efeitos
    {
        BlocoHit,
        BlocoDestroi,
        BolinhaDestroi,
        BolinhaHit
    }

    private void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
        quantidadeEfeitos = databaseEfeitos.Length;
    }

    public void InstanciaEfeito(Efeitos efeitoParaInstanciar, Vector3 posicao, Quaternion rotacao)
    {
        if (!ligado)
            return;
        for (int i = 0; i < quantidadeEfeitos; i++)
        {
            if (databaseEfeitos[i].Efeito == efeitoParaInstanciar)
                Instantiate(databaseEfeitos[i].prefab, posicao, rotacao);
        }
    }
}
