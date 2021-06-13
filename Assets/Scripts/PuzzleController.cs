using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    private List<Puzzle> puzzles;
    public float initDuration = .3f;
    public float clickDuration = .3f;
    public float initShot = 1;
    public float consumeDuration = .3f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        puzzles = new List<Puzzle>();
        _player = FindObjectOfType<Player>();
        foreach (Transform t in transform)
        {
            Puzzle puzzle = new Puzzle();
            puzzle.pieces = new List<Piece>();
            puzzle.consumeDuration = consumeDuration;
            foreach (Transform tr in t)
            {
                try
                {
                    Piece piece = tr.GetComponent<Piece>();
                    if(piece)
                        puzzle.pieces.Add(piece);
                }
                catch (Exception e) { }
            }
            print(puzzle.pieces.Count);
            puzzle.Init(_player);
            puzzles.Add(puzzle);
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
    public bool isCollected;
    public float consumeDuration;
    public List<Piece> pieces;
    private List<Transform> piecesT;
    private Player _player;

    private int _clickAmount = 1;

    public void Init(Player player)
    {
        piecesT = new List<Transform>();
        _player = player;
        player.OnCollected += CollectPuzzle;
        foreach (Piece piece in pieces)
        {
            piece.OnEndLife += _player.InstantDeath;
            piecesT.Add(piece.transform);
            if (!piece.basePiece)
            {
                piece.OnClicked += ClickPuzzle;
            }
        }
    }
    
    private void ClickPuzzle()
    {
        _clickAmount += 1;
        if (_clickAmount >= pieces.Count)
        {
            isCollected = true;
            foreach (Piece piece in pieces)
            {
                piece.gameObject.tag = "Collect";
            }
        }
        _player.StopGrab();
    }

    private void CollectPuzzle(Transform puzzleT)
    {
        if (piecesT.Contains(puzzleT))
        {
            if (isCollected)
            {
                foreach (Piece piece in pieces)
                {
                    piece.FreePiece();
                }
                _player.CollectItem(piecesT[0].parent, consumeDuration);
            }
            else
            {
                _player.InstantDeath();
            }
        }
    }
}
