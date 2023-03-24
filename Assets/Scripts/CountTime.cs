using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountTime : MonoBehaviour
{
    [SerializeField] private int currentTime = 120;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private UnityEvent timeEnded;

    void Start()
    {
        StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        yield return new WaitForSeconds(Constants.StartDelay);

        bool timerIsRunnning = true;
        while (timerIsRunnning)
        {
            text.text = $"Time: {currentTime}";

            if (currentTime == 0)
            {
                timeEnded.Invoke();
                timerIsRunnning = false;
            }

            yield return new WaitForSeconds(1);
            currentTime -= 1;
        }
    }
}
