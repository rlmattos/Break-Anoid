using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeSFX : MonoBehaviour
{
    public static GerenciadorDeSFX instancia;
    [SerializeField] bool ligado = true;
    [SerializeField] SFX_SO[] databaseEfeitos;
    AudioSource[] trilhas = new AudioSource[5];
    int quantidadeEfeitos = -1;

    public enum Efeitos
    {
        BlocoHit,
        PalhetaHit,
        ParedeHit,
        BolinhaHit
    }

    private void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
        quantidadeEfeitos = databaseEfeitos.Length;
        for (int i = 0; i < trilhas.Length; i++)
        {
            trilhas[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TocaSFX(Efeitos efeitoParaInstanciar, float volume, float pitch)
    {
        if (!ligado)
            return;
        Debug.Log($"Tocando {efeitoParaInstanciar}");
        for (int i = 0; i < quantidadeEfeitos; i++)
        {
            if (databaseEfeitos[i].Efeito == efeitoParaInstanciar)
            {
                int indiceDaTrilha = EscolheTrilhaLivre(pitch);
                if(indiceDaTrilha != -1)
                    trilhas[indiceDaTrilha].PlayOneShot(databaseEfeitos[i].clip, volume);
                else
                    Debug.LogWarning($"SFX {efeitoParaInstanciar} não pode ser tocado pois todas as {trilhas.Length} estão ocupadas");
                return;
            }
        }
    }

    private int EscolheTrilhaLivre(float pitch)
    {
        for (int i = 0; i < trilhas.Length; i++)
        {
            if (!trilhas[i].isPlaying)
            {
                trilhas[i].pitch = pitch;
                return i;
            }
        }
        return 0;
    }
}
