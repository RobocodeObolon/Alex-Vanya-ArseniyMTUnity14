using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carmove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");   
        turnInput = -Input.GetAxis("Horizontal"); 
    }

    void FixedUpdate()
    {
        
        rb.velocity = transform.up * moveInput * moveSpeed;

       
        rb.MoveRotation(rb.rotation + turnInput * turnSpeed * Time.fixedDeltaTime);
    }
 
}
