using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGenerator : MonoBehaviour
{
    // Prefab for the maze tiles
    [Tooltip("Prefab for the maze tiles.")]
    [SerializeField] private MazeTile tilePrefab;

    // Parent transform to hold all the maze tiles
    [Tooltip("Parent transform to hold all the maze tiles.")]
    [SerializeField] private Transform parentTransform;

    // Prefab for the player
    [Tooltip("Prefab for the player.")]
    [SerializeField] private GameObject playerPrefab;

    // Prefab for the player
    [Tooltip("Prefab for the score pickup.")]
    [SerializeField] private GameObject scorePrefab;

    protected int width = 20;
    protected int height = 20;

    // 2D array to hold references to all the maze tiles
    protected MazeTile[,] mazeTiles;

    protected ObjectsSpawner m_objectsSpawner;

    private void Start()
    {

        // Initialize PlayerSpawner
        if (m_objectsSpawner == null)
        {
            m_objectsSpawner = gameObject.AddComponent<ObjectsSpawner>();
        }

        if (SettingsManager.Instance)
        {
            width = SettingsManager.Instance.Width;
            height = SettingsManager.Instance.Height;
        }

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
    virtual public void GenerateMaze() 
    {
        m_objectsSpawner.ClearSpawnedObjects();
        m_objectsSpawner.ClearOccupiedTiles();
    }

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

    // Method to check if a position is within the maze bounds
    protected bool IsWithinBounds(Vector2Int position)
    {
        return position.x > 0 && position.x < width - 1 && position.y > 0 && position.y < height - 1;
    }

    protected void InstantiateGameObjects()
    {
        //Spawn Player
        m_objectsSpawner.SpawnObjects(width, height, mazeTiles, playerPrefab, 1);

        //Spawn score pickups
        m_objectsSpawner.SpawnObjects(width, height, mazeTiles, scorePrefab, LevelManager.Instance.maxScoreToWin);
    }
}
