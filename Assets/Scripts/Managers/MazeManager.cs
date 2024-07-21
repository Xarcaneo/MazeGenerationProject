using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeManager : MonoBehaviour
{
    [SerializeField, Tooltip("Maze Generator Prefab")]
    private GameObject mazeGeneratorPrefab;

    private GameObject mazeGeneratorInstance;

    private void Start()
    {
        InstantiateMazeGenerator();

        LevelManager.Instance.NewLevel += GenerateNewMaze;
    }

    private void OnDestroy() => LevelManager.Instance.NewLevel -= GenerateNewMaze;

    private void InstantiateMazeGenerator()
    {
        if (mazeGeneratorPrefab == null)
        {
            Debug.LogError("MazeGenerator prefab is not assigned.");
            return;
        }

        mazeGeneratorInstance = Instantiate(mazeGeneratorPrefab, Vector3.zero, Quaternion.identity);
    }

    // Method to generate a new maze
    private void GenerateNewMaze()
    {
        mazeGeneratorInstance.GetComponent<MazeGenerator>().ClearMaze();
        mazeGeneratorInstance.GetComponent<MazeGenerator>().GenerateMaze();
        LevelManager.Instance.ResetLevelVariables();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
