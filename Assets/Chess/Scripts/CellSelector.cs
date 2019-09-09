using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(GraphicRaycaster))]
[RequireComponent(typeof(MoveSelector))]
public class CellSelector : MonoBehaviour, ISelector
{
    private GameManager gameManager;
    private MoveSelector moveSelector;

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

        moveSelector = (moveSelector) ?? GetComponent<MoveSelector>();
        graphicRaycaster = GetComponent<GraphicRaycaster>();
    }

    public void EnterState(GameObject go)
    {
        enabled = true;
    }

    public void ExitState(GameObject go)
    {
        enabled = false;
        moveSelector.EnterState(go);
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

            if ( gameManager.FriendlyPieceAtGridPoint(selectedCell.boardPosition))
            {
                GameObject movingPiece = gameManager.PieceAtGridPoint(selectedCell.boardPosition);
                gameManager.SelectPiece(movingPiece);
                
                ExitState(movingPiece);
            }
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
