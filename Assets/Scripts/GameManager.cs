using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Quiz quiz;
    ResultScreen resultScreen;

    void Awake()
    {
        quiz = FindAnyObjectByType<Quiz>();
        resultScreen = FindAnyObjectByType<ResultScreen>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
        resultScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            resultScreen.gameObject.SetActive(true);
            resultScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {
        // load the scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
