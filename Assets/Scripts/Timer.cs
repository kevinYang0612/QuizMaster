using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 5f;
    [SerializeField] float timeToShowCorrectAnswer = 1f;

    public bool loadNextQuestion;
    public bool isAnsweringQuestion = false;
    public float fillFraction;
    float timerValue;
    void Update()
    {
        updateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    private void updateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (timerValue > 0) // display the timer fraction
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else // when time runs out
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else 
        {
            if (timerValue > 0) // display the timer fraction
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else // when time runs out for displaying right answer
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
