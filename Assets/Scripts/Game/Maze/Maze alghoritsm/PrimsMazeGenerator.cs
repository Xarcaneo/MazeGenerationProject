using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsMazeGenerator : MazeGenerator
{
    // List to hold the current frontier tiles
    private List<Vector2Int> frontierTiles = new List<Vector2Int>();

    // List to hold temporary frontier tiles for consideration
    private List<Vector2Int> tempFrontierTiles = new List<Vector2Int>();

    public override void GenerateMaze()
    {
        base.GenerateMaze();

        MazeTile tile;

        Vector2Int startPos = new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        tile = mazeTiles[startPos.x, startPos.y];
        AddFrontierTiles(tile.position.x, tile.position.y, false);
        tile.ChangeState(MazeTile.TileState.Passage);

        // While there are tiles in the frontier list
        while (frontierTiles.Count > 0)
        {
            MazeTile frontierTile = GetRandomTile(frontierTiles);
            Vector2Int frontierPosition = new Vector2Int(frontierTile.position.x, frontierTile.position.y);

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

            MazeTile tempFrontier = GetRandomTile(tempFrontierTiles);

            if (tempFrontier != null)
            {
                MakePassageBetween(frontierPosition, new Vector2Int(tempFrontier.position.x, tempFrontier.position.y));
            }

            frontierTiles.Remove(new Vector2Int(frontierTile.position.x, frontierTile.position.y));
        }


        InstantiateGameObjects();
    }

    // Overloaded method to get a random tile from a List<Vector2Int>
    private MazeTile GetRandomTile(List<Vector2Int> frontierTiles)
    {
        if (frontierTiles.Count == 0) return null;

        int randomIndex = Random.Range(0, frontierTiles.Count);
        Vector2Int randomPosition = frontierTiles[randomIndex];

        return mazeTiles[randomPosition.x, randomPosition.y];
    }

    // Method to add frontier tiles around a specified position
    void AddFrontierTiles(int x, int y, bool isBorderTile)
    {
        // Add frontier tiles in all four cardinal directions
        AddFrontierTile(x - 2, y, isBorderTile); // Left
        AddFrontierTile(x + 2, y, isBorderTile); // Right
        AddFrontierTile(x, y - 2, isBorderTile); // Up
        AddFrontierTile(x, y + 2, isBorderTile); // Down
    }

    // Method to add a single tile to the frontier
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

    // Method to create a passage between two positions
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

        tempFrontierTiles.Clear();
    }

    public override void ClearMaze()
    {
        base.ClearMaze();

        frontierTiles.Clear();
        tempFrontierTiles.Clear();
    }
}
