using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
	{
		List<Vector2Int> locations = new List<Vector2Int>();

		int forwardDirection = GameManager.Instance.currentPlayer.forward;
		Vector2Int forward = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);

		if (GameManager.Instance.PieceAtGridPoint(forward) == false)
			locations.Add(forward);

		if (!HasMoved)
		{
			Vector2Int forward2 = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection * 2);
			if (GameManager.Instance.PieceAtGridPoint(forward2) == false)
				locations.Add(forward2);
		}


		Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);

		if (GameManager.Instance.PieceAtGridPoint(forwardRight))
			locations.Add(forwardRight);

		Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);

		if (GameManager.Instance.PieceAtGridPoint(forwardLeft))
			locations.Add(forwardLeft);

		return locations;
	}

}
