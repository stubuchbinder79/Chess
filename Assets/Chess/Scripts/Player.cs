using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public readonly List<GameObject> pieces;
    public List<string> capturedPieces;

    public readonly int forward;
    public readonly bool castleIsActive;

    public readonly string name;

    public Player(string name, bool positiveZMovement)
    {
        this.name = name;
        this.forward = (positiveZMovement) ? 1 : -1;

        this.pieces = new List<GameObject>();
        this.capturedPieces = new List<string>();

        this.castleIsActive = true;
    }
}
