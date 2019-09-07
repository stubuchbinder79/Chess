using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public List<GameObject> pieces;
    public List<string> capturedPieces;

    public int forward;
    public bool castleIsActive;

    public string name;

    public Player(string name, bool positiveZMovement)
    {
        this.name = name;
        this.forward = (positiveZMovement) ? 1 : -1;

        this.pieces = new List<GameObject>();
        this.capturedPieces = new List<string>();

        this.castleIsActive = true;
    }
}
