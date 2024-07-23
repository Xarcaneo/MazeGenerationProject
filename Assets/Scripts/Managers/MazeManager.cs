using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the instantiation and generation of mazes using different algorithms.
/// </summary>
public class MazeManager : MonoBehaviour
{
    /// <summary>
    /// Prefab for the Prim's Algorithm Maze Generator.
    /// </summary>
    [SerializeField, Tooltip("Prim's Algorithm Maze Generator Prefab")]
    private GameObject primMazeGeneratorPrefab;

    /// <summary>
    /// Prefab for the DFS Algorithm Maze Generator.
    /// </summary>
    [SerializeField, Tooltip("DFS Algorithm Maze Generator Prefab")]
    private GameObject dfsMazeGeneratorPrefab;

    /// <summary>
    /// Instance of the currently active maze generator.
    /// </summary>
    private GameObject mazeGeneratorInstance;

    /// <summary>
    /// Initializes the MazeManager and subscribes to the NewLevel event.
    /// </summary>
    private void Start()
    {
        InstantiateMazeGenerator();

        // Subscribe to the NewLevel event from the LevelManager
        LevelManager.Instance.NewLevel += GenerateNewMaze;
    }

    /// <summary>
    /// Unsubscribes from the NewLevel event when the MazeManager is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        LevelManager.Instance.NewLevel -= GenerateNewMaze;
    }

    /// <summary>
    /// Instantiates the appropriate maze generator based on the selected algorithm in the settings.
    /// </summary>
    private void InstantiateMazeGenerator()
    {
        if (SettingsManager.Instance == null)
        {
            Debug.LogError("SettingsManager instance is not found.");
            return;
        }

        GameObject selectedPrefab = null;

        // Select the maze generator prefab based on the selected algorithm
        switch (SettingsManager.Instance.SelectedMazeAlgorithm)
        {
            case SettingsManager.MazeAlgorithm.Prim:
                selectedPrefab = primMazeGeneratorPrefab;
                break;
            case SettingsManager.MazeAlgorithm.DFS:
                selectedPrefab = dfsMazeGeneratorPrefab;
                break;
                // Add more cases for other algorithms if needed
        }

        if (selectedPrefab == null)
        {
            Debug.LogError("Selected maze generator prefab is not assigned.");
            return;
        }

        // Instantiate the selected maze generator prefab
        mazeGeneratorInstance = Instantiate(selectedPrefab, Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Generates a new maze by clearing the current maze and generating a new one.
    /// </summary>
    private void GenerateNewMaze()
    {
        if (mazeGeneratorInstance != null)
        {
            MazeGenerator mazeGenerator = mazeGeneratorInstance.GetComponent<MazeGenerator>();
            mazeGenerator.ClearMaze();
            mazeGenerator.GenerateMaze();
            LevelManager.Instance.ResetLevelVariables();
        }
    }

    /// <summary>
    /// Returns to the main menu by clearing the maze and resetting level variables.
    /// </summary>
    public void BackToMenu()
    {
        if (mazeGeneratorInstance != null)
        {
            mazeGeneratorInstance.GetComponent<MazeGenerator>().ClearMaze();
        }
        LevelManager.Instance.ResetLevelVariables();
        SceneManager.LoadScene("MainMenuScene");
    }
}
