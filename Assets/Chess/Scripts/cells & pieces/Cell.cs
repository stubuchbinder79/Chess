using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int boardPosition = Vector2Int.zero;

    [HideInInspector]
    public Board board;

    [HideInInspector]
    public RectTransform rectTransform = null;

    [SerializeField, Tooltip("the sprite to show that the cell has been selected, or that a move is possible")]
    private GameObject highlight;
    public bool highlighted
    {
        set
        {
            highlight.SetActive(value);
        }
    }

    private void Awake()
    {
        highlighted = false;
    }

    internal void Setup(Vector2Int position, Board newBoard)
    {

        this.boardPosition = position;
        this.board = newBoard;
        this.rectTransform = GetComponent<RectTransform>();
    }
}
