using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor.Rendering;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        isComplete = false;
    }

    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion) {

            if (progressBar.value == progressBar.maxValue) {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            getNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnswering) {
            displayAnswer(-1);
            setButtonState(false);
        }
    }

    public void onAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        displayAnswer(index);
        setButtonState(false);
        timer.cancelTimer();
        scoreText.text = "Score " + scoreKeeper.calculateScore() + "%";
    }

    private void displayAnswer(int index)
    {
        if (index == currentQuestion.getCorrectAnswer())
        {
            questionText.text = "Correct!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.increaseCorrectAnswers();
        }
        else
        {
            string correctAnswer = currentQuestion.getAnswer(currentQuestion.getCorrectAnswer());
            questionText.text = "Wrong! See the correct answer:\n" + correctAnswer;
            Image buttonImage = answerButtons[currentQuestion.getCorrectAnswer()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void getNextQuestion() {
        if (questions.Count > 0) {
            setButtonState(true);
            setDefaultButtonSprites();
            getRandomQuestion();
            displayQuestion();
            progressBar.value++;
            scoreKeeper.increaseQuestionsSeen();
        }
    }

    void getRandomQuestion() {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
            questions.Remove(currentQuestion);
    }

    void setDefaultButtonSprites() {
        int numberOfAnswers = answerButtons.Length;
        for (int i = 0; i < numberOfAnswers; ++i) {
            answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

    public void displayQuestion() {
        questionText.text = currentQuestion.getQuestion();
        int numberOfAnswers = answerButtons.Length;
        for (int i = 0; i < numberOfAnswers; ++i) {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.getAnswer(i);
        }
    }

    void setButtonState(bool state) {
        int numberOfAnswers = answerButtons.Length;
        for (int i = 0; i < numberOfAnswers; ++i) {
            answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }
}
