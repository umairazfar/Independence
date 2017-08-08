using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {

    bool occupied = false;

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetOccupied(bool occ)
    {
        occupied = occ;
    }
}
