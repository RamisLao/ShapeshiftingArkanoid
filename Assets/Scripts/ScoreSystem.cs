using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private ObjectShake objectShake;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    private int currentScore = 0;
    public int CurrentScore => currentScore;

    private void Start()
    {
        text.text = "Score: 0";
    }

    public void AddScore(int score)
    {
        currentScore += score;
        text.color = successColor;
        text.text = $"Score: {currentScore}";
    }

    public void SubtractScore(int score)
    {
        currentScore -= score;
        text.color = failureColor;
        text.text = $"Score: {currentScore}";
    }
}
