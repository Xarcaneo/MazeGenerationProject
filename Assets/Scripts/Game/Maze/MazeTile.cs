using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTile : MonoBehaviour
{
    public enum TileState { Wall, Passage }
    public TileState CurrentState;

    // Position of the tile in the maze
    public Vector2Int position;

    // Flag to indicate if the tile is a border tile
    public bool isBorderTile;

    private void Start()
    {
        CurrentState = TileState.Wall;
        isBorderTile = false;
    }

    // Method to change the state of the tile
    public void ChangeState(TileState newState)
    {
        CurrentState = newState;

        if (CurrentState == TileState.Passage)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
