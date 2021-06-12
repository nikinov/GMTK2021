using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleAttach : MonoBehaviour
{

    Vector2 pos;


    // Update is called once per frame
    void Update()
    {
        //get mouse input in pixels
        Vector3 mouse = Input.mousePosition;

        //turning mouse pixel position to world space vectors
        pos = Camera.main.ScreenToWorldPoint(mouse);
        
    }

    //setting the puzzle to mouse position on drag it works like a charm away from how stupid btw XD
    private void OnMouseDrag()
    {
        transform.position = pos;
    }
}
