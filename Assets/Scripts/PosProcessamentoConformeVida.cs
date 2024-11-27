using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PosProcessamentoConformeVida : MonoBehaviour
{
    [SerializeField] Volume volume;
    float valorAtual;
    private void OnEnable()
    {
        Vida.atualizouVida += AoPerderVida;
    }

    private void OnDisable()
    {
        Vida.atualizouVida -= AoPerderVida;
    }

    private void Start()
    {
        volume.weight = 0;
        StartCoroutine(AtualizaEfeito());
    }

    void AoPerderVida(float vidaAtual)
    {
        valorAtual = 1 - ((vidaAtual) / Vida.VidaMaxima);
    }

    IEnumerator AtualizaEfeito()
    {
        while (true)
        {
            volume.weight = Mathf.Lerp(volume.weight, valorAtual, Time.deltaTime * 5);
            yield return 0;
        }
    }
}
