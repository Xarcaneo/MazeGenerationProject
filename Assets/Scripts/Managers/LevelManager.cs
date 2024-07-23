using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the level progression, score tracking, and related events in the game.
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Maximum score required to complete the level.
    /// </summary>
    [SerializeField, Tooltip("Max score to complete level")]
    public int maxScoreToWin;

    /// <summary>
    /// Current level of the game.
    /// </summary>
    private int m_current_level = 1;

    /// <summary>
    /// Current score of the player in the current level.
    /// </summary>
    private int m_current_score = 0;

    /// <summary>
    /// Event triggered when the score is increased.
    /// </summary>
    public event Action ScoreIncreased;

    /// <summary>
    /// Event triggered when the score is reset.
    /// </summary>
    public event Action ScoreReseted;

    /// <summary>
    /// Event triggered when a new level is started.
    /// </summary>
    public event Action NewLevel;

    /// <summary>
    /// Singleton instance of the LevelManager.
    /// </summary>
    public static LevelManager Instance { get; private set; }

    /// <summary>
    /// Initializes the singleton instance and prevents it from being destroyed on scene load.
    /// </summary>
    private void Awake()
    {
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

    /// <summary>
    /// Increases the player's score and checks if the level is completed.
    /// </summary>
    public void IncreaseScore()
    {
        if (m_current_score < maxScoreToWin)
        {
            m_current_score++;
            ScoreIncreased?.Invoke();

            if (m_current_score == maxScoreToWin)
            {
                ResetLevelVariables();
                m_current_level++;
                NewLevel?.Invoke();
            }
        }
    }

    /// <summary>
    /// Resets the level-specific variables such as the score.
    /// </summary>
    public void ResetLevelVariables()
    {
        m_current_score = 0;
        ScoreReseted?.Invoke();
    }

    /// <summary>
    /// Gets the current score of the player.
    /// </summary>
    /// <returns>The current score.</returns>
    public int GetScore()
    {
        return m_current_score;
    }

    /// <summary>
    /// Gets the current level of the game.
    /// </summary>
    /// <returns>The current level.</returns>
    public int GetCurrentLevel()
    {
        return m_current_level;
    }
}
