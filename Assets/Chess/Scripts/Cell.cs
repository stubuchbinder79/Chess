using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int boardPosition = Vector2Int.zero;

    [HideInInspector]
    public Board board;

    [HideInInspector]
    public RectTransform rectTransform = null;

    internal void Setup(Vector2Int position, Board newBoard)
    {

        this.boardPosition = position;
        this.board = newBoard;
        this.rectTransform = GetComponent<RectTransform>();
    }
}
