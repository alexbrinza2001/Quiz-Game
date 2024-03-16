using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")]
public class QuestionSO : ScriptableObject {
    
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswer;

    public string getQuestion() {
        return question;
    }

    public string getAnswer(int index) {
        return answers[index];
    }

    public int getCorrectAnswer() {
        return correctAnswer;
    }
}
