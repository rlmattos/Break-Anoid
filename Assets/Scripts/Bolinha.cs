using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolinha : MonoBehaviour
{
    [SerializeField] float velocidade = 5;
    Vector2 velAtual;
    Rigidbody2D rb;
    Vector2 temposDosRepatimentos;
    float intervaloDeRebatimento = 0.1f;
    Vector3 posOriginal;
    [SerializeField] Transform trPalheta;

    enum Estados
    {
        Aguardando,
        EmJogo
    }
    Estados estadoAtual;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Reseta();
    }

    private void Dispara()
    {
        transform.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic;
        velAtual = (Vector2.up + Vector2.right).normalized * velocidade;
        estadoAtual = Estados.EmJogo;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && estadoAtual == Estados.Aguardando)
            Dispara();        
    }

    private void FixedUpdate()
    {
        rb.velocity = velAtual;
        if (rb.position.y < 0)
            Reseta();
    }

    void Reseta()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        velAtual = Vector3.zero;
        rb.MovePosition(trPalheta.position + Vector3.up * 0.5f);
        transform.SetParent(trPalheta);
        estadoAtual = Estados.Aguardando;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 pontoColisao = collision.contacts[0].point;
        Vector2 distanciaColisao = pontoColisao - rb.position;
        distanciaColisao.x = Mathf.Abs(distanciaColisao.x);
        distanciaColisao.y = Mathf.Abs(distanciaColisao.y);
        if (distanciaColisao.x > distanciaColisao.y)
        {
            if (Time.time - temposDosRepatimentos.x > intervaloDeRebatimento)
                Rebate(true);
        }
        else
        {
            if (Time.time - temposDosRepatimentos.y > intervaloDeRebatimento)
                Rebate(false);
        }

        Bloco bloco = collision.gameObject.GetComponentInParent<Bloco>();
        if (bloco)
            bloco.Destroi();
    }

    void Rebate(bool horizontal)
    {
        if (horizontal)
        {
            velAtual.x *= -1;
            temposDosRepatimentos.x = Time.time;
        }
        else
        {
            velAtual.y *= -1;
            temposDosRepatimentos.y = Time.time;
        }
    }
}
