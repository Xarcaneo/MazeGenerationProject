using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField, Tooltip("Max score to complete level")]
    public int maxScoreToWin;

    private int m_current_level = 1;
    private int m_current_score = 0;

    public event Action ScoreIncreased;
    public event Action ScoreReseted;
    public event Action NewLevel;

    public static LevelManager Instance { get; private set; }

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

    public void IncreaseScore()
    {
        if (m_current_score < maxScoreToWin)
            m_current_score++;

        ScoreIncreased?.Invoke();

        if (m_current_score == maxScoreToWin)
        {
            ResetLevelVariables();
            m_current_level++;
            NewLevel?.Invoke();
        }
    }

    public void ResetLevelVariables()
    {
        m_current_score = 0;

        ScoreReseted?.Invoke();
    }

    public int GetScore() { return m_current_score; }
    public int GetCurrentLevel() { return m_current_level; }
}
