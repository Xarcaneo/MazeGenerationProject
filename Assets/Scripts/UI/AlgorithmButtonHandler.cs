using UnityEngine;

/// <summary>
/// Handles the button interaction to change the maze generation algorithm in the SettingsManager.
/// </summary>
public class AlgorithmButtonHandler : MonoBehaviour
{
    /// <summary>
    /// Changes the maze algorithm in the SettingsManager based on the provided direction.
    /// </summary>
    /// <param name="direction">The direction to change the algorithm (-1 for previous, 1 for next).</param>
    public void ChangeAlgorithm(int direction)
    {
        if (SettingsManager.Instance != null)
        {
            // Change the maze algorithm in the SettingsManager
            SettingsManager.Instance.ChangeAlgorithm(direction);
        }
        else
        {
            Debug.LogError("SettingsManager instance not found.");
        }
    }
}
