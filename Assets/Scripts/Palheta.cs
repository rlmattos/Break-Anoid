using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palheta : MonoBehaviour
{
    [SerializeField] float velMovimento;
    float inputHorizontal;
    Vector2 velAtual;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LeInput();
    }

    private void LeInput()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        AplicaMovimento();
    }

    private void AplicaMovimento()
    {
        velAtual.x = inputHorizontal * velMovimento;
        rb.velocity = velAtual;
    }
}
