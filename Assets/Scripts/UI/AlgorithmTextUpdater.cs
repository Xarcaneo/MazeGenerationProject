using TMPro;
using UnityEngine;

public class AlgorithmTextUpdater : MonoBehaviour
{
    [SerializeField, Tooltip("TextMeshPro text object to display the selected algorithm")]
    private TextMeshProUGUI algorithmText;

    private void Start()
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.AlgorithmChanged += OnAlgorithmChanged;
            UpdateAlgorithmText(); // Initialize with the current algorithm
        }
        else
        {
            Debug.LogError("SettingsManager instance not found.");
        }
    }

    private void OnDestroy()
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.AlgorithmChanged -= OnAlgorithmChanged;
        }
    }

    private void OnAlgorithmChanged()
    {
        UpdateAlgorithmText();
    }

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
