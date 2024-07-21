using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeManager : MonoBehaviour
{
    [SerializeField, Tooltip("Prim's Algorithm Maze Generator Prefab")]
    private GameObject primMazeGeneratorPrefab;

    [SerializeField, Tooltip("DFS Algorithm Maze Generator Prefab")]
    private GameObject dfsMazeGeneratorPrefab;

    private GameObject mazeGeneratorInstance;

    private void Start()
    {
        InstantiateMazeGenerator();

        LevelManager.Instance.NewLevel += GenerateNewMaze;
    }

    private void OnDestroy() => LevelManager.Instance.NewLevel -= GenerateNewMaze;

    private void InstantiateMazeGenerator()
    {
        if (SettingsManager.Instance == null)
        {
            Debug.LogError("SettingsManager instance is not found.");
            return;
        }

        GameObject selectedPrefab = null;

        switch (SettingsManager.Instance.SelectedMazeAlgorithm)
        {
            case SettingsManager.MazeAlgorithm.Prim:
                selectedPrefab = primMazeGeneratorPrefab;
                break;
            case SettingsManager.MazeAlgorithm.DFS:
                selectedPrefab = dfsMazeGeneratorPrefab;
                break;
                // Add more cases for other algorithms
        }

        if (selectedPrefab == null)
        {
            Debug.LogError("Selected maze generator prefab is not assigned.");
            return;
        }

        mazeGeneratorInstance = Instantiate(selectedPrefab, Vector3.zero, Quaternion.identity);
    }

    // Method to generate a new maze
    private void GenerateNewMaze()
    {
        if (mazeGeneratorInstance != null)
        {
            mazeGeneratorInstance.GetComponent<MazeGenerator>().ClearMaze();
            mazeGeneratorInstance.GetComponent<MazeGenerator>().GenerateMaze();
            LevelManager.Instance.ResetLevelVariables();
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
