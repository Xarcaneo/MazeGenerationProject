using System.Collections;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public enum MazeAlgorithm
    {
        Prim
    }

    public MazeAlgorithm SelectedMazeAlgorithm = MazeAlgorithm.Prim;

    public static SettingsManager Instance { get; private set; }

    public int Width { get; set; } = 20;
    public int Height { get; set; } = 20;
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
}
