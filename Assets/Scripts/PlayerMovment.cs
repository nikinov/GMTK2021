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

    public Transform TopTarget;
    Vector2 top;

    public Transform BotTarget;
    Vector2 bot;

    public Transform MidTarget;
    Vector2 mid;


    private void Start()
    {

        

    }






    // Update is called once per frame
    void Update()
    {

        top = TopTarget.position;

        bot = BotTarget.position;

        mid = MidTarget.position;

        //geting H & V input
        movment.x = 1f;

        //movment.y = Input.GetAxisRaw("Vertical");


        //cam follow 
        cam.position = new Vector3 (transform.position.x , 0f, -4f);


        if (Input.GetKey(KeyCode.W))
        {
           transform.position =  Vector2.MoveTowards(transform.position, top, speed * Time.deltaTime);
            
        }
      



        if (Input.GetKey(KeyCode.S))
        {

          transform.position =  Vector2.MoveTowards(transform.position, bot, speed * Time.deltaTime);

        }
        
        

       if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
       {
            transform.position = Vector2.MoveTowards(transform.position, mid, speed * Time.deltaTime);
       }


        //transform.position +=new Vector3 (1f,0f,0f) * Time.deltaTime;


    }


    private void FixedUpdate()
    {
        //moving using rigidbody
       //rb.MovePosition(rb.position + new Vector2(movment.x,0f) * (speed * Time.fixedDeltaTime));







    }

    //death and replaying menu view
    public void Destroy()
    {
        Destroy(gameObject);

        ReplayMenu.SetActive(true);
    }

}