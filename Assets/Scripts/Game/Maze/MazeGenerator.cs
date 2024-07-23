using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for generating a maze. 
/// Provides methods to generate a grid and maze, and to clear the maze.
/// </summary>
public abstract class MazeGenerator : MonoBehaviour
{
    /// <summary>
    /// Prefab for the maze tiles.
    /// </summary>
    [Tooltip("Prefab for the maze tiles.")]
    [SerializeField] private MazeTile tilePrefab;

    /// <summary>
    /// Parent transform to hold all the maze tiles.
    /// </summary>
    [Tooltip("Parent transform to hold all the maze tiles.")]
    [SerializeField] private Transform parentTransform;

    /// <summary>
    /// Prefab for the player.
    /// </summary>
    [Tooltip("Prefab for the player.")]
    [SerializeField] private GameObject playerPrefab;

    /// <summary>
    /// Prefab for the score pickup.
    /// </summary>
    [Tooltip("Prefab for the score pickup.")]
    [SerializeField] private GameObject scorePrefab;

    /// <summary>
    /// Width of the maze.
    /// </summary>
    protected int width = 20;

    /// <summary>
    /// Height of the maze.
    /// </summary>
    protected int height = 20;

    /// <summary>
    /// 2D array to hold references to all the maze tiles.
    /// </summary>
    protected MazeTile[,] mazeTiles;

    /// <summary>
    /// Spawner for various objects within the maze.
    /// </summary>
    protected ObjectsSpawner m_objectsSpawner;

    /// <summary>
    /// Initializes the maze generator and starts the maze generation process.
    /// </summary>
    private void Start()
    {
        // Initialize ObjectsSpawner if not already assigned
        if (m_objectsSpawner == null)
        {
            m_objectsSpawner = gameObject.AddComponent<ObjectsSpawner>();
        }

        // Update maze dimensions from settings if available
        if (SettingsManager.Instance)
        {
            width = SettingsManager.Instance.Width;
            height = SettingsManager.Instance.Height;
        }

        // Generate the initial grid and the maze
        GenerateGrid();
        GenerateMaze();
    }

    /// <summary>
    /// Generates a grid of maze tiles.
    /// </summary>
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

                // Instantiate and position the tile
                MazeTile tileObject = Instantiate(tilePrefab, position, Quaternion.identity, parentTransform);

                // Set the tile's position in the grid
                tileObject.position = new Vector2Int(x, y);

                // Store the tile in the maze tiles array
                mazeTiles[x, y] = tileObject;
            }
        }
    }

    /// <summary>
    /// Generates the maze. To be overridden by derived classes.
    /// </summary>
    virtual public void GenerateMaze()
    {
        // Clear previously spawned objects
        m_objectsSpawner.ClearSpawnedObjects();
        m_objectsSpawner.ClearOccupiedTiles();
    }

    /// <summary>
    /// Clears the maze by setting all tiles to wall state.
    /// </summary>
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

    /// <summary>
    /// Checks if a position is within the maze bounds.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>True if the position is within the bounds, false otherwise.</returns>
    protected bool IsWithinBounds(Vector2Int position)
    {
        return position.x > 0 && position.x < width - 1 && position.y > 0 && position.y < height - 1;
    }

    /// <summary>
    /// Instantiates the player and score objects within the maze.
    /// </summary>
    protected void InstantiateGameObjects()
    {
        // Spawn player
        m_objectsSpawner.SpawnObjects(width, height, mazeTiles, playerPrefab, 1);

        // Spawn score pickups
        m_objectsSpawner.SpawnObjects(width, height, mazeTiles, scorePrefab, LevelManager.Instance.maxScoreToWin);
    }
}
