using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Tooltip("the game board containing the cells")]
    private Board board;
   
    public GamePieceSettings gamePieces;

    public Player white;
    public Player black;

    [SerializeField, Tooltip("sprite used to display player 1 name")]
    private PlayerSprite player1;
    [SerializeField, Tooltip("sprite used to display player 2 name")]
    private PlayerSprite player2;

    public Player currentPlayer { get; internal set; }
    public Player otherPlayer { get; internal set; }

    private GameObject[,] pieces = new GameObject[8, 8];
    
    public List<Vector2Int> currentMoves = new List<Vector2Int>();
    
    private void Start()
    {
	    StartNewGame();
     
    }

    // switches current player to the other player
    public void NextPlayer()
    {
	    Player tempPlayer = currentPlayer;
	    currentPlayer = otherPlayer;
	    otherPlayer = tempPlayer;
    }
    
    #region Game Initialization
    
    private void StartNewGame()
    {
	    white = new Player("Stu Buchbinder", true);
	    black = new Player("Tom Kent", false);

	    player1.player = white;
	    player2.player = black;

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


    #endregion
    

    // returns the Vector2Int board location for the game object
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

	// does the game object exist in the current player's pieces list
    public virtual bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return currentPlayer.pieces.Contains(piece);
    }

    // is the current player able to castle
    public bool CastleIsActive(GameObject piece)
    {
        return (DoesPieceBelongToCurrentPlayer(piece) && currentPlayer.castleIsActive);
    }
 

    
    public GameObject PieceAtGridPoint(Vector2Int gridPoint)
    {
	    if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
	    {
		    return null;
	    }
	    return pieces[gridPoint.x, gridPoint.y];
    }

    
    public bool FriendlyPieceAtGridPoint(Vector2Int gridPoint)
    {
	    GameObject piece = PieceAtGridPoint(gridPoint);

	    if (piece == null)
		    return false;

	    if (otherPlayer.pieces.Contains(piece))
		    return false;

	    return true;
    }

    public List<Vector2Int> MovesForPiece(GameObject movingPiece)
    {
	    Piece piece = movingPiece.GetComponent<Piece>();
	    Vector2Int gridPoint = GridForPiece(movingPiece);
        
	    currentMoves = piece.MoveLocations(gridPoint);
        
	    // filter out offboard locations
	    currentMoves.RemoveAll(tile => tile.x < 0 || tile.x > 7 || tile.y < 0 || tile.y > 7);
        
	    // filter friendly pieces
	    currentMoves.RemoveAll(loc => FriendlyPieceAtGridPoint(loc));
        
	    if (piece.type == PieceType.Pawn)
	    {
		    // filter out forward gridPoints that have a piece occupying them
		    // this is to prevent moving 2 spaces if the other player has a piece 
		    // in front of the pawn
        
		    Vector2Int gridPoint1 = new Vector2Int(gridPoint.x, gridPoint.y + currentPlayer.forward);
		    Vector2Int gridPoint2 = new Vector2Int(gridPoint.x, gridPoint.y + currentPlayer.forward * 2);
        
		    if (PieceAtGridPoint(gridPoint1))
		    {
			    currentMoves.Remove(gridPoint1);
		    }
        
		    if (PieceAtGridPoint(gridPoint2))
		    {
			    currentMoves.Remove(gridPoint2);
		    }
	    }
        
	    return currentMoves;
    }

    public void Move(GameObject movingPiece, Vector2Int gridPoint)
    {
	    Vector2Int startGridPoint = GridForPiece(movingPiece);
	    pieces[startGridPoint.x, startGridPoint.y] = null;
	    pieces[gridPoint.x, gridPoint.y] = movingPiece;
	    
	    board.MovePiece(movingPiece, gridPoint);

	    movingPiece.GetComponent<Piece>().HasMoved = true;

	    if (movingPiece.GetComponent<King>() != null)
	    {
		    Piece thePiece = movingPiece.GetComponent<Piece>();
		    // check if castle
		    Debug.Log("check castle");
		    if (gridPoint.x == thePiece.originalLocation.x + 2)
		    {
			    Debug.Log("castled right");

			    GameObject rook = PieceAtGridPoint(new Vector2Int(gridPoint.x + 1, gridPoint.y));
			    Move(rook, new Vector2Int(gridPoint.x - 1, gridPoint.y));
		    }
		    else if (gridPoint.x == thePiece.originalLocation.x - 2)
		    {
			    GameObject rook = PieceAtGridPoint(new Vector2Int(gridPoint.x - 2, gridPoint.y));
			    Move(rook, new Vector2Int(gridPoint.x + 1, gridPoint.y));
			    Debug.Log("castled left");
		    }
	    }
    }
    
    #region Board Pass Through Methods

    public void SelectPiece(GameObject selectedPiece)
    {
	    board.SelectPiece(selectedPiece);
    }

    
    public void AttackPieceAtGridPoint(Vector2Int loc)
    {
	    board.AttackPieceAtGridPoint(loc);
    }

    public void HighlightPieceAtGridPoint(Vector2Int loc)
    {
	    board.HighlightPieceAtGridPoint(loc);
    }

    public void DeselectPiece(GameObject movingPiece)
    {
	    board.DeselectPiece(movingPiece);
    }


    #endregion


    public void CapturePiece(Vector2Int gridPoint, Player gameManagerCurrentPlayer)
    {
	    GameObject pieceToCapture = PieceAtGridPoint(gridPoint);
	    string pieceType = pieceToCapture.GetComponent<Piece>().type.ToString();
	    
	    currentPlayer.capturedPieces.Add(pieceType);
	    pieces[gridPoint.x, gridPoint.y] = null;
        
	    if (pieceToCapture.GetComponent<Piece>().type == PieceType.King)
	    {
		    Debug.Log(currentPlayer.name + " wins!");
	    }
	    Destroy(pieceToCapture);
    }


}
