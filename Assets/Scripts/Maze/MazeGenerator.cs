using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // The width of the maze grid
    [Tooltip("Width of the maze grid.")]
    [SerializeField] protected int width = 20;

    // The height of the maze grid
    [Tooltip("Height of the maze grid.")]
    [SerializeField] protected int height = 20;

    // Prefab for the maze tiles
    [Tooltip("Prefab for the maze tiles.")]
    [SerializeField] private MazeTile tilePrefab;

    // Parent transform to hold all the maze tiles
    [Tooltip("Parent transform to hold all the maze tiles.")]
    [SerializeField] private Transform parentTransform;

    // 2D array to hold references to all the maze tiles
    protected MazeTile[,] mazeTiles;

    private void Start()
    {
        GenerateGrid();
        GenerateMaze();
    }

    // Method to generate a grid of maze tiles
    private void GenerateGrid()
    {
        // Initialize the maze tiles array with the specified width and height
        mazeTiles = new MazeTile[width, height];

        // Get the size of the tile prefab's sprite
        SpriteRenderer tileSpriteRenderer = tilePrefab.GetComponent<SpriteRenderer>();
        Vector2 tileSize = tileSpriteRenderer.bounds.size;

        // Create a grid of tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate the position for each tile based on its size
                Vector3 position = new Vector3(x * tileSize.x, y * tileSize.y, 0);

                MazeTile tileObject = Instantiate(tilePrefab, position, Quaternion.identity, parentTransform);

                // Set the tile's position in the grid
                tileObject.position = new Vector2Int(x, y);

                // Store the tile in the maze tiles array
                mazeTiles[x, y] = tileObject;
            }
        }
    }

    // Virtual method to generate the maze
    virtual public void GenerateMaze() { }

    // Virtual method to clear the maze
    virtual public void ClearMaze()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                mazeTiles[x, y].ChangeState(MazeTile.TileState.Wall);
            }
        }
    }  
}
