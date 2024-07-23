using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a tile in the maze, which can be a wall or a passage.
/// </summary>
public class MazeTile : MonoBehaviour
{
    /// <summary>
    /// Enum representing the state of the tile: Wall or Passage.
    /// </summary>
    public enum TileState { Wall, Passage }

    /// <summary>
    /// The current state of the tile.
    /// </summary>
    public TileState CurrentState;

    /// <summary>
    /// Sprite to be used for wall tiles.
    /// </summary>
    [Tooltip("Sprite for wall tiles.")]
    [SerializeField] private Sprite wallSprite;

    /// <summary>
    /// Sprite to be used for passage tiles.
    /// </summary>
    [Tooltip("Sprite for passage tiles.")]
    [SerializeField] private Sprite passageSprite;

    /// <summary>
    /// The position of the tile in the maze.
    /// </summary>
    public Vector2Int position;

    /// <summary>
    /// Flag indicating if the tile is a border tile.
    /// </summary>
    public bool isBorderTile;

    /// <summary>
    /// Initializes the tile as a wall and sets the border tile flag to false.
    /// </summary>
    private void Start()
    {
        // Initialize the tile state and border flag
        CurrentState = TileState.Wall;
        isBorderTile = false;
    }

    /// <summary>
    /// Changes the state of the tile to the specified new state.
    /// </summary>
    /// <param name="newState">The new state to change the tile to.</param>
    public void ChangeState(TileState newState)
    {
        // Update the current state of the tile
        CurrentState = newState;

        // Change the sprite and collider based on the new state
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
