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

    public static void CarregaCena(string nomeDaCena, float duracaoDoFade)
    {
        if (!inicializado || spriteDeFade == null)
            Inicializa();
        AplicaFade(duracaoDoFade, nomeDaCena);
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


        RectTransform spriteDeFadeTr = spriteDeFade.GetComponent<RectTransform>();
        spriteDeFadeTr.anchorMin = Vector2.zero;
        spriteDeFadeTr.anchorMax = Vector2.one;
        spriteDeFadeTr.anchoredPosition = Vector2.zero;

        DontDestroyOnLoad(canvas);

        inicializado = true;
    }

    static async void AplicaFade(float duracaoDoFade, string nomeDaCena)
    {
        spriteDeFade.enabled = true;
        spriteDeFade.color = Color.clear;
        Color corAtual;
        float tempoPassado = 0;
        while (duracaoDoFade > tempoPassado)
        {
            corAtual = Color.Lerp(Color.clear, Color.black, tempoPassado / duracaoDoFade);
            spriteDeFade.color = corAtual;
            await Task.Delay(50);
            tempoPassado += 0.05f;
        }

        spriteDeFade.color = Color.black;

        AsyncOperation carregamentoDeCena = SceneManager.LoadSceneAsync(nomeDaCena);
        carregamentoDeCena.allowSceneActivation = false;
        while (carregamentoDeCena.progress < 0.8)
        {
            Debug.Log(carregamentoDeCena.progress);
            await Task.Yield();
        }

        carregamentoDeCena.allowSceneActivation = true;
        await Task.Delay(1000);

        tempoPassado = 0;
        while (duracaoDoFade > tempoPassado)
        {
            corAtual = Color.Lerp(Color.black, Color.clear, tempoPassado / duracaoDoFade);
            spriteDeFade.color = corAtual;
            await Task.Delay(50);
            tempoPassado += 0.05f;
        }

        spriteDeFade.color = Color.clear;
        spriteDeFade.enabled = false;
    }
}
