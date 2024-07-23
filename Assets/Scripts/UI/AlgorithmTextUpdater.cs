using TMPro;
using UnityEngine;

/// <summary>
/// Updates the TextMeshPro text object to display the currently selected maze generation algorithm.
/// </summary>
public class AlgorithmTextUpdater : MonoBehaviour
{
    /// <summary>
    /// TextMeshPro text object to display the selected algorithm.
    /// </summary>
    [SerializeField, Tooltip("TextMeshPro text object to display the selected algorithm")]
    private TextMeshProUGUI algorithmText;

    /// <summary>
    /// Subscribes to the AlgorithmChanged event and initializes the text with the current algorithm.
    /// </summary>
    private void Start()
    {
        if (SettingsManager.Instance != null)
        {
            // Subscribe to the AlgorithmChanged event
            SettingsManager.Instance.AlgorithmChanged += OnAlgorithmChanged;
            // Initialize with the current algorithm
            UpdateAlgorithmText();
        }
        else
        {
            Debug.LogError("SettingsManager instance not found.");
        }
    }

    /// <summary>
    /// Unsubscribes from the AlgorithmChanged event to prevent memory leaks.
    /// </summary>
    private void OnDestroy()
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.AlgorithmChanged -= OnAlgorithmChanged;
        }
    }

    /// <summary>
    /// Event handler for the AlgorithmChanged event, updates the displayed algorithm text.
    /// </summary>
    private void OnAlgorithmChanged()
    {
        UpdateAlgorithmText();
    }

    /// <summary>
    /// Updates the TextMeshPro text object to display the currently selected maze algorithm.
    /// </summary>
    private void UpdateAlgorithmText()
    {
        if (algorithmText != null)
        {
            algorithmText.text = SettingsManager.Instance.SelectedMazeAlgorithm.ToString();
        }
        else
        {
            Debug.LogError("TextMeshPro text object is not assigned.");
        }
    }
}
