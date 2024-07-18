using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField, Tooltip("Max score to complete level")]
    public int maxScoreToWin;

    [Tooltip("MazeManager GameObject.")]
    [SerializeField] private MazeManager mazeManager;

    [Tooltip("TextMesgoPro text object for current level text")]
    [SerializeField] private TextMeshProUGUI currentLevelText;

    [Tooltip("Reference to the TimerManager")]
    [SerializeField] private TimerManager timerManager;

    private int m_current_level = 1;
    private int m_current_score = 0;

    public event Action ScoreIncreased;
    public event Action ScoreReseted;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        currentLevelText.text = "Level: " + m_current_level;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SettingsManager.Instance.HasTimeLimit && timerManager != null)
        {
            timerManager.TimeUp += OnTimeUp;
            timerManager.InitializeTimer(SettingsManager.Instance.Width, SettingsManager.Instance.Height);
        }
    }

    private void OnDestroy()
    {
        if (timerManager != null)
        {
            timerManager.TimeUp -= OnTimeUp;
        }
    }

    public void IncreaseScore()
    {
        if (m_current_score < maxScoreToWin)
            m_current_score++;

        ScoreIncreased?.Invoke();

        if (m_current_score == maxScoreToWin && mazeManager)
        {
            ResetLevelVariables();
            m_current_level++;
            currentLevelText.text = "Level: " + m_current_level;
            mazeManager.GenerateNewMaze();
        }
    }

    private void OnTimeUp()
    {
        Debug.Log("Time's up!");
        // Handle game over logic here
    }

    public void ResetLevelVariables()
    {
        m_current_score = 0;

        if (SettingsManager.Instance.HasTimeLimit && timerManager != null)
        {
            timerManager.InitializeTimer(SettingsManager.Instance.Width, SettingsManager.Instance.Height);
        }

        ScoreReseted?.Invoke();
    }

    public int GetScore() { return m_current_score; }
}
