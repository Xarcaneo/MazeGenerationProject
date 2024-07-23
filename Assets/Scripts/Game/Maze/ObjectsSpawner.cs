using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the spawning of objects within the maze, ensuring they are placed on passage tiles and not overlapping.
/// </summary>
public class ObjectsSpawner : MonoBehaviour
{
    /// <summary>
    /// List to track occupied tiles.
    /// </summary>
    private List<Vector2Int> occupiedTiles = new List<Vector2Int>();

    /// <summary>
    /// List to keep references to spawned objects.
    /// </summary>
    private List<GameObject> spawnedObjects = new List<GameObject>();

    /// <summary>
    /// Spawns a specific number of objects at random passage tiles in the maze.
    /// </summary>
    /// <param name="width">The width of the maze.</param>
    /// <param name="height">The height of the maze.</param>
    /// <param name="mazeTiles">The 2D array of maze tiles.</param>
    /// <param name="prefab">The prefab to spawn.</param>
    /// <param name="numberOfObjects">The number of objects to spawn.</param>
    public void SpawnObjects(int width, int height, MazeTile[,] mazeTiles, GameObject prefab, int numberOfObjects)
    {
        List<MazeTile> passageTiles = new List<MazeTile>();

        // Find all passage tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (mazeTiles[x, y].CurrentState == MazeTile.TileState.Passage)
                {
                    passageTiles.Add(mazeTiles[x, y]);
                }
            }
        }

        if (passageTiles.Count > 0)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                // Choose a random passage tile that is not already occupied
                MazeTile randomTile = null;
                Vector2Int randomPosition;
                do
                {
                    randomTile = passageTiles[Random.Range(0, passageTiles.Count)];
                    randomPosition = randomTile.position;
                } while (occupiedTiles.Contains(randomPosition));

                // Mark the tile as occupied
                occupiedTiles.Add(randomPosition);

                // Calculate the correct position for instantiation considering the prefab's collider offset
                BoxCollider2D prefabBoxCollider = prefab.GetComponent<BoxCollider2D>();
                Vector3 instantiatePos = new Vector3(randomTile.transform.position.x - prefabBoxCollider.offset.x, randomTile.transform.position.y - prefabBoxCollider.offset.y);

                // Instantiate the object at the position of the random passage tile
                GameObject spawnedObject = Instantiate(prefab, instantiatePos, Quaternion.identity);
                spawnedObjects.Add(spawnedObject);
            }
        }
        else
        {
            Debug.LogWarning("No passage tiles found to spawn objects.");
        }
    }

    /// <summary>
    /// Clears the list of occupied tiles.
    /// </summary>
    public void ClearOccupiedTiles()
    {
        occupiedTiles.Clear();
    }

    /// <summary>
    /// Destroys all spawned objects and clears the list.
    /// </summary>
    public void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}
