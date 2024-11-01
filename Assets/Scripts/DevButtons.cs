using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevButtons : MonoBehaviour
{
    [SerializeField] GameObject demoBlocks;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale *= 2;
        }else if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale *= 0.5f;
        }else if (Input.GetKeyDown(KeyCode.Home))
        {
            demoBlocks.SetActive(!demoBlocks.activeSelf);
        }else if(Input.GetKeyDown(KeyCode.PageUp))
        {
            GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Vitoria);
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
            GerenciadorDeJogo.AtualizaEstado(GerenciadorDeJogo.EstadosDeJogo.Derrota);
        }
    }
}
