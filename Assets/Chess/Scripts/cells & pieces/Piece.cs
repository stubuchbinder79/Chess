

using System.Collections.Generic;
using UnityEngine;


public enum PieceType { King, Queen, Bishop, Knight, Rook, Pawn };

public abstract class Piece : MonoBehaviour
{
    public Vector2Int originalLocation;
    public PieceType type;
    public bool HasMoved = false;
    protected Vector2Int[] RookDirections = {new Vector2Int(0,1), new Vector2Int(1, 0),
        new Vector2Int(0, -1), new Vector2Int(-1, 0)};
    protected Vector2Int[] BishopDirections = {new Vector2Int(1,1), new Vector2Int(1, -1),
        new Vector2Int(-1, -1), new Vector2Int(-1, 1)};

    protected Vector2Int[] CastleDirections = { new Vector2Int(-2, 0), new Vector2Int(2, 0) };

    public abstract List<Vector2Int> MoveLocations(Vector2Int gridPoint);
}


