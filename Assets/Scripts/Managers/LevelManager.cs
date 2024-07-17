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

    private int m_current_level = 1;
    private int m_current_score = 0;

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

    public void IncreaseScore()
    {
        if (m_current_score < maxScoreToWin)
            m_current_score++;

        if (m_current_score == maxScoreToWin && mazeManager)
        {
            m_current_score = 0;
            m_current_level++;
            currentLevelText.text = "Level: " + m_current_level;
            mazeManager.GenerateNewMaze();
        }
    }
}
