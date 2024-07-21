using System;
using System.Collections;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public enum MazeAlgorithm
    {
        Prim,
        DFS
    }

    public MazeAlgorithm SelectedMazeAlgorithm = MazeAlgorithm.Prim;
    public Action AlgorithmChanged;

    public static SettingsManager Instance { get; private set; }

    public int Width { get; set; } = 10;
    public int Height { get; set; } = 10;
    public bool HasTimeLimit { get; set; } = false;

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

    public void SwitchHasTimeLimitSetting() => HasTimeLimit = !HasTimeLimit;

    // Method to switch maze algorithm with wrapping
    public void ChangeAlgorithm(int direction)
    {
        int newIndex = (int)SelectedMazeAlgorithm + direction;

        if (newIndex < 0)
        {
            newIndex = Enum.GetValues(typeof(MazeAlgorithm)).Length - 1;
        }
        else if (newIndex >= Enum.GetValues(typeof(MazeAlgorithm)).Length)
        {
            newIndex = 0;
        }

        SelectedMazeAlgorithm = (MazeAlgorithm)newIndex;
        AlgorithmChanged?.Invoke();
    }
}
