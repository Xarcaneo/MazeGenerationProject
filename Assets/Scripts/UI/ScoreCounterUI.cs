using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab for the Carrot Icon")]
    private GameObject carrotIconPrefab;

    [SerializeField, Tooltip("Color for the collected Carrot Icon")]
    private Color collectedColor = Color.green;

    private List<Image> carrotIcons = new List<Image>();

    void Start()
    {
        LevelManager.Instance.ScoreIncreased += OnScoreIncreased;
        LevelManager.Instance.ScoreReseted += OnScoreReseted;

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
            carrotIcons[currentScore - 1].color = collectedColor;
        }
    }

    private void OnScoreReseted()
    {
        ClearCarrotIcons();
        InitializeCarrotIcons();
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ScoreIncreased -= OnScoreIncreased;
            LevelManager.Instance.ScoreReseted -= OnScoreReseted;
        }
    }
}
