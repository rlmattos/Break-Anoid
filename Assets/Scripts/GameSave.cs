using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave
{
    const string VOLUME = "VOLUME";
    const string TELA_CHEIA = "TELA_CHEIA";

    public static void SalvaVolume(float volume)
    {
        PlayerPrefs.SetFloat(VOLUME, volume);
    }

    public static float CarregaVolume(float volumePadrao)
    {
        return PlayerPrefs.GetFloat(VOLUME, volumePadrao);
    }

    public static void SalvaTelaCheia(bool telaCheia)
    {
        PlayerPrefs.SetInt(TELA_CHEIA, telaCheia ? 1 : 0);
    }

    public static bool CarregaTelaCheia(bool valorPadrao)
    {
        return PlayerPrefs.GetInt(TELA_CHEIA, valorPadrao ? 1 : 0) == 1;
    }
}
