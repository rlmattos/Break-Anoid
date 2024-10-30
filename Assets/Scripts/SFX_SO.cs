using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX")]
public class SFX_SO : ScriptableObject
{
    [SerializeField] public AudioClip clip;
    [SerializeField] GerenciadorDeSFX.Efeitos efeito;
    public GerenciadorDeSFX.Efeitos Efeito => efeito;
}
