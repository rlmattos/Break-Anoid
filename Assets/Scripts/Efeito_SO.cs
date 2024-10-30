using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Efeito")]
public class Efeito_SO : ScriptableObject
{
    [SerializeField] public GameObject prefab;
    [SerializeField] GerenciadorDeEfeitos.Efeitos efeito;
    public GerenciadorDeEfeitos.Efeitos Efeito => efeito;
}
