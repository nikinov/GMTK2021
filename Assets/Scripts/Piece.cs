using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public delegate void ClickAction();
    public event ClickAction OnClicked;
    public bool basePiece;
    
    public float initDuration = .3f;
    public float clickDuration = .1f;
    public float initShot = 1;

    private bool _init = false;
    private bool _grabbed;
    
    private Vector3 _initRot;
    private Vector3 _initScale;
    private Vector2 _startPos;

    private void Start()
    {
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
             transform.DOMoveX(transform.position.x - 1, initDuration);
             transform.DORotate(_initRot, initDuration);
             StartCoroutine(waitForInit());
         }
         else if (other.transform == transform.parent.GetChild(0) && _init && !basePiece)
         {
             OnClicked?.Invoke();
             transform.DOMove(transform.parent.GetChild(0).position, clickDuration);
             transform.DORotate(transform.parent.GetChild(0).localEulerAngles, clickDuration);
         }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    IEnumerator waitForInit()
    {
        yield return new WaitForSeconds(initDuration);
        _init = true;
    }
}
