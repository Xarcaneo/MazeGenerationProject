using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Updates the displayed value of a TextMeshProUGUI component based on the value of a slider.
/// </summary>
public class SliderValueUpdater : MonoBehaviour
{
    /// <summary>
    /// The slider component to get the value from.
    /// </summary>
    [SerializeField, Tooltip("The slider component to get the value from")]
    private Slider slider;

    /// <summary>
    /// The TextMeshPro text component to display the value.
    /// </summary>
    [SerializeField, Tooltip("The TextMeshPro text component to display the value")]
    private TextMeshProUGUI valueText;

    /// <summary>
    /// The prefix text to display before the value (e.g., 'Height:' or 'Width:').
    /// </summary>
    [SerializeField, Tooltip("The prefix text to display before the value (e.g., 'Height:' or 'Width:')")]
    private string prefixText = "Height";

    /// <summary>
    /// Initializes the slider value updater.
    /// </summary>
    private void Start()
    {
        if (slider == null)
        {
            Debug.LogError("Slider reference is missing.");
            return;
        }

        if (valueText == null)
        {
            Debug.LogError("TextMeshProUGUI reference is missing.");
            return;
        }

        // Initialize the text with the current slider value
        UpdateValueText(slider.value);

        // Add a listener to call the UpdateValueText method whenever the slider value changes
        slider.onValueChanged.AddListener(UpdateValueText);
    }

    /// <summary>
    /// Updates the text to display the current slider value.
    /// </summary>
    /// <param name="value">The current value of the slider.</param>
    private void UpdateValueText(float value)
    {
        // Format the value to 0 decimal places and update the text component
        valueText.text = prefixText + ": " + value.ToString("F0");
    }
}
