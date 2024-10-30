using System;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    public Action<Bloco> blocoDestruido;
    public void DefineCor(Color cor)
    {
        sprite.color = cor;
    }

    public void Destroi()
    {
        blocoDestruido?.Invoke(this);
        Destroy(gameObject);
    }
}
