using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CellSelector))]
public class MoveSelector : MonoBehaviour, ISelector
{
    private GameManager gameManager;
    private CellSelector cellSelector;
    private GameObject movingPiece;
    private List<Vector2Int> moveLocations;
    private bool hasMoved = false;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    
    [SerializeField, Tooltip("The EventSystem used to capture UI")]
    private EventSystem eventSystem;


    private void Awake()
    {
        gameManager = GameManager.Instance;
        Assert.IsNotNull(gameManager, "GameManager not found");

        eventSystem = (eventSystem) ?? FindObjectOfType<EventSystem>();
        Assert.IsNotNull(eventSystem, "EventSystem not found.");

        graphicRaycaster = GetComponent<GraphicRaycaster>();
        cellSelector = GetComponent<CellSelector>();
    }

    public void EnterState(GameObject go)
    {
        enabled = true;
        movingPiece = go;

        hasMoved = false;
        moveLocations = gameManager.MovesForPiece(movingPiece);

        foreach (Vector2Int loc in moveLocations)
        {
            if (gameManager.PieceAtGridPoint(loc))
            {
                // attack
                gameManager.AttackPieceAtGridPoint(loc);
            }
            else
            {
                gameManager.HighlightPieceAtGridPoint(loc);

            }
        }

    }
    public void ExitState(GameObject go)
    {
        this.enabled = false;
        gameManager.DeselectPiece(movingPiece);

        movingPiece = null;
        cellSelector.EnterState(null);

        if (hasMoved)
            gameManager.NextPlayer();


    }

    private void Start()
    {
        enabled = false;
    }

    private void Update()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count > 0 && Input.GetMouseButtonDown(0))
        {
            Cell selectedCell = CellFromResults(results);
            Vector2Int gridPoint = selectedCell.boardPosition;

            // check for valid move locations
            if (gameManager.PieceAtGridPoint(gridPoint) == null)
            {
                if (!moveLocations.Contains(gridPoint))
                    return;
                gameManager.Move(movingPiece, gridPoint);
                hasMoved = true;
            } else if (gameManager.currentMoves.Contains(gridPoint) &&
                       !gameManager.FriendlyPieceAtGridPoint(gridPoint))
            {
                // capture opponent piece
                gameManager.CapturePiece(gridPoint, gameManager.currentPlayer);
                gameManager.Move(movingPiece, gridPoint);
                hasMoved = true;
            }
            else
            {
                hasMoved = false;
            }
            
            ExitState(null);
        }
    }

    private Cell CellFromResults(List<RaycastResult> results)
    {
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Cell>())
            {
                return result.gameObject.GetComponent<Cell>();
        
            }
        }
        return null;
    }
}
