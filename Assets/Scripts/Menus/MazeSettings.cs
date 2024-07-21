using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MazeSettings : MonoBehaviour
{
    [SerializeField, Tooltip("Slider for setting maze width")]
    private Slider widthSlider;

    [SerializeField, Tooltip("Text to display maze width value")]
    private TextMeshProUGUI widthValueText;

    [SerializeField, Tooltip("Slider for setting maze height")]
    private Slider heightSlider;

    [SerializeField, Tooltip("Text to display maze height value")]
    private TextMeshProUGUI heightValueText;

    private void Start()
    {
        widthSlider.onValueChanged.AddListener(OnWidthSliderValueChanged);
        heightSlider.onValueChanged.AddListener(OnHeightSliderValueChanged);

        UpdateWidthText(widthSlider.value);
        UpdateHeightText(heightSlider.value);

        widthSlider.value = SettingsManager.Instance.Width;
        heightSlider.value = SettingsManager.Instance.Height;
    }

    private void OnWidthSliderValueChanged(float value)
    {
        UpdateWidthText(value);
        SettingsManager.Instance.Width = (int)widthSlider.value;
    }

    private void OnHeightSliderValueChanged(float value)
    {
        UpdateHeightText(value);
        SettingsManager.Instance.Height = (int)heightSlider.value;
    }

    private void UpdateWidthText(float value)
    {
        widthValueText.text = value.ToString("F0");
    }

    private void UpdateHeightText(float value)
    {
        heightValueText.text = value.ToString("F0");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
