using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FadeDeTela : MonoBehaviour
{
    static Image spriteDeFade;
    static bool inicializado;
    public static Action<string> mudouDeCena;

    public static void CarregaCena(string nomeDaCena, float duracaoDoFade, float tempoDeEspera = 3)
    {
        if (!inicializado || spriteDeFade == null)
            Inicializa();

        ExecutaCarregamento(nomeDaCena, duracaoDoFade, tempoDeEspera);
    }

    static async void ExecutaCarregamento(string nomeDaCena, float duracaoDoFade, float duracaoDaEspera)
    {
        await AplicaFadeAssincrono(Color.clear, Color.black, duracaoDoFade);
        if (duracaoDaEspera > 0)
        {
            SceneManager.LoadScene("Loading");
            await AplicaFadeAssincrono(Color.black, Color.clear, duracaoDoFade * 0.5f);
        }
        AsyncOperation carregamentoDeCena = SceneManager.LoadSceneAsync(nomeDaCena);
        carregamentoDeCena.allowSceneActivation = false;
        while (carregamentoDeCena.progress < 0.8)
        {
            Debug.Log(carregamentoDeCena.progress);
            await Task.Yield();
        }
        if (duracaoDaEspera > 0)
        {
            await Task.Delay((int)(1000 * duracaoDaEspera));
            await AplicaFadeAssincrono(Color.clear, Color.black, duracaoDoFade);
        }
        carregamentoDeCena.allowSceneActivation = true;
        mudouDeCena?.Invoke(nomeDaCena);
        await Task.Delay(150);
        await AplicaFadeAssincrono(Color.black, Color.clear, duracaoDoFade * 0.5f);

    }

    static void Inicializa()
    {
        Canvas canvas = new GameObject("CanvasFader", new System.Type[]
        {
            typeof(Canvas),
            typeof(CanvasScaler),
            typeof(GraphicRaycaster)
        }).GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = Int16.MaxValue;

        GameObject spriteFadeGO = new GameObject("Sprite de Fade");
        spriteFadeGO.transform.SetParent(canvas.transform);
        spriteDeFade = spriteFadeGO.AddComponent<Image>();
        spriteDeFade.raycastTarget = false;

        RectTransform spriteDeFadeTr = spriteDeFade.GetComponent<RectTransform>();
        spriteDeFadeTr.anchorMin = Vector2.zero;
        spriteDeFadeTr.anchorMax = Vector2.one;
        spriteDeFadeTr.anchoredPosition = Vector2.zero;

        DontDestroyOnLoad(canvas);

        inicializado = true;
    }
    public static async void AplicaFade(Color corInicial, Color corFinal, float duracaoDoFade)
    {
        await AplicaFadeAssincrono(corInicial, corFinal, duracaoDoFade);
    }
    static async Task AplicaFadeAssincrono(Color corInicial, Color corFinal, float duracaoDoFade)
    {
        if (!inicializado || spriteDeFade == null)
            Inicializa();
        spriteDeFade.enabled = true;
        spriteDeFade.color = corInicial;
        Color corAtual;
        float tempoPassado = 0;
        while (duracaoDoFade > tempoPassado)
        {
            corAtual = Color.Lerp(corInicial, corFinal, tempoPassado / duracaoDoFade);
            spriteDeFade.color = corAtual;
            await Task.Delay(50);
            tempoPassado += 0.05f;
        }
        spriteDeFade.color = corFinal;
    }
}
