using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


// This class is dealing with displaying question, options, and answer on UI. 
public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    // TextMeshProGUI controls unity
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    // QuestionSO here needs to access its methods. 

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    // empty game object, it needs to add the actual answer button that is built.
    int correctAnswerIndex;
    bool hasAnswerEarly = true; // either answered or let timer runs out. 

    [Header("Button Colors")]
    // these sprites are instantiated/selected in unity by dragging to the field
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite; 

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer; // timer class, 

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText; // text on UI canvas
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    private bool hasIncrementedProgressBar = false;

    public bool isComplete;

    // Display new question, turn buttons on, answer question, turn buttons off
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        // timer is looking for Timer type under the QuizCanvas, which is set in Timer.cs
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() * 0 + "%";
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        // fillFraction is public field in Timer class. 
        // check if needs to load next question, only after when displaying
        // correct answer for 10 seconds. 
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnswerEarly = false; // not yet answering the question
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnswerEarly && !timer.isAnsweringQuestion 
                && !hasIncrementedProgressBar) // which means timer runs out time and no answering
        {
            DisplayAnswer(-1); // force to display correct answer in else block
            SetButtonState(false);

            progressBar.value++;
            hasIncrementedProgressBar = true;
        }
    }

    // connecting this method to all four buttons, we need to know 
    //which button was called. Options On Click, choose QuizCanvas 
    public void OnAnswerSelected(int index)
    {
        // if we clicked the answer button, set hasAnswerEarly = true;
        hasAnswerEarly = true;
        DisplayAnswer(index);

        // due to user can still choose after selecting right or wrong
        SetButtonState(false); // we want to disable the buttons at this stage
        timer.CancelTimer();

        progressBar.value++;
        hasIncrementedProgressBar = true;
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;
        // if selected index == correct index saved in Question 1 SO
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct";
            // Temp variable, grabbing the current button image
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Sorry, the correct answer is: \n" + currentQuestion.GetOption(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            hasIncrementedProgressBar = false;
            // reset the button interactable state
            SetButtonState(true); 
            // reset the button sprite to not display correct answer sprite
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementQuestionsSeen();
        }
    }
    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
            
    }

    private void DisplayQuestion()
    {
        // displaying question to TextMeshPro from questionSO
        questionText.text = currentQuestion.GetQuestion();
        // looping to display all 4 options
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            // gameobject.get TextMeshProGUI's children, which is the text of button. 
            buttonText.text = currentQuestion.GetOption(i);
            // setting the first option
        }
    }

    // whenever a user finished answering either right or wrong, 
    // we need to disable the option buttons by changing the interactable state
    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state; // set the flag from passing in
        }
    }

    private void SetDefaultButtonSprite()
    {
        Image buttonImage;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
