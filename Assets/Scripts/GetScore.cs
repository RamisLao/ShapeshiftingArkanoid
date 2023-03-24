using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    private void OnEnable()
    {
        text.text = $"Your score: {scoreSystem.CurrentScore}";
    }
}
