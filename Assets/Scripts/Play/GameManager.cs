using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform backgrounds;
    [SerializeField] private float backgroundSize;
    [SerializeField] private float songDelay;
    
    private Player _player;
    private AudioManager _audioManager;
    private List<Transform> _backgrounds;
    private Transform _lastTransform;
    private int count = -1;

    private void Awake()
    {
        PlayerPrefs.SetString("#", "JOURNEY");
        _audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(waitForSong());
    }

    private void Start()
    {
        _backgrounds = new List<Transform>();
        foreach (Transform background in backgrounds)
        {
            _backgrounds.Add(background);
        }
        _lastTransform = _backgrounds[_backgrounds.Count - 1];
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (_player.transform.position.x >= _lastTransform.position.x - backgroundSize)
        {
            count += 1;
            if (count > _backgrounds.Count - 1)
                count = 0;
            _backgrounds[count].position = new Vector3(_lastTransform.position.x + backgroundSize, _backgrounds[count].position.y, _backgrounds[count].position.z);
            _lastTransform = _backgrounds[count];
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator waitForSong()
    {
        yield return new WaitForSeconds(songDelay);
        _audioManager.PlaySound(PlayerPrefs.GetString("#"));
    }
}
