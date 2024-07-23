using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a maze using the Depth-First Search (DFS) algorithm.
/// </summary>
public class DFSMazeGenerator : MazeGenerator
{
    /// <summary>
    /// Overrides the base class method to generate a maze using DFS.
    /// </summary>
    public override void GenerateMaze()
    {
        // Call the base class method
        base.GenerateMaze();

        // Stack to store the cells for DFS
        Stack<Vector2Int> stack = new Stack<Vector2Int>();

        // Choose a random start position within the maze boundaries
        Vector2Int startPos = new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        stack.Push(startPos);

        // Mark the start position as a passage
        mazeTiles[startPos.x, startPos.y].ChangeState(MazeTile.TileState.Passage);

        // Perform DFS to carve out passages
        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                // Push the current cell back to the stack
                stack.Push(current);

                // Choose a random unvisited neighbor
                Vector2Int chosenNeighbor = neighbors[Random.Range(0, neighbors.Count)];

                if (IsWithinBounds(chosenNeighbor))
                    stack.Push(chosenNeighbor);

                // Remove the wall between the current cell and the chosen neighbor
                Vector2Int wall = new Vector2Int((current.x + chosenNeighbor.x) / 2, (current.y + chosenNeighbor.y) / 2);
                mazeTiles[wall.x, wall.y].ChangeState(MazeTile.TileState.Passage);

                // Mark the chosen neighbor as a passage
                mazeTiles[chosenNeighbor.x, chosenNeighbor.y].ChangeState(MazeTile.TileState.Passage);
            }
        }

        // Set the boundary tiles to be walls
        SetBoundaryTilesToWall();

        // Instantiate game objects if necessary (defined in base class)
        base.InstantiateGameObjects();
    }

    /// <summary>
    /// Gets the unvisited neighbors of a given cell.
    /// </summary>
    /// <param name="cell">The cell to find unvisited neighbors for.</param>
    /// <returns>A list of unvisited neighbor positions.</returns>
    private List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        // Check each of the four directions for unvisited neighbors
        AddIfUnvisited(neighbors, cell.x + 2, cell.y);
        AddIfUnvisited(neighbors, cell.x - 2, cell.y);
        AddIfUnvisited(neighbors, cell.x, cell.y + 2);
        AddIfUnvisited(neighbors, cell.x, cell.y - 2);

        return neighbors;
    }

    /// <summary>
    /// Adds a cell to the list of neighbors if it is unvisited.
    /// </summary>
    /// <param name="neighbors">The list of neighbors to add to.</param>
    /// <param name="x">The x-coordinate of the cell to check.</param>
    /// <param name="y">The y-coordinate of the cell to check.</param>
    private void AddIfUnvisited(List<Vector2Int> neighbors, int x, int y)
    {
        if ((IsWithinBounds(new Vector2Int(x, y)) || IsOnBounds(new Vector2Int(x, y))) && mazeTiles[x, y].CurrentState == MazeTile.TileState.Wall)
        {
            neighbors.Add(new Vector2Int(x, y));
        }
    }

    /// <summary>
    /// Checks if a position is on the maze bounds.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>True if the position is on the bounds, false otherwise.</returns>
    protected bool IsOnBounds(Vector2Int position)
    {
        return position.x == 0 || position.x == width - 1 || position.y == 0 || position.y == height - 1;
    }

    /// <summary>
    /// Sets the boundary tiles of the maze to be walls.
    /// </summary>
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
