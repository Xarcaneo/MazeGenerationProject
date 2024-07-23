using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the UI for displaying the score using carrot icons.
/// </summary>
public class ScoreCounterUI : MonoBehaviour
{
    /// <summary>
    /// Prefab for the carrot icon.
    /// </summary>
    [SerializeField, Tooltip("Prefab for the Carrot Icon")]
    private GameObject carrotIconPrefab;

    /// <summary>
    /// Sprite for the collected carrot icon.
    /// </summary>
    [SerializeField, Tooltip("Sprite for the collected Carrot Icon")]
    private Sprite collectedCarrotSprite;

    /// <summary>
    /// Reference to the TimerManager.
    /// </summary>
    [SerializeField, Tooltip("Reference to the TimerManager")]
    private TimerManager timerManager;

    /// <summary>
    /// List to hold the carrot icon images.
    /// </summary>
    private List<Image> carrotIcons = new List<Image>();

    /// <summary>
    /// Reference to the Animator component.
    /// </summary>
    private Animator m_animator;

    /// <summary>
    /// Initializes the ScoreCounterUI and subscribes to events.
    /// </summary>
    void Start()
    {
        m_animator = GetComponent<Animator>();

        // Subscribe to the LevelManager events
        LevelManager.Instance.ScoreIncreased += OnScoreIncreased;
        LevelManager.Instance.ScoreReseted += OnScoreReseted;

        // Subscribe to the TimerManager event if available
        if (timerManager != null)
        {
            timerManager.TimeAlmostUp += OnTimeAlmostUp;
        }

        InitializeCarrotIcons();
    }

    /// <summary>
    /// Initializes the carrot icons based on the maximum score to win.
    /// </summary>
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

    /// <summary>
    /// Clears the current carrot icons from the UI.
    /// </summary>
    private void ClearCarrotIcons()
    {
        foreach (Image icon in carrotIcons)
        {
            Destroy(icon.gameObject);
        }
        carrotIcons.Clear();
    }

    /// <summary>
    /// Handles the score increased event by updating the carrot icons.
    /// </summary>
    private void OnScoreIncreased()
    {
        int currentScore = LevelManager.Instance.GetScore();

        if (currentScore - 1 < carrotIcons.Count)
        {
            carrotIcons[currentScore - 1].sprite = collectedCarrotSprite;
        }
    }

    /// <summary>
    /// Handles the score reset event by resetting the carrot icons and animations.
    /// </summary>
    private void OnScoreReseted()
    {
        m_animator.Play("Idle");
        ClearCarrotIcons();
        InitializeCarrotIcons();
    }

    /// <summary>
    /// Handles the time almost up event by playing the "Hurry" animation.
    /// </summary>
    private void OnTimeAlmostUp()
    {
        m_animator.Play("Hurry");
    }

    /// <summary>
    /// Unsubscribes from events when the ScoreCounterUI is destroyed.
    /// </summary>
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
