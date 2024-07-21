using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab for the Carrot Icon")]
    private GameObject carrotIconPrefab;

    [SerializeField, Tooltip("Sprite for the collected Carrot Icon")]
    private Sprite collectedCarrotSprite;

    [SerializeField, Tooltip("Reference to the TimerManager")]
    private TimerManager timerManager;

    private List<Image> carrotIcons = new List<Image>();
    private Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();

        LevelManager.Instance.ScoreIncreased += OnScoreIncreased;
        LevelManager.Instance.ScoreReseted += OnScoreReseted;
        if (timerManager != null)
        {
            timerManager.TimeAlmostUp += OnTimeAlmostUp;
        }

        InitializeCarrotIcons();
    }

    private void InitializeCarrotIcons()
    {
        ClearCarrotIcons();

        for (int i = 0; i < LevelManager.Instance.maxScoreToWin; i++)
        {
            GameObject icon = Instantiate(carrotIconPrefab, transform);
            Image iconImage = icon.GetComponent<Image>();
            if (iconImage != null)
            {
                carrotIcons.Add(iconImage);
            }
        }
    }

    private void ClearCarrotIcons()
    {
        foreach (Image icon in carrotIcons)
        {
            Destroy(icon.gameObject);
        }
        carrotIcons.Clear();
    }

    private void OnScoreIncreased()
    {
        int currentScore = LevelManager.Instance.GetScore();

        if (currentScore - 1 < carrotIcons.Count)
        {
            carrotIcons[currentScore - 1].sprite = collectedCarrotSprite;
        }
    }

    private void OnScoreReseted()
    {
        m_animator.Play("Idle");
        ClearCarrotIcons();
        InitializeCarrotIcons();
    }

    private void OnTimeAlmostUp() { m_animator.Play("Hurry"); }

    private void OnDestroy()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ScoreIncreased -= OnScoreIncreased;
            LevelManager.Instance.ScoreReseted -= OnScoreReseted;
        }
        if (timerManager != null)
        {
            timerManager.TimeAlmostUp -= OnTimeAlmostUp;
        }
    }
}
