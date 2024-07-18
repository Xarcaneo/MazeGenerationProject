using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Tooltip("TextMeshPro text object for timer text")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Tooltip("Base time limit for the smallest maze (10x10) in seconds")]
    [SerializeField] private float baseTimeLimit = 30f;

    [Tooltip("Additional time per cell for larger mazes")]
    [SerializeField] private float timePerCell = 0.1f;

    [Tooltip("Time in seconds when the timer text color should change")]
    [SerializeField] private float warningTime = 10f;

    [Tooltip("Color for the timer text when time is below warning time")]
    [SerializeField] private Color warningColor = Color.red;

    private float currentTime;
    private Color originalColor;

    public event Action TimeUp;

    private void Start()
    {
        if (timerText != null)
        {
            originalColor = timerText.color;
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
        currentTime = baseTimeLimit + (totalCells * timePerCell);
    }

    private IEnumerator CountdownTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerUI();
        }

        TimeUp?.Invoke();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = $"Time: {timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";

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
        StopAllCoroutines();
    }
}
