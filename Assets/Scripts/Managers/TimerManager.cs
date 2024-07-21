using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Tooltip("TextMeshPro text object for timer text")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Tooltip("Additional time per cell for larger mazes")]
    [SerializeField] private float timePerCell = 0.1f;

    [Tooltip("Time in seconds when the timer text color should change")]
    [SerializeField] private float warningTime = 10f;

    [Tooltip("Color for the timer text when time is below warning time")]
    [SerializeField] private Color warningColor = Color.red;

    private float currentTime;
    private Color originalColor;

    private void Start()
    {
        if (timerText != null)
        {
            originalColor = timerText.color;
        }

        if (SettingsManager.Instance && SettingsManager.Instance.HasTimeLimit)
        {
            InitializeTimer(SettingsManager.Instance.Width, SettingsManager.Instance.Height);
        }

        LevelManager.Instance.ScoreReseted += OnScoreReseted;
    }

    private void OnScoreReseted()
    {
        if (SettingsManager.Instance && SettingsManager.Instance.HasTimeLimit)
        {
            InitializeTimer(SettingsManager.Instance.Width, SettingsManager.Instance.Height);
        }
    }

    public void InitializeTimer(int width, int height)
    {
        StopAllCoroutines();
        AdjustTimeLimit(width, height);
        StartCoroutine(CountdownTimer());
    }

    private void AdjustTimeLimit(int width, int height)
    {
        int totalCells = width * height;
        currentTime = totalCells * timePerCell;
    }

    private IEnumerator CountdownTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";

            // Change color if below warning time
            if (currentTime <= warningTime)
            {
                timerText.color = warningColor;
            }
            else
            {
                timerText.color = originalColor;
            }
        }
    }

    private void OnDestroy()
    {
        LevelManager.Instance.ScoreReseted -= OnScoreReseted;
        StopAllCoroutines();
    }
}
