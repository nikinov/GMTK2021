using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public delegate void ClickAction();
    public event ClickAction OnClicked;
    public delegate void EndLifeAction();
    public event EndLifeAction OnEndLife;
    
    public bool basePiece;
    public bool collcted;
    
    private float initDuration = .3f;
    private float clickDuration = .3f;
    private float initShot = 1;

    private bool _init = false;
    private bool _grabbed;
    
    private Vector3 _initRot;
    private Vector3 _initScale;
    private Vector2 _startPos;

    private PuzzleController _puzzleController;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _puzzleController = FindObjectOfType<PuzzleController>();
        initDuration = _puzzleController.initDuration;
        clickDuration = _puzzleController.clickDuration;
        initShot = _puzzleController.initShot;
        if (basePiece)
            collcted = true;
        _initScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _initRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 180);
        transform.localScale = new Vector3(.1f, .1f, .1f);
    }
    
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("RightBorder") && !_init)
         {
             transform.DOScale(_initScale, initDuration);
             transform.DOMoveX(transform.position.x - initShot, initDuration);
             transform.DORotate(_initRot, initDuration);
             if (!basePiece)
                 transform.parent.GetChild(transform.GetSiblingIndex() - 1).DOMoveX(
                     transform.parent.GetChild(transform.GetSiblingIndex() - 1).position.x - initShot, initDuration);
             else
                 StartCoroutine(waitForAnim(initDuration));
             _init = true;
         }
         else if (_init && !basePiece && !other.CompareTag("Player"))
         {
             if (other.transform == transform.parent.GetChild(transform.GetSiblingIndex() - 1))
             {
                  print("yayyyyy");
                  OnClicked?.Invoke();
                  transform.DOMove(transform.parent.GetChild(transform.GetSiblingIndex() - 1).position, clickDuration);
                  transform.DORotate(transform.parent.GetChild(transform.GetSiblingIndex() - 1).localEulerAngles, clickDuration);
                  collcted = true;
                  _init = false;
                  StartCoroutine(waitForAnim(clickDuration));
             }
         }
         else if (other.gameObject.CompareTag("LeftBorder"))
         {
             OnEndLife?.Invoke();
         }
    }

    public void FreePiece()
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
    }

    IEnumerator waitForAnim(float duration)
    {
        yield return new WaitForSeconds(duration);
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}





