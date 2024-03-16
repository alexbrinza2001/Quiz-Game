using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void showFinalScore() {
        finalScoreText.text = "Congratulations!\nYou got a score of " + scoreKeeper.calculateScore() + "%";
    }

}
