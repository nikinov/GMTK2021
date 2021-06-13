using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private Material highlight;
    public float initDuration = .3f;
    public float clickDuration = .3f;
    public float initShot = 1;
    public float consumeDuration = .3f;
    public float highLightIntensity;
    public float normalLightIntensity;
    public float litUpDuration;
    private bool _nothingHighlighted;

    private List<Puzzle> _puzzles;
    private List<Puzzle> _highlightPuzzles;
    private Player _player;

    private MapRecorder _mapRecorder;

    // Start is called before the first frame update
    void Start()
    {
        _nothingHighlighted = true;
        _puzzles = new List<Puzzle>();
        _highlightPuzzles = new List<Puzzle>();
        _player = FindObjectOfType<Player>();
        _mapRecorder = FindObjectOfType<MapRecorder>();
        _player.OnCollected += ConsumeHighlight;
        _mapRecorder.OnMake += MakeAdd;
        foreach (Transform t in transform)
        {
            MakeAdd(t);
        }
    }

    private void Highlight(Puzzle puzzle)
    {
        _highlightPuzzles.Add(puzzle);
        if (_nothingHighlighted)
        {
            puzzle.SetMat(highlight);
            _nothingHighlighted = false;
        }
        else
        {
            puzzle.canHighlight = true;
        }
    }

    public void ConsumeHighlight(Transform pice)
    {
        print(_highlightPuzzles.Count);
        _highlightPuzzles.Remove(_highlightPuzzles[0]);
        print("count2: " + _highlightPuzzles.Count);
        if (_highlightPuzzles.Count > 0)
        {
            _highlightPuzzles[0].SetMat(highlight);
            _nothingHighlighted = false;
        }
        else
        {
            _nothingHighlighted = true;
        }
    }

    public void MakeAdd(Transform t)
    {
        Puzzle puzzle = new Puzzle();
        puzzle.pieces = new List<Piece>();
        foreach (Transform tr in t)
        {
            try
            {
                Piece piece = tr.GetComponent<Piece>();
                if (piece)
                    puzzle.pieces.Add(piece);
                piece.OnHighlight += Highlight;
            }
            catch (Exception e)
            {
            }
        }

        puzzle.consumeDuration = consumeDuration;
        puzzle.highLightIntensity = highLightIntensity;
        puzzle.normalLightIntensity = normalLightIntensity;
        puzzle.litUpDuration = litUpDuration;

        puzzle.Init(_player);
        _puzzles.Add(puzzle);
    }

}

[Serializable]
public class Puzzle
{
    public bool isCollected;
    public bool canHighlight;
    public float consumeDuration;
    public float highLightIntensity;
    public float normalLightIntensity;
    public float litUpDuration;
    public List<Piece> pieces;
    public List<SpriteRenderer> _sprites { get; private set; }
    private List<Transform> _piecesT;
    private Player _player;

    private int _clickAmount = 1;

    public void Init(Player player)
    {
        _piecesT = new List<Transform>();
        _sprites = new List<SpriteRenderer>();
        _player = player;
        player.OnCollected += CollectPuzzle;
        foreach (Piece piece in pieces)
        {
            SpriteRenderer spriteRenderer = piece.GetComponent<SpriteRenderer>();
            _sprites.Add(spriteRenderer);
            piece.OnEndLife += _player.InstantDeath;
            _piecesT.Add(piece.transform);
            if (!piece.basePiece)
            {
                piece.OnClicked += ClickPuzzle;
            }
            piece.SetPuzzle(this);
            float factor = Mathf.Pow(2,normalLightIntensity);
            Color color = new Color(spriteRenderer.material.color.r*factor,spriteRenderer.material.color.g*factor,spriteRenderer.material.color.b*factor);
            spriteRenderer.materials[0].color = color;
        }
    }

    public void SetMat(Material mat)
    {
        foreach (SpriteRenderer sprite in _sprites)
        {
            float factor = Mathf.Pow(2,normalLightIntensity);
            Color color = new Color(sprite.material.color.r*factor,sprite.material.color.g*factor,sprite.material.color.b*factor);
            sprite.materials[0].color = color;
            sprite.material = mat;
            factor = Mathf.Pow(2,highLightIntensity);
            sprite.materials[0].DOColor(
                new Color(sprite.material.color.r * factor, sprite.material.color.g * factor,
                    sprite.material.color.b * factor), litUpDuration);
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
        if (_piecesT.Contains(puzzleT))
        {
            if (isCollected)
            {
                foreach (Piece piece in pieces)
                {
                    piece.FreePiece();
                }
                
                _player.CollectItem(_piecesT[0].parent, consumeDuration);
            }
            else
            {
                _player.InstantDeath();
            }
        }
    }
}
