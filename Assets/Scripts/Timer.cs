using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToComplete = 15f;
    [SerializeField] float timeToShowAnswer = 10f;
    public bool loadNextQuestion;
    public bool isAnswering = false;
    public float fillFraction;
    float timerValue;

    void Update()
    {
        updateTimer();
    }

    public void cancelTimer() {
        timerValue = 0;
    }

    void updateTimer() {
        timerValue -= Time.deltaTime;

        if (isAnswering) {
            
            if (timerValue > 0) {
                fillFraction = timerValue / timeToComplete;
            }
            else {
                timerValue = timeToShowAnswer;
                isAnswering = false;
            }   
        }
        else {
            if (timerValue > 0) {
                fillFraction = timerValue / timeToShowAnswer;
            }
            else {
                timerValue = timeToComplete;
                isAnswering = true;
                loadNextQuestion = true;
            }
        }
    }
}
