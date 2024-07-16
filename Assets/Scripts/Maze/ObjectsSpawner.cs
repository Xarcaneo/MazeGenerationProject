using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    // List to track occupied tiles
    private List<Vector2Int> occupiedTiles = new List<Vector2Int>();

    // List to keep references to spawned objects
    private List<GameObject> spawnedObjects = new List<GameObject>();

    // Method to spawn a specific number of objects at random passage tiles
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

                // Instantiate the object at the position of the random passage tile
                GameObject spawnedObject = Instantiate(prefab, randomTile.transform.position, Quaternion.identity);
                spawnedObjects.Add(spawnedObject);
            }
        }
        else
        {
            Debug.LogWarning("No passage tiles found to spawn objects.");
        }
    }

    // Method to clear the occupied tiles list
    public void ClearOccupiedTiles()
    {
        occupiedTiles.Clear();
    }

    // Method to clear all spawned objects
    public void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}
