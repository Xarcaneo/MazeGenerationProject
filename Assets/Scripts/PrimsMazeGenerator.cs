using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsMazeGenerator : MazeGenerator
{
    // List to hold the current frontier tiles
    private List<Vector2Int> frontierTiles = new List<Vector2Int>();

    // List to hold temporary frontier tiles for consideration
    private List<Vector2Int> tempFrontierTiles = new List<Vector2Int>();

    // Coroutine to generate the maze using Prim's algorithm
    protected override IEnumerator GenerateMaze()
    {
        MazeTile tile;

        do
        {
            frontierTiles.Clear();
            tile = GetRandomTile(mazeTiles);
            AddFrontierTiles(tile.position.x, tile.position.y, false);
        } while (frontierTiles.Count != 4);

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

            yield return new WaitForSeconds(mazeGenerationDelay);
        }
    }

    // Method to check if a position is within the maze bounds
    private bool IsWithinBounds(Vector2Int position)
    {
        return position.x > 0 && position.x < width - 1 && position.y > 0 && position.y < height - 1;
    }

    // Method to get a random tile from a MazeTile[,] array
    private MazeTile GetRandomTile(MazeTile[,] mazeTiles)
    {
        int width = mazeTiles.GetLength(0);
        int height = mazeTiles.GetLength(1);

        int randomX = Random.Range(0, width);
        int randomY = Random.Range(0, height);

        return mazeTiles[randomX, randomY];
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

                    if (debugMode)
                    {
                        Debug.Log($"Added frontier tile at [{x}, {y}]");
                        mazeTiles[x, y].GetComponent<SpriteRenderer>().color = Color.cyan;
                    }
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
}
