using System.Collections;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to the MazeGenerator object")]
    private MazeGenerator mazeGenerator;

    void Start()
    {
        if (mazeGenerator == null)
        {
            Debug.LogError("MazeGenerator reference is missing.");
            return;
        }
    }

    void Update()
    {
        // Check for "R" key press to regenerate the maze
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateNewMaze();
        }
    }

    // Method to generate a new maze
    public void GenerateNewMaze()
    {
        mazeGenerator.ClearMaze();
        mazeGenerator.GenerateMaze();
    }
}
