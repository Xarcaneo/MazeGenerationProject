using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] protected int width = 20;
    [SerializeField] protected int height = 20;
    [SerializeField] private MazeTile tilePrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] protected bool debugMode = false;
    [SerializeField] protected float mazeGenerationDelay = 0.0f;

    protected MazeTile[,] mazeTiles;

    private void Start()
    {
        GenerateGrid();
        StartCoroutine(GenerateMaze());
    }

    private void GenerateGrid()
    {
        mazeTiles = new MazeTile[width, height];

        // Get the size of the tile prefab's sprite
        SpriteRenderer tileSpriteRenderer = tilePrefab.GetComponent<SpriteRenderer>();
        Vector2 tileSize = tileSpriteRenderer.bounds.size;

        // Create a grid of tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * tileSize.x, y * tileSize.y, 0);
                MazeTile tileObject = Instantiate(tilePrefab, position, Quaternion.identity, parentTransform);
                tileObject.position = new Vector2Int(x,y);

                mazeTiles[x, y] = tileObject;
            }
        }
    }

    virtual protected IEnumerator GenerateMaze()
    {
        yield return new WaitForSeconds(mazeGenerationDelay);
    }
}
