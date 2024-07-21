using UnityEngine;

public class AlgorithmButtonHandler : MonoBehaviour
{
    // Method to change the maze algorithm in SettingsManager
    public void ChangeAlgorithm(int direction)
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.ChangeAlgorithm(direction);
        }
        else
        {
            Debug.LogError("SettingsManager instance not found.");
        }
    }
}
