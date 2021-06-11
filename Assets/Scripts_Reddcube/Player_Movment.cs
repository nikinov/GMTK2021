using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movment : MonoBehaviour
{

    public float speed = 10f;

    Vector2 movment;

    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {

        movment.x = Input.GetAxisRaw("Horizontal");

        movment.y = Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + movment * speed * Time.fixedDeltaTime);

    }
}
