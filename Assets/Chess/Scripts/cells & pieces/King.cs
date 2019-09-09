using System.Collections.Generic;
using UnityEngine;


public class King : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();

        int forwardDirection = GameManager.Instance.currentPlayer.forward;

        Vector2Int forward = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);
        locations.Add(forward);

        Vector2Int backward = new Vector2Int(gridPoint.x, gridPoint.y - forwardDirection);
        locations.Add(backward);

        Vector2Int left = new Vector2Int(gridPoint.x - 1, gridPoint.y);
        locations.Add(left);

        Vector2Int right = new Vector2Int(gridPoint.x + 1, gridPoint.y);
        locations.Add(right);

        Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);
        locations.Add(forwardLeft);

        Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);
        locations.Add(forwardRight);

        Vector2Int backLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y - forwardDirection);
        locations.Add(backLeft);

        Vector2Int backRight = new Vector2Int(gridPoint.x + 1, gridPoint.y - forwardDirection);
        locations.Add(backRight);

        // add locations for castling
        if (GameManager.Instance.CastleIsActive(gameObject))
        {
            if (!GameManager.Instance.PieceAtGridPoint(new Vector2Int(gridPoint.x + 1, gridPoint.y)))
            {
                Vector2Int castleRight = new Vector2Int(gridPoint.x + 2, gridPoint.y);
                locations.Add(castleRight);
            }

            if (!GameManager.Instance.PieceAtGridPoint(new Vector2Int(gridPoint.x - 1, gridPoint.y)))
            {
                Vector2Int castleLeft = new Vector2Int(gridPoint.x - 2, gridPoint.y);
                locations.Add(castleLeft);
            }

        }

        return locations;
    }
}

