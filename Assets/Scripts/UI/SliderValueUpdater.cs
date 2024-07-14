using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueUpdater : MonoBehaviour
{
    [SerializeField, Tooltip("The slider component to get the value from")]
    private Slider slider;

    [SerializeField, Tooltip("The TextMeshPro text component to display the value")]
    private TextMeshProUGUI valueText;

    [SerializeField, Tooltip("The prefix text to display before the value (e.g., 'Height:' or 'Width:')")]
    private string prefixText = "Height";

    void Start()
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

    private void UpdateValueText(float value)
    {
        valueText.text = prefixText + ": " + value.ToString("F0"); // "F0" formats the float to 0 decimal places
    }
}
