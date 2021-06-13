using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void CollectAction(Transform puzzle);
    public event CollectAction OnCollected;

    [SerializeField] private float consumeTime;

    private PlayerMovment _playerMovment;
    private GameManager _gameManager;
    private UiManager _uiManager;
    
    private Transform _grabbedObj;
    private bool _grabbed;
    private Vector2 _grabOffset;
    

    private void Start()
    {
        _uiManager = FindObjectOfType<UiManager>();
        _playerMovment = FindObjectOfType<PlayerMovment>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 origin = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("obsticle") || other.gameObject.CompareTag("Piece"))
        {
            InstantDeath();
        }
        else if (other.gameObject.CompareTag("Collect"))
        {
            OnCollected?.Invoke(other.transform);
            foreach (Transform tr in other.transform.parent)
            {
                tr.gameObject.tag = "Untagged";
            }
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            FinishLevel();
        }
    }

    public void FinishLevel()
    {
        gameObject.SetActive(false);

        _uiManager.FinishUI(1);
    }

    public void InstantDeath()
    {
        gameObject.SetActive(false);

        _uiManager.DeathUI(1);
    }

    public void StopGrab()
    {
        _grabbed = false;
    }

    public void CollectItem(Transform forDestruction, float duration)
    {
        StartCoroutine(waitForConsume(forDestruction, duration));
    }


    IEnumerator waitForConsume(Transform piece, float duration)
    {
        piece.DOScale(Vector3.zero, duration);
        piece.DOScale(piece.localEulerAngles - new Vector3(0, 0, 180), duration);
        piece.DOMoveX(piece.position.x - 1, duration);
        yield return new WaitForSeconds(duration);
        piece.gameObject.SetActive(false);
    }
}

