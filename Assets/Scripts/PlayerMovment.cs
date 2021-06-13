using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    public float speed = 10f;

    public GameObject ReplayMenu;

    public Transform TopTarget;
    Vector2 top;

    public Transform BotTarget;
    Vector2 bot;

    public Transform MidTarget;
    Vector3 mid;

    Vector3 offset;

    Vector2 pos;

    // Update is called once per frame
    void Update()
    {

        top = TopTarget.position;

        bot = BotTarget.position;

        mid = MidTarget.position;

        //movment.y = Input.GetAxisRaw("Vertical");
        
        BotTarget.position = new Vector3(transform.position.x , -4f, -4f);
        TopTarget.position = new Vector3(transform.position.x , 4f, -4f);
        MidTarget.position = new Vector3(transform.position.x, 0f, -4f);



        if (Input.GetKey(KeyCode.W))
        { 
            transform.position =  Vector2.Lerp(transform.position, top, speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position =  Vector2.Lerp(transform.position, bot, speed * Time.deltaTime);
        }
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && transform.position != mid)
        {
            transform.position = Vector2.Lerp(transform.position,new Vector2 (transform.position.x,0), speed * Time.deltaTime);
        }
        
        transform.parent.Translate(Vector3.right * (speed * Time.deltaTime));
    }


    private void FixedUpdate()
    {
        //moving using rigidbody
        //rb.MovePosition(rb.position + movment * (speed * Time.fixedDeltaTime));
    }
}