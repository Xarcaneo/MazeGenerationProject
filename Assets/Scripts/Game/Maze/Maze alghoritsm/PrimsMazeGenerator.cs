using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a maze using Prim's algorithm.
/// </summary>
public class PrimsMazeGenerator : MazeGenerator
{
    /// <summary>
    /// List to hold the current frontier tiles.
    /// </summary>
    private List<Vector2Int> frontierTiles = new List<Vector2Int>();

    /// <summary>
    /// List to hold temporary frontier tiles for consideration.
    /// </summary>
    private List<Vector2Int> tempFrontierTiles = new List<Vector2Int>();

    /// <summary>
    /// Overrides the base class method to generate a maze using Prim's algorithm.
    /// </summary>
    public override void GenerateMaze()
    {
        // Call the base class method
        base.GenerateMaze();

        // Initialize the maze generation with a random start position
        MazeTile tile;
        Vector2Int startPos = new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        tile = mazeTiles[startPos.x, startPos.y];

        // Add initial frontier tiles and mark the start position as a passage
        AddFrontierTiles(tile.position.x, tile.position.y, false);
        tile.ChangeState(MazeTile.TileState.Passage);

        // Continue generating the maze while there are frontier tiles
        while (frontierTiles.Count > 0)
        {
            // Select a random frontier tile
            MazeTile frontierTile = GetRandomTile(frontierTiles);
            Vector2Int frontierPosition = new Vector2Int(frontierTile.position.x, frontierTile.position.y);

            // Mark the frontier tile as a passage if within bounds, otherwise mark it as a border tile
            if (IsWithinBounds(frontierPosition))
            {
                frontierTile.ChangeState(MazeTile.TileState.Passage);
            }
            else
            {
                frontierTile.isBorderTile = true;
            }

            // Add surrounding tiles of the frontier tile to the frontier
            AddFrontierTiles(frontierTile.position.x, frontierTile.position.y, frontierTile.isBorderTile);

            // Select a temporary frontier tile and create a passage between it and the current frontier tile
            MazeTile tempFrontier = GetRandomTile(tempFrontierTiles);
            if (tempFrontier != null)
            {
                MakePassageBetween(frontierPosition, new Vector2Int(tempFrontier.position.x, tempFrontier.position.y));
            }

            // Remove the current frontier tile from the list
            frontierTiles.Remove(new Vector2Int(frontierTile.position.x, frontierTile.position.y));
        }

        // Instantiate game objects if necessary (defined in base class)
        base.InstantiateGameObjects();
    }

    /// <summary>
    /// Gets a random tile from the list of frontier tiles.
    /// </summary>
    /// <param name="frontierTiles">The list of frontier tiles.</param>
    /// <returns>A random frontier tile, or null if the list is empty.</returns>
    private MazeTile GetRandomTile(List<Vector2Int> frontierTiles)
    {
        if (frontierTiles.Count == 0) return null;

        int randomIndex = Random.Range(0, frontierTiles.Count);
        Vector2Int randomPosition = frontierTiles[randomIndex];

        return mazeTiles[randomPosition.x, randomPosition.y];
    }

    /// <summary>
    /// Adds frontier tiles around a specified position.
    /// </summary>
    /// <param name="x">The x-coordinate of the position.</param>
    /// <param name="y">The y-coordinate of the position.</param>
    /// <param name="isBorderTile">Whether the tile is a border tile.</param>
    void AddFrontierTiles(int x, int y, bool isBorderTile)
    {
        // Add frontier tiles in all four cardinal directions
        AddFrontierTile(x - 2, y, isBorderTile); // Left
        AddFrontierTile(x + 2, y, isBorderTile); // Right
        AddFrontierTile(x, y - 2, isBorderTile); // Up
        AddFrontierTile(x, y + 2, isBorderTile); // Down
    }

    /// <summary>
    /// Adds a single tile to the frontier.
    /// </summary>
    /// <param name="x">The x-coordinate of the tile.</param>
    /// <param name="y">The y-coordinate of the tile.</param>
    /// <param name="isBorderTile">Whether the tile is a border tile.</param>
    void AddFrontierTile(int x, int y, bool isBorderTile)
    {
        // Check if the tile is within the maze boundaries
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            Vector2Int newFrontierTile = new Vector2Int(x, y);

            if (mazeTiles[x, y].CurrentState == MazeTile.TileState.Wall)
            {
                // Add to frontier if it has no passage neighbors, is not already in the frontier, and is not a border tile
                if (!frontierTiles.Contains(newFrontierTile) && !isBorderTile)
                {
                    frontierTiles.Add(newFrontierTile);
                }
            }
            else
            {
                // If the tile is not a wall, add it to temporary frontier if not already present
                if (!frontierTiles.Contains(newFrontierTile))
                {
                    tempFrontierTiles.Add(newFrontierTile);
                }
            }
        }
    }

    /// <summary>
    /// Creates a passage between two positions.
    /// </summary>
    /// <param name="pos1">The first position.</param>
    /// <param name="pos2">The second position.</param>
    private void MakePassageBetween(Vector2Int pos1, Vector2Int pos2)
    {
        // Calculate the middle position between the two tiles
        int midX = (pos1.x + pos2.x) / 2;
        int midY = (pos1.y + pos2.y) / 2;

        // Set the middle tile to a passage if it's within bounds
        if (midX >= 0 && midX < width && midY >= 0 && midY < height)
        {
            mazeTiles[midX, midY].ChangeState(MazeTile.TileState.Passage);
        }

        // Clear the temporary frontier tiles
        tempFrontierTiles.Clear();
    }

    /// <summary>
    /// Clears the maze and resets the frontier lists.
    /// </summary>
    public override void ClearMaze()
    {
        base.ClearMaze();

        frontierTiles.Clear();
        tempFrontierTiles.Clear();
    }
}
