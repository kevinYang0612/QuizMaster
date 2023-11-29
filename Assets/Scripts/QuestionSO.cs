using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz question", fileName = "New Queston")] 
// add options to create something in this list

// this class is dealing with the actual question and answer
// Each question will need to fill in the question and options
// Example Question 1 created as questionSO. 
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] options = new string[4];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
    public string GetOption(int index)
    {
        return options[index];
    }

}
