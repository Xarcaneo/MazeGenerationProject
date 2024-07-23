using UnityEngine;
using TMPro;

/// <summary>
/// Updates the level text UI to reflect the current level in the game.
/// </summary>
public class LevelTextUpdater : MonoBehaviour
{
    /// <summary>
    /// Reference to the TextMeshProUGUI component that displays the level text.
    /// </summary>
    [SerializeField] private TextMeshProUGUI levelText;

    /// <summary>
    /// Subscribes to the NewLevel event when the object is enabled.
    /// </summary>
    private void OnEnable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.NewLevel += UpdateLevelText;
        }
    }

    /// <summary>
    /// Unsubscribes from the NewLevel event when the object is disabled.
    /// </summary>
    private void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.NewLevel -= UpdateLevelText;
        }
    }

    /// <summary>
    /// Initializes the level text when the script starts.
    /// </summary>
    private void Start()
    {
        UpdateLevelText();
    }

    /// <summary>
    /// Updates the level text to show the current level.
    /// </summary>
    private void UpdateLevelText()
    {
        if (LevelManager.Instance != null)
        {
            levelText.text = LevelManager.Instance.GetCurrentLevel().ToString();
        }
    }
}
