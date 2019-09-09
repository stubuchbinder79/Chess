using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();

        int forwardDirection = GameManager.Instance.currentPlayer.forward;

        Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection * 2);
        Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection * 2);
        Vector2Int leftForward = new Vector2Int(gridPoint.x - 2, gridPoint.y + forwardDirection);
        Vector2Int rightForward = new Vector2Int(gridPoint.x + 2, gridPoint.y + forwardDirection);
        Vector2Int leftBackward = new Vector2Int(gridPoint.x - 2, gridPoint.y - forwardDirection);
        Vector2Int rightBackward = new Vector2Int(gridPoint.x + 2, gridPoint.y - forwardDirection);
        Vector2Int backLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y - (forwardDirection * 2));
        Vector2Int backRight = new Vector2Int(gridPoint.x + 1, gridPoint.y - (forwardDirection * 2));

        locations.Add(forwardLeft);
        locations.Add(forwardRight);
        locations.Add(leftForward);
        locations.Add(rightForward);
        locations.Add(leftBackward);
        locations.Add(rightBackward);
        locations.Add(backLeft);
        locations.Add(backRight);

        return locations;
    }
}


