using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    public float speed = 10f;

    [SerializeField] private Rigidbody2D rb;
    
    private Vector2 movment;

    // Update is called once per frame
    void Update()
    {
        movment.x = Input.GetAxisRaw("Horizontal");

        movment.y = Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movment * (speed * Time.fixedDeltaTime));
    }
}