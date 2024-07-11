using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsMazeGenerator : MazeGenerator
{
    private List<Vector2Int> frontierTiles = new List<Vector2Int>();
    private List<Vector2Int> tempFrontierTiles = new List<Vector2Int>();

    protected override IEnumerator GenerateMaze()
    {
        MazeTile tile = GetRandomTile(mazeTiles);
        tile.ChangeState(MazeTile.TileState.Passage);
        AddFrontierTiles(tile.position.x, tile.position.y);

        while(frontierTiles.Count > 0)
        {
            MazeTile frontierTile = GetRandomTile(frontierTiles);
            if (frontierTile.position.x > 0 && frontierTile.position.x < width - 1 && frontierTile.position.y > 0 && frontierTile.position.y < height - 1)
            {
                frontierTile.ChangeState(MazeTile.TileState.Passage);
                AddFrontierTiles(frontierTile.position.x, frontierTile.position.y);

                MazeTile tempFrontier = GetRandomTile(tempFrontierTiles);
                MakePassageBetween(new Vector2Int(frontierTile.position.x, frontierTile.position.y), new Vector2Int(tempFrontier.position.x, tempFrontier.position.y));
            }

            frontierTiles.Remove(new Vector2Int(frontierTile.position.x, frontierTile.position.y));

            yield return new WaitForSeconds(mazeGenerationDelay);
        }
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

    void AddFrontierTiles(int x, int y)
    {
        AddFrontierTile(x - 2, y);
        AddFrontierTile(x + 2, y);
        AddFrontierTile(x, y - 2);
        AddFrontierTile(x, y + 2);
    }

    void AddFrontierTile(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            Vector2Int newFrontierTile = new Vector2Int(x, y);

            if (mazeTiles[x, y].CurrentState == MazeTile.TileState.Wall)
            {
                if (!frontierTiles.Contains(newFrontierTile))
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
        // Calculate the middle position
        int midX = (pos1.x + pos2.x) / 2;
        int midY = (pos1.y + pos2.y) / 2;

        // Set the middle tile to Passage
        if (midX >= 0 && midX < width && midY >= 0 && midY < height)
        {
            mazeTiles[midX, midY].ChangeState(MazeTile.TileState.Passage);
        }

        tempFrontierTiles.Clear();
    }
}
