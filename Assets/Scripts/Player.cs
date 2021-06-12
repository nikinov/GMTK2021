using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health { get; private set; } = 3;

    private PlayerMovment _playerMovment;
    private GameManager _gameManager;
    private Transform _grabbedObj;
    private bool _grabbed;
    private Vector2 _grabOffset;
    

    private void Start()
    {
        _playerMovment = FindObjectOfType<PlayerMovment>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 origin = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            print(origin);
            RaycastHit2D hit = Physics2D.Raycast(origin, new Vector3(0, 0, 1));
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Piece"))
                {
                    _grabbed = true;
                    _grabbedObj = hit.transform;
                    _grabOffset = new Vector2(origin.x - hit.transform.position.x, origin.y - hit.transform.position.y);
                }
            }
        }

        if (Input.GetMouseButton(0) && _grabbed)
        {
            Vector3 origin = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            _grabbedObj.position = new Vector3(origin.x - _grabOffset.x, origin.y - _grabOffset.y, 0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _grabbed = false;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("obsticle") || other.collider.CompareTag("Piece"))
        {
            _playerMovment.Destroy();
        }
    }

    public void AddHealth()
    {
        health += 1;
    }

    public void SubtractHealth()
    {
        health -= 1;
    }

    public void ResetHealth()
    {
        health = 3;
    }

    public void StopGrab()
    {
        _grabbed = false;
    }
}

