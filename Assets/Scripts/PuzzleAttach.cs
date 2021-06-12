using UnityEngine;

public class PuzzleAttach : MonoBehaviour
{
    private Vector2 _pos;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        //get mouse input in pixels
        var mouse = Input.mousePosition;

        //turning mouse pixel position to world space vectors
        _pos = _camera.ScreenToWorldPoint(mouse);
    }
    
    //setting the puzzle to mouse position on drag it works like a charm away from how stupid btw XD
    private void OnMouseDrag()
    {
        transform.position = _pos;
    }
}