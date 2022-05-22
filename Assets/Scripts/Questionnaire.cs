using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Questionnaire : MonoBehaviour
{
    [Serializable]
    public struct Question
    {
        public string question;
        public string answer1;
        public string answer2;
        public List<StatCategories> leftAnswerTallies;
        public List<StatCategories> rightAnswerTallies;

    }

    [SerializeField] private List<Question> allQuestions = new List<Question>();

    private int currentQuestionIndex = 0;

    [SerializeField] private TextMeshProUGUI questionText;

    [SerializeField] private TextMeshProUGUI answer1Text;
    [SerializeField] private TextMeshProUGUI answer2Text;

    [SerializeField] private RectTransform leftAnswerRect;
    [SerializeField] private RectTransform rightAnswerRect;

    [SerializeField] private GameObject inputBlocker;

    [SerializeField] private Animator questionnaireAnimator;

    [SerializeField] private RectTransform selectorRect;
    private float startingSelectorXPosition;

    [SerializeField] private float maxSwipeDistance;

    [SerializeField] private CanvasGroup leftAnswerCanvasGroup;
    [SerializeField] private CanvasGroup rightAnswerCanvasGroup;

    [SerializeField] private InputDetection inputDetection;

    [SerializeField] private SafeArea safeArea;

    [SerializeField] private RectTransform leftFeedBackRect;
    [SerializeField] private RectTransform rightFeedbackRect;

    [SerializeField] private PlayerStats playerStats;

    public Action OnQuestionnaireEnded;

    private void Awake()
    {
        startingSelectorXPosition = selectorRect.anchoredPosition.x;
    }

    private void Start()
    {
        float safeAreaWidth = safeArea.GetSafeAreaWidth();
        maxSwipeDistance = safeAreaWidth / 2f;
        leftFeedBackRect.sizeDelta = new Vector2(safeAreaWidth / 2f, leftFeedBackRect.rect.height);
        rightFeedbackRect.sizeDelta = new Vector2(safeAreaWidth / 2f, rightFeedbackRect.rect.height);
        leftAnswerRect.sizeDelta = new Vector2(safeAreaWidth / 2f, leftAnswerRect.rect.height);
        rightAnswerRect.sizeDelta = new Vector2(safeAreaWidth / 2f, rightAnswerRect.rect.height);
    }

    public void StartQuestionnaire()
    {
        FillQuestionInfo();
        gameObject.SetActive(true);
        inputBlocker.SetActive(true);
    }

    public void HideQuestionnaire()
    {
        gameObject.SetActive(false);
    }

    public void CheckForNextQuestion()
    {
        if (currentQuestionIndex >= allQuestions.Count)
        {
            EndQuestionnaire();
        }
        else if (allQuestions.Count == 0)
        {
            EndQuestionnaire();
        }
        else
        {
            StartNextQuestionAnimation();
        }
    }

    public void StartNextQuestionAnimation()
    {
        questionnaireAnimator.SetTrigger("Next");
    }

    public void EndQuestionnaire()
    {
        questionnaireAnimator.SetTrigger("Exit");
    }

    public void FillQuestionInfo()
    {
        UpdateAnswerDisplayOpacities(0f, 0f);
        UpdateSelectorPosition(0f);
        questionText.text = allQuestions[currentQuestionIndex].question;
        answer1Text.text = allQuestions[currentQuestionIndex].answer1;
        answer2Text.text = allQuestions[currentQuestionIndex].answer2;
    }

    public void EndQuestionFadeIn()
    {
        inputBlocker.SetActive(false);
        inputDetection.InitializeChoiceSwipe(ChoiceType.Question);
    }

    public void QuestionnaireEnded()
    {
        playerStats.DisplayStats();
        OnQuestionnaireEnded?.Invoke();
    }

    public void UpdateSelectorPosition(float _difference)
    {
        if (_difference > 0)
        {
            _difference = Mathf.Clamp(_difference, 0, maxSwipeDistance);
        }

        if (_difference < 0)
        {
            _difference = Mathf.Clamp(_difference, -maxSwipeDistance, 0);
        }

        selectorRect.anchoredPosition = new Vector2(startingSelectorXPosition + _difference, selectorRect.anchoredPosition.y);
    }

    public void UpdateAnswerDisplayOpacities(float _left, float _right)
    {
        leftAnswerCanvasGroup.alpha = _left;
        rightAnswerCanvasGroup.alpha = _right;
    }

    public void SwipedLeft()
    {
        UpdateAnswerDisplayOpacities(1f, 0f);
        inputBlocker.SetActive(true);
        playerStats.AddTallies(allQuestions[currentQuestionIndex].leftAnswerTallies);
        currentQuestionIndex++;
        CheckForNextQuestion();
        //play left swipe animation
    }

    public void SwipedRight()
    {
        UpdateAnswerDisplayOpacities(0f, 1f);
        inputBlocker.SetActive(true);
        playerStats.AddTallies(allQuestions[currentQuestionIndex].rightAnswerTallies);
        currentQuestionIndex++;
        CheckForNextQuestion();
        //play right swipe animation
    }


}
