using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    public void DefineCor(Color cor)
    {
        sprite.color = cor;
    }

    public void Destroi()
    {
        Destroy(gameObject);
    }
}
