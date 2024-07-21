using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSMazeGenerator : MazeGenerator
{
    public override void GenerateMaze()
    {
        base.GenerateMaze();

        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int startPos = new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        stack.Push(startPos);

        mazeTiles[startPos.x, startPos.y].ChangeState(MazeTile.TileState.Passage);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                stack.Push(current); // Push current back to the stack

                Vector2Int chosenNeighbor = neighbors[Random.Range(0, neighbors.Count)];

                if (IsWithinBounds(chosenNeighbor))
                    stack.Push(chosenNeighbor);

                // Remove the wall between current and chosenNeighbor
                Vector2Int wall = new Vector2Int((current.x + chosenNeighbor.x) / 2, (current.y + chosenNeighbor.y) / 2);

                mazeTiles[wall.x, wall.y].ChangeState(MazeTile.TileState.Passage);

                mazeTiles[chosenNeighbor.x, chosenNeighbor.y].ChangeState(MazeTile.TileState.Passage);
            }
        }

        SetBoundaryTilesToWall();
        InstantiateGameObjects();
    }

    private List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        // Check each of the 4 directions for unvisited neighbors
        AddIfUnvisited(neighbors, cell.x + 2, cell.y);
        AddIfUnvisited(neighbors, cell.x - 2, cell.y);
        AddIfUnvisited(neighbors, cell.x, cell.y + 2);
        AddIfUnvisited(neighbors, cell.x, cell.y - 2);

        return neighbors;
    }

    private void AddIfUnvisited(List<Vector2Int> neighbors, int x, int y)
    {
        if ((IsWithinBounds(new Vector2Int(x, y)) || IsOnBounds(new Vector2Int(x, y))) && mazeTiles[x, y].CurrentState == MazeTile.TileState.Wall)
        {
            neighbors.Add(new Vector2Int(x, y));
        }
    }

    // Method to check if a position is on the maze bounds
    protected bool IsOnBounds(Vector2Int position)
    {
        return position.x == 0 || position.x == width - 1 || position.y == 0 || position.y == height - 1;
    }

    private void SetBoundaryTilesToWall()
    {
        for (int x = 0; x < width; x++)
        {
            mazeTiles[x, 0].ChangeState(MazeTile.TileState.Wall);
            mazeTiles[x, height - 1].ChangeState(MazeTile.TileState.Wall);
        }

        for (int y = 0; y < height; y++)
        {
            mazeTiles[0, y].ChangeState(MazeTile.TileState.Wall);
            mazeTiles[width - 1, y].ChangeState(MazeTile.TileState.Wall);
        }
    }
}
