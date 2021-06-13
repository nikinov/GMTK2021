using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform backgrounds;
    private Player _player;
    private List<Transform> _backgrounds;

    private void Start()
    {
        _backgrounds = new List<Transform>();
        foreach (Transform background in backgrounds)
        {
            _backgrounds.Add(background);
        }
        
        _player = FindObjectOfType<Player>();
    }
}
