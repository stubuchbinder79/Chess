using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
	public Transform pieces;
	public Transform cells;

    [Tooltip("the tile prefab")]
    public GameObject cellPrefab;

    public float cellSize
    {
        get
        {
            RectTransform rect = GetComponent<RectTransform>();
            return rect.sizeDelta.x / 8;
        }
    }

    // flip the board if networked game and we're black player
    private bool flip = false;

    private Cell[,] allCells = new Cell[8, 8];
 

    private void Start()
    {
        CreateBoardCells();
    }

    private void CreateBoardCells()
    {

        // spawn cells
        for (int y = 0; y  < 8; y ++)
        {
            for (int x = 0; x < 8; x++)
            {
                int xPos = (!flip) ? x : 7 - x;
                int yPos = (!flip) ? y : 7 - y;

                // create a new cell and parent to this GO
                GameObject cell = Instantiate(cellPrefab, transform.position, Quaternion.identity, cells);
                cell.name = "Cell_" + x + "_" + y;

                // position
                RectTransform rect = cell.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(cellSize, cellSize);
                rect.anchoredPosition = new Vector2(xPos * cellSize, yPos * cellSize);

                allCells[x, y] = cell.GetComponent<Cell>();
                allCells[x, y].Setup(new Vector2Int(x, y), this);

               
            }
        }

        // color
        for (int x = 0; x < 8; x += 2)
        {
            for (int y = 0; y < 8; y++)
            {
                int modular = (!flip) ? 0 : 1;
                int offset = (y % 2 != modular) ? 0 : 1;
                int finalX = x + offset;

                allCells[finalX, y].GetComponent<Image>().color = new Color(255, 255, 255);
            }
        }

    }


	public GameObject AddPiece(GameObject piece, int col, int row)
	{
		GameObject go = Instantiate(piece, transform.position, Quaternion.identity, pieces);

		// position
		RectTransform rect = go.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(cellSize, cellSize);

		// flip board if black and networked game
		if (flip)
		{
			col = 7 - col;
			row = 7 - row;
		}


		rect.anchoredPosition = new Vector2((col * cellSize), (row * cellSize));

		return go;
	}

	public void SelectPiece(GameObject selectedPiece)
	{
		DeselectPiece(null);
		Vector2Int gridPoint = GameManager.Instance.GridForPiece(selectedPiece);
		allCells[gridPoint.x, gridPoint.y].highlighted = true;
	}

	public void AttackPieceAtGridPoint(Vector2Int gridPoint)
	{
		allCells[gridPoint.x, gridPoint.y].attacking = true;
	}

	public void HighlightPieceAtGridPoint(Vector2Int gridPoint)
	{
		allCells[gridPoint.x, gridPoint.y].highlighted = true;
	}

	// removes the highlights from all cells
	public void DeselectPiece([CanBeNull] GameObject movingPiece)
	{
		foreach (Cell cell in allCells)
		{
			cell.attacking = false;
			cell.highlighted = false;
		}
	}

	public void MovePiece(GameObject movingPiece, Vector2Int gridPoint)
	{
		 int col = (!flip) ? gridPoint.x : 7 - gridPoint.x;
		 int row = (!flip) ? gridPoint.y : 7 - gridPoint.y;
        
		 Vector2Int point = new Vector2Int(col, row);
		 Vector3 gridPos = point.Vector3FromVector2Int();
     
		 movingPiece.GetComponent<RectTransform>().anchoredPosition = new Vector2(col * cellSize, row *cellSize);
	}
}
