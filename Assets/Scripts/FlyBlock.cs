using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBlock : MonoBehaviour
{
    public float speed = 5f;  // Set your desired speed here
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");  // Get player input
        Vector3 velocity = new Vector3(horizontalInput * speed, rb.velocity.y, 0);
        rb.velocity = velocity;
    }
}
