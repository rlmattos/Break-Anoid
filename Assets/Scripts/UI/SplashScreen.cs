using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float duracaoFadeIn = 1;
    [SerializeField] float tempoDeExibicao = 3;
    [SerializeField] float duracaoFadeOut = 1;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        FadeDeTela.AplicaFade(Color.black, Color.clear, duracaoFadeIn);
        yield return new WaitForSeconds(duracaoFadeOut);
        yield return new WaitForSeconds(tempoDeExibicao);
        FadeDeTela.CarregaCena("MenuInicial", duracaoFadeOut, 0);
    }
}
