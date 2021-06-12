using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    public float speed = 10f;

    [SerializeField] private Rigidbody2D rb;
    
    private Vector2 movment;

    public Transform cam;

    public GameObject ReplayMenu;
    // Update is called once per frame
    void Update()
    {
      
        //geting H & V input
        movment.x = 1f;

        movment.y = Input.GetAxisRaw("Vertical");


        //cam follow 
        cam.position = new Vector3 (transform.position.x , 0f, -4f);
        
    }


    private void FixedUpdate()
    {
        //moving using rigidbody
        rb.MovePosition(rb.position + movment * (speed * Time.fixedDeltaTime));
    }

    //death and replaying menu view
    public void Destroy()
    {
        Destroy(gameObject);

        ReplayMenu.SetActive(true);
    }

}