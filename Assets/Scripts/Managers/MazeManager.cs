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
    }

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
    public void GenerateNewMaze()
    {
        mazeGeneratorInstance.GetComponent<MazeGenerator>().ClearMaze();
        mazeGeneratorInstance.GetComponent<MazeGenerator>().GenerateMaze();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}