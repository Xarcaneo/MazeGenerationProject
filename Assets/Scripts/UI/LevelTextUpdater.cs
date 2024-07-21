using UnityEngine;
using TMPro;

public class LevelTextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private void OnEnable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.NewLevel += UpdateLevelText;
        }
    }

    private void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.NewLevel -= UpdateLevelText;
        }
    }

    private void Start()
    {
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        if (LevelManager.Instance != null)
        {
            levelText.text = LevelManager.Instance.GetCurrentLevel().ToString();
        }
    }
}
