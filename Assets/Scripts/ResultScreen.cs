using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScreen : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper; // accessing the result scorekeeper
    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations!\n You Scored: " 
                                + scoreKeeper.CalculateScore() + "%";
    }

}
