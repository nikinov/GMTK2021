using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private List<Puzzle> puzzles;
    
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        foreach (Puzzle puzzle in puzzles)
        {
            puzzle.Init(_player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

[Serializable]
class Puzzle
{
    [SerializeField] private List<Piece> pieces;
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        foreach (Piece piece in pieces)
        {
            if (!piece.basePiece)
            {
                piece.OnClicked += ClickPuzzle;
            }
        }
    }
    
    private void ClickPuzzle()
    {
        _player.StopGrab();
    }

    public void Miss()
    {
        
    }
}

[Serializable]
class ColorBeat
{
    public int type;

    public List<Color> themeColors;
}
