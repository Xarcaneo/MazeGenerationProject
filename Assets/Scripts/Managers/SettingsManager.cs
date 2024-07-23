using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages game settings including maze algorithm selection, dimensions, and time limit options.
/// </summary>
public class SettingsManager : MonoBehaviour
{
    /// <summary>
    /// Enum representing the available maze algorithms.
    /// </summary>
    public enum MazeAlgorithm
    {
        Prim,
        DFS
    }

    /// <summary>
    /// The selected maze algorithm.
    /// </summary>
    public MazeAlgorithm SelectedMazeAlgorithm = MazeAlgorithm.Prim;

    /// <summary>
    /// Event triggered when the maze algorithm is changed.
    /// </summary>
    public Action AlgorithmChanged;

    /// <summary>
    /// Singleton instance of the SettingsManager.
    /// </summary>
    public static SettingsManager Instance { get; private set; }

    /// <summary>
    /// Width of the maze.
    /// </summary>
    public int Width { get; set; } = 10;

    /// <summary>
    /// Height of the maze.
    /// </summary>
    public int Height { get; set; } = 10;

    /// <summary>
    /// Indicates whether the maze has a time limit.
    /// </summary>
    public bool HasTimeLimit { get; set; } = false;

    /// <summary>
    /// Initializes the singleton instance and prevents it from being destroyed on scene load.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Toggles the time limit setting.
    /// </summary>
    public void SwitchHasTimeLimitSetting()
    {
        HasTimeLimit = !HasTimeLimit;
    }

    /// <summary>
    /// Changes the selected maze algorithm with wrapping based on the provided direction.
    /// </summary>
    /// <param name="direction">Direction to change the algorithm (-1 for previous, 1 for next).</param>
    public void ChangeAlgorithm(int direction)
    {
        int newIndex = (int)SelectedMazeAlgorithm + direction;

        // Wrap around if the new index is out of bounds
        if (newIndex < 0)
        {
            newIndex = Enum.GetValues(typeof(MazeAlgorithm)).Length - 1;
        }
        else if (newIndex >= Enum.GetValues(typeof(MazeAlgorithm)).Length)
        {
            newIndex = 0;
        }

        // Update the selected maze algorithm and invoke the AlgorithmChanged event
        SelectedMazeAlgorithm = (MazeAlgorithm)newIndex;
        AlgorithmChanged?.Invoke();
    }
}
