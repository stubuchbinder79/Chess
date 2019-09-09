using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Tooltip("the game board containing the cells")]
    private Board board;
   
    public GamePieceSettings gamePieces;

    public Player white;
    public Player black;

    public Player currentPlayer { get; internal set; }
    public Player otherPlayer { get; internal set; }

    private GameObject[,] pieces = new GameObject[8, 8];
    
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
        AddPiece(gamePieces.whiteRook, white, 0, 0);
        AddPiece(gamePieces.whiteKnight, white, 1, 0);
        AddPiece(gamePieces.whiteBishop, white, 2, 0);
        AddPiece(gamePieces.whiteQueen, white, 3, 0);
        AddPiece(gamePieces.whiteKing, white, 4, 0);
        AddPiece(gamePieces.whiteBishop, white, 5, 0);
        AddPiece(gamePieces.whiteKnight, white, 6, 0);
        AddPiece(gamePieces.whiteRook, white, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(gamePieces.whitePawn, white, i, 1);
        }

        AddPiece(gamePieces.blackRook, black, 0, 7);
        AddPiece(gamePieces.blackKnight, black, 1, 7);
        AddPiece(gamePieces.blackBishop, black, 2, 7);
        AddPiece(gamePieces.blackQueen, black, 3, 7);
        AddPiece(gamePieces.blackKing, black, 4, 7);
        AddPiece(gamePieces.blackBishop, black, 5, 7);
        AddPiece(gamePieces.blackKnight, black, 6, 7);
        AddPiece(gamePieces.blackRook, black, 7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(gamePieces.blackPawn, black, i, 6);
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
