using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Tooltip("the game board containing the cells")]
    private Board board;

    public Player white;
    public Player black;

    public Player currentPlayer { get; internal set; }
    public Player otherPlayer { get; internal set; }

    internal GameObject[,] pieces = new GameObject[8, 8];

    // game piece prefabs
    [SerializeField] private GameObject WhitePawn;
    [SerializeField] private GameObject BlackPawn;

    [SerializeField] private GameObject WhiteRook;
    [SerializeField] private GameObject BlackRook;

    [SerializeField] private GameObject WhiteKnight;
    [SerializeField] private GameObject BlackKnight;

    [SerializeField] private GameObject WhiteBishop;
    [SerializeField] private GameObject BlackBishop;

    [SerializeField] private GameObject WhiteQueen;
    [SerializeField] private GameObject BlackQueen;

    [SerializeField] private GameObject WhiteKing;
    [SerializeField] private GameObject BlackKing;

    private void Start()
    {
        white = new Player("white", true);
        black = new Player("black", false);

        currentPlayer = white;
        otherPlayer = black;

        SetPieces();
    }

    private void SetPieces()
    {
        AddPiece(WhiteRook, white, 0, 0);
        AddPiece(WhiteKnight, white, 1, 0);
        AddPiece(WhiteBishop, white, 2, 0);
        AddPiece(WhiteQueen, white, 3, 0);
        AddPiece(WhiteKing, white, 4, 0);
        AddPiece(WhiteBishop, white, 5, 0);
        AddPiece(WhiteKnight, white, 6, 0);
        AddPiece(WhiteBishop, white, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(WhitePawn, white, i, 1);
        }

        AddPiece(BlackRook, black, 0, 7);
        AddPiece(BlackKnight, black, 1, 7);
        AddPiece(BlackBishop, black, 2, 7);
        AddPiece(BlackQueen, black, 3, 7);
        AddPiece(BlackKing, black, 4, 7);
        AddPiece(BlackBishop, black, 5, 7);
        AddPiece(BlackKnight, black, 6, 7);
        AddPiece(BlackRook, black, 7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(BlackPawn, black, i, 6);
        }


    }

    private void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
        pieces[col, row] = pieceObject;
        pieceObject.GetComponent<Piece>().originalLocation = new Vector2Int(col, row);
    }


    public GameObject PieceAtGrid(Vector2Int gridPoint)
	{
		if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
		{
			return null;
		}
		return pieces[gridPoint.x, gridPoint.y];
	}

	public Vector2Int GridForPiece(GameObject piece)
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (pieces[i, j] == piece)
				{
					return new Vector2Int(i, j);
				}
			}
		}

		return new Vector2Int(-1, -1);
	}

    public virtual bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return currentPlayer.pieces.Contains(piece);
    }

    public bool CastleIsActive(GameObject piece)
    {
        return (DoesPieceBelongToCurrentPlayer(piece) && currentPlayer.castleIsActive);
    }

}
