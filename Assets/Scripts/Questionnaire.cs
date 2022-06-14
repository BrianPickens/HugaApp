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

    [SerializeField] private Image leftSideBackground;
    [SerializeField] private Image rightSideBackground;

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

    [SerializeField] private PictureManager pictureManager;

    [SerializeField] private GameObject inkblotPicture;
    [SerializeField] private GameObject drinkStandPicture;
    [SerializeField] private GameObject chainsawPicture;

    [SerializeField] private Image drinkStandImage;
    [SerializeField] private Image chainsawImage;


    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;

    [SerializeField] private Color textColor;
    [SerializeField] private Color transparentTextColor;

    [SerializeField] private Color imageColorWhite;
    [SerializeField] private Color imageColorTransparent;

    private float startingTextBoxWidth;

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
        startingTextBoxWidth = leftAnswerRect.rect.width;
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

        leftAnswerRect.sizeDelta = new Vector2(startingTextBoxWidth, leftAnswerRect.rect.height);
        rightAnswerRect.sizeDelta = new Vector2(startingTextBoxWidth, rightAnswerRect.rect.height);

        leftSideBackground.color = redColor;
        rightSideBackground.color = blueColor;

        answer1Text.color = textColor;
        answer2Text.color = textColor;

        if (currentQuestionIndex == 13)
        {
            inkblotPicture.SetActive(true);
        }
        else
        {
            inkblotPicture.SetActive(false);
        }

        if (currentQuestionIndex == 18)
        {
            drinkStandPicture.SetActive(true);
            chainsawPicture.SetActive(true);

            drinkStandImage.color = imageColorWhite;
            chainsawImage.color = imageColorWhite;

        }
        else
        {
            drinkStandPicture.SetActive(false);
            chainsawPicture.SetActive(false);
        }

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
        //playerStats.DisplayStats();
        OnQuestionnaireEnded?.Invoke();
    }

    public void UpdateSelectorPosition(float _difference)
    {



        if (_difference >= 0)
        {
            _difference = Mathf.Clamp(_difference, 0, maxSwipeDistance);

            rightSideBackground.color = blueColor;
            answer2Text.color = textColor;


            float t = _difference / maxSwipeDistance;

            Color newColor = Color.Lerp(redColor, blueColor, t);

            leftSideBackground.color = newColor;

            Color newTextColor = Color.Lerp(textColor, transparentTextColor, t);

            answer1Text.color = newTextColor;

            if (currentQuestionIndex == 18)
            {
                chainsawImage.color = imageColorWhite;

                Color imageColor = Color.Lerp(imageColorWhite, imageColorTransparent, t);
                drinkStandImage.color = imageColor;

            }

                //leftAnswerRect.sizeDelta = new Vector2(startingTextBoxWidth + Mathf.Abs(_difference), leftAnswerRect.rect.height);
                // rightAnswerRect.sizeDelta = new Vector2(startingTextBoxWidth - Mathf.Abs(_difference), rightAnswerRect.rect.height);

        }
        else if (_difference < 0)
        {
            _difference = Mathf.Clamp(_difference, -maxSwipeDistance, 0);

            leftSideBackground.color = redColor;
            answer1Text.color = textColor;


            float t = Mathf.Abs(_difference / maxSwipeDistance);

            Color newColor = Color.Lerp(blueColor, redColor, t);

            rightSideBackground.color = newColor;

            Color newTextColor = Color.Lerp(textColor, transparentTextColor, t);

            answer2Text.color = newTextColor;

            if (currentQuestionIndex == 18)
            {
                drinkStandImage.color = imageColorWhite;

                Color imageColor = Color.Lerp(imageColorWhite, imageColorTransparent, t);
                chainsawImage.color = imageColor;
            }

            // leftAnswerRect.sizeDelta = new Vector2(startingTextBoxWidth - Mathf.Abs(_difference), leftAnswerRect.rect.height);
            //rightAnswerRect.sizeDelta = new Vector2(startingTextBoxWidth + Mathf.Abs(_difference), rightAnswerRect.rect.height);
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
        UpdateAnswerDisplayOpacities(0f, 1f);
        inputBlocker.SetActive(true);
        playerStats.AddTallies(allQuestions[currentQuestionIndex].leftAnswerTallies);
        CheckForDealBreaker(true);
        currentQuestionIndex++;
        CheckForNextQuestion();
    }

    public void SwipedRight()
    {
        UpdateAnswerDisplayOpacities(1f, 0f);
        inputBlocker.SetActive(true);
        playerStats.AddTallies(allQuestions[currentQuestionIndex].rightAnswerTallies);
        CheckForDealBreaker(false);
        currentQuestionIndex++;
        CheckForNextQuestion();

    }

    public void CheckForDealBreaker(bool _swipeLeft)
    {
        switch (currentQuestionIndex)
        {
            case 2:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Jaguar);
                    playerStats.AddDealBreakerAnimal(Animals.Jaguar);
                }
                break;

            case 3:
                if (!_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Spider);
                    playerStats.AddDealBreakerAnimal(Animals.Spider);
                }
                break;

            case 4:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Catfish);
                    playerStats.AddDealBreakerAnimal(Animals.Catfish);
                }
                break;

            case 5:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Skunk);
                    playerStats.AddDealBreakerAnimal(Animals.Skunk);
                }
                break;

            case 6:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Badger);
                    playerStats.AddDealBreakerAnimal(Animals.Badger);
                }
                break;

            case 7:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Hippo);
                    playerStats.AddDealBreakerAnimal(Animals.Hippo);
                }
                break;

            case 8:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Ants);
                    playerStats.AddDealBreakerAnimal(Animals.Ants);
                }
                break;

            case 9:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Bunny);
                    playerStats.AddDealBreakerAnimal(Animals.Bunny);
                }
                break;

            case 10:
                if (!_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Crocodile);
                    playerStats.AddDealBreakerAnimal(Animals.Crocodile);
                }
                break;

            case 11:
                if (!_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Dolphin);
                    playerStats.AddDealBreakerAnimal(Animals.Dolphin);
                }
                break;

            case 12:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Snake);
                    playerStats.AddDealBreakerAnimal(Animals.Snake);
                }
                break;

            case 13:
                if (!_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Octopus);
                    playerStats.AddDealBreakerAnimal(Animals.Octopus);
                }
                break;

            case 14:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Dog);
                    playerStats.AddDealBreakerAnimal(Animals.Dog);
                }
                break;

            case 16:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Gorilla);
                    playerStats.AddDealBreakerAnimal(Animals.Gorilla);
                }
                break;

            case 17:
                if (!_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Shark);
                    playerStats.AddDealBreakerAnimal(Animals.Shark);
                }
                break;

            case 18:
                if (_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Squirrel);
                    playerStats.AddDealBreakerAnimal(Animals.Squirrel);
                }
                break;

            case 19:
                if (!_swipeLeft)
                {
                    pictureManager.RemoveProfile(Animals.Krill);
                    playerStats.AddDealBreakerAnimal(Animals.Krill);
                }
                break;
        }
    }


}
