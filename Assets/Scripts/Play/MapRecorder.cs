using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MapRecorder : MonoBehaviour
{
    public delegate void MakeAction(Transform t);

    public event MakeAction OnMake;
    
    public List<KeyCode> keyCodes;
    public List<GameObject> puzzlePrefs;
    public bool recoreder;
    public string currentSave;

    [SerializeField] private Transform finishLine;
    [SerializeField] private float finishLineShow;
    
    private Player _player;
    private PlayerMovment _playerMovement;
    private PuzzleController _puzzleController;
    private List<Insert> _timeStamps;
    private float _prevTime;

    private void Awake()
    {
        _timeStamps = new List<Insert>();
        
        _player = FindObjectOfType<Player>();
        _playerMovement = FindObjectOfType<PlayerMovment>();
        _puzzleController = FindObjectOfType<PuzzleController>();

        if (recoreder)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (keyCode == KeyCode.X)
                {
                    throw new Exception("sorry dude X is for save config");
                }
                if (keyCode == KeyCode.S || keyCode == KeyCode.W)
                {
                    throw new Exception("nope can't use S and X, they are player controls");
                }
            }
        }
        else
        {
            foreach (string str in PlayerPrefs.GetString(currentSave).Split('$'))
            {
                string[] nsr = str.Split('#');
                Insert insert = new Insert();
                insert.timeStamp = float.Parse(nsr[0]);
                insert.type = int.Parse(nsr[1]);
                _timeStamps.Add(insert);
                print(insert.type + "   " + insert.timeStamp);
            }
            GenerateMap();
        }
    }

    private void Start()
    {
        _prevTime = Time.time;
    }

    private void Update()
    {
        if (recoreder)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Insert insert = new Insert();
                    insert.timeStamp = -(Time.time - _prevTime);
                    insert.type = keyCodes.IndexOf(keyCode);
                    _timeStamps.Add(insert);
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                string encode = "";
                bool first = true;
                foreach (Insert insert in _timeStamps)
                {
                    if (first)
                    {
                        encode = encode + insert.timeStamp + "#" + insert.type;
                        first = false;
                    }
                    else
                        encode = encode + "$" + insert.timeStamp + "#" + insert.type;
                }
                PlayerPrefs.SetString(currentSave, encode);
            }
        }
    }

    public void GenerateMap()
    {
        float timing = 0f;
        foreach (Insert insert in _timeStamps)
        {
            timing += insert.timeStamp;
            StartCoroutine(waitForInstantiate(insert, timing));
        }

        StartCoroutine(waitForFinish());
    }

    IEnumerator waitForInstantiate(Insert insert, float timing)
    {
        yield return new WaitForSeconds(-(timing));
        GameObject g = Instantiate(
            puzzlePrefs[insert.type],
            _puzzleController.transform);
        g.transform.position += new Vector3(_player.transform.position.x, 0);
        OnMake?.Invoke(g.transform);
    }

    IEnumerator waitForFinish()
    {
        yield return new WaitForSeconds(finishLineShow);
        finishLine.position += new Vector3(_player.transform.position.x + 30, 0);
    }
}

[Serializable]
class Insert
{
    public float timeStamp;

    public int type;
}
