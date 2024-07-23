using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the UI settings for configuring the maze dimensions.
/// </summary>
public class MazeSettings : MonoBehaviour
{
    /// <summary>
    /// Slider for setting the maze width.
    /// </summary>
    [SerializeField, Tooltip("Slider for setting maze width")]
    private Slider widthSlider;

    /// <summary>
    /// Text to display the current maze width value.
    /// </summary>
    [SerializeField, Tooltip("Text to display maze width value")]
    private TextMeshProUGUI widthValueText;

    /// <summary>
    /// Slider for setting the maze height.
    /// </summary>
    [SerializeField, Tooltip("Slider for setting maze height")]
    private Slider heightSlider;

    /// <summary>
    /// Text to display the current maze height value.
    /// </summary>
    [SerializeField, Tooltip("Text to display maze height value")]
    private TextMeshProUGUI heightValueText;

    /// <summary>
    /// Initializes the settings UI and sets up listeners for the sliders.
    /// </summary>
    private void Start()
    {
        // Add listeners for the sliders to handle value changes
        widthSlider.onValueChanged.AddListener(OnWidthSliderValueChanged);
        heightSlider.onValueChanged.AddListener(OnHeightSliderValueChanged);

        // Update the displayed text to match the initial slider values
        UpdateWidthText(widthSlider.value);
        UpdateHeightText(heightSlider.value);

        // Set slider values to match the current settings from the SettingsManager
        widthSlider.value = SettingsManager.Instance.Width;
        heightSlider.value = SettingsManager.Instance.Height;
    }

    /// <summary>
    /// Handles changes in the width slider value.
    /// </summary>
    /// <param name="value">The new width value.</param>
    private void OnWidthSliderValueChanged(float value)
    {
        UpdateWidthText(value);
        SettingsManager.Instance.Width = (int)widthSlider.value;
    }

    /// <summary>
    /// Handles changes in the height slider value.
    /// </summary>
    /// <param name="value">The new height value.</param>
    private void OnHeightSliderValueChanged(float value)
    {
        UpdateHeightText(value);
        SettingsManager.Instance.Height = (int)heightSlider.value;
    }

    /// <summary>
    /// Updates the text displaying the current width value.
    /// </summary>
    /// <param name="value">The current width value.</param>
    private void UpdateWidthText(float value)
    {
        widthValueText.text = value.ToString("F0");
    }

    /// <summary>
    /// Updates the text displaying the current height value.
    /// </summary>
    /// <param name="value">The current height value.</param>
    private void UpdateHeightText(float value)
    {
        heightValueText.text = value.ToString("F0");
    }

    /// <summary>
    /// Loads the gameplay scene when the play button is clicked.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
