using System;
using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the countdown timer for the game and updates the timer UI.
/// </summary>
public class TimerManager : MonoBehaviour
{
    /// <summary>
    /// TextMeshPro text object for displaying the timer.
    /// </summary>
    [Tooltip("TextMeshPro text object for timer text")]
    [SerializeField] private TextMeshProUGUI timerText;

    /// <summary>
    /// Additional time added per cell for larger mazes.
    /// </summary>
    [Tooltip("Additional time per cell for larger mazes")]
    [SerializeField] private float timePerCell = 0.1f;

    /// <summary>
    /// Time in seconds when the timer text color should change.
    /// </summary>
    [Tooltip("Time in seconds when the timer text color should change")]
    [SerializeField] private float warningTime = 10f;

    /// <summary>
    /// Color for the timer text when time is below the warning time.
    /// </summary>
    [Tooltip("Color for the timer text when time is below warning time")]
    [SerializeField] private Color warningColor = Color.red;

    /// <summary>
    /// Reference to the MazeManager.
    /// </summary>
    [Tooltip("Reference to the MazeManager")]
    [SerializeField] private MazeManager mazeManager;

    /// <summary>
    /// Current remaining time.
    /// </summary>
    private float currentTime;

    /// <summary>
    /// Original color of the timer text.
    /// </summary>
    private Color originalColor;

    /// <summary>
    /// Flag to indicate if the warning time has been triggered.
    /// </summary>
    private bool warningTimeOn;

    /// <summary>
    /// Event triggered when the time is almost up.
    /// </summary>
    public event Action TimeAlmostUp;

    /// <summary>
    /// Initializes the TimerManager.
    /// </summary>
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

    /// <summary>
    /// Resets the timer when the score is reset.
    /// </summary>
    private void OnScoreReseted()
    {
        if (SettingsManager.Instance && SettingsManager.Instance.HasTimeLimit)
        {
            InitializeTimer(SettingsManager.Instance.Width, SettingsManager.Instance.Height);
        }
    }

    /// <summary>
    /// Initializes the timer based on the maze dimensions.
    /// </summary>
    /// <param name="width">Width of the maze.</param>
    /// <param name="height">Height of the maze.</param>
    public void InitializeTimer(int width, int height)
    {
        warningTimeOn = false;
        StopAllCoroutines();
        AdjustTimeLimit(width, height);
        StartCoroutine(CountdownTimer());
    }

    /// <summary>
    /// Adjusts the time limit based on the maze size.
    /// </summary>
    /// <param name="width">Width of the maze.</param>
    /// <param name="height">Height of the maze.</param>
    private void AdjustTimeLimit(int width, int height)
    {
        int totalCells = width * height;
        currentTime = totalCells * timePerCell;
    }

    /// <summary>
    /// Coroutine for the countdown timer.
    /// </summary>
    /// <returns>IEnumerator for the coroutine.</returns>
    private IEnumerator CountdownTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerUI();
        }

        // Trigger the back to main menu when time is up
        mazeManager.BackToMenu();
    }

    /// <summary>
    /// Updates the timer UI with the current remaining time.
    /// </summary>
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";

            if (warningTimeOn) return;

            // Change color if below warning time
            if (currentTime <= warningTime)
            {
                TimeAlmostUp?.Invoke();
                timerText.color = warningColor;
                warningTimeOn = true;
            }
            else
            {
                timerText.color = originalColor;
            }
        }
    }

    /// <summary>
    /// Cleans up event subscriptions and stops all coroutines when the TimerManager is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        LevelManager.Instance.ScoreReseted -= OnScoreReseted;
        StopAllCoroutines();
    }
}
