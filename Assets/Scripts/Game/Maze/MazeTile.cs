using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeTile : MonoBehaviour
{
    public enum TileState { Wall, Passage }
    public TileState CurrentState;

    [Tooltip("Sprite for wall tiles.")]
    [SerializeField] private Sprite wallSprite;

    [Tooltip("Sprite for passage tiles.")]
    [SerializeField] private Sprite passageSprite;

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
            this.GetComponent<SpriteRenderer>().sprite = passageSprite;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = wallSprite;
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
