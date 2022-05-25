using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MatchManager : MonoBehaviour
{

    [Serializable]
    public struct Match
    {
        public string name;
        public Sprite picture;
        public Animals animalType;
        public string pickupLine;
    }

    [SerializeField] private List<Match> allProfiles = new List<Match>();

    private List<Match> allMatches = new List<Match>();

    private int currentProfileIndex = 0;

    [SerializeField] private RectTransform pictureRect;
    private float startingPictureXPosition;
    [SerializeField] private TextMeshProUGUI profileNameText;

    [SerializeField] private Image profileImage;

    [SerializeField] private GameObject inputBlocker;
    [SerializeField] private Animator profileAnimator;

   // [SerializeField] private CanvasGroup leftHugImage;
   // [SerializeField] private CanvasGroup rightHugImage;

    [SerializeField] private InputDetection inputDetection;

    [SerializeField] private float maxSwipeDistance;

    [SerializeField] private SafeArea safeArea;

    //[SerializeField] private RectTransform leftFeedbackRect;
   // [SerializeField] private RectTransform rightFeedbackRect;
    [SerializeField] private CanvasGroup leftFeedbackSide;
    [SerializeField] private CanvasGroup rightFeedbackSide;

    [SerializeField] private Animator feedbackAnimator;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private AnimatorCallback feedbackAnimatorCallback;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private TextMeshProUGUI pickUpLineText;

    [SerializeField] private CanvasGroup leftArrowCanvas;
    [SerializeField] private CanvasGroup rightArrowCanvas;
    [SerializeField] private CanvasGroup selectorCanvas;

    [SerializeField] private CanvasGroup noHugCanvas;
    [SerializeField] private CanvasGroup hugCanvas;

    [SerializeField] private GameObject fullHeart;
    [SerializeField] private GameObject brokenHeart;

    public Action OnSwipingEnd;

    private void Awake()
    {
        startingPictureXPosition = pictureRect.anchoredPosition.x;

    }

    private void Start()
    {
        float safeAreaWidth = safeArea.GetSafeAreaWidth();
       // leftFeedbackRect.sizeDelta = new Vector2(safeAreaWidth / 2f, leftFeedbackRect.rect.height);
       // rightFeedbackRect.sizeDelta = new Vector2(safeAreaWidth / 2f, rightFeedbackRect.rect.height);
        feedbackAnimatorCallback.OnAnimatorComplete = FeedbackEnd;

    }

    public void StartMatches()
    {

        //randomize animal list
        for (int i = 0; i < allProfiles.Count; i++)
        {
            Match tempProfile = allProfiles[i];
            int randomIndex = UnityEngine.Random.Range(i, allProfiles.Count);
            allProfiles[i] = allProfiles[randomIndex];
            allProfiles[randomIndex] = tempProfile;
        }

        playerStats.DetermineMatches();
        List<Animals> animalMatches = new List<Animals>();

        for (int i = 0; i < playerStats.GetMatches().Count; i++)
        {
            animalMatches.Add(playerStats.GetMatches()[i]);
        }

        for (int i = 0; i < animalMatches.Count; i++)
        {
            for (int j = 0; j < allProfiles.Count; j++)
            {
                if (animalMatches[i] == allProfiles[j].animalType)
                {
                    allMatches.Add(allProfiles[j]);
                }
            }
        }

        if (allMatches.Count == 0 && playerStats.GetNumMatchAttempts() > 0)
        {
            List<Animals> matchAttempts = new List<Animals>();
            matchAttempts = playerStats.GetMatchAttemptsList();

            int randomIndex = UnityEngine.Random.Range(0, matchAttempts.Count);
            for (int i = 0; i < allProfiles.Count; i++)
            {
                if (matchAttempts[randomIndex] == allProfiles[i].animalType)
                {
                    playerStats.AddAnimalMatch(matchAttempts[randomIndex]);
                    allMatches.Add(allProfiles[i]);
                }
            }
        }

        if (allMatches.Count == 0)
        {
            EndSwiping();
            return;
        }

        FillProfileInfo();
        gameObject.SetActive(true);
        inputBlocker.SetActive(true);
    }

    public void HideMatches()
    {
        gameObject.SetActive(false);
    }

    public void StartNextSwipe()
    {
        profileAnimator.SetTrigger("Next");
    }

    public void CheckForNextPicture()
    {
        if (currentProfileIndex >= allMatches.Count)
        {
            StartEndSwiping();
        }
        else if (allMatches.Count == 0)
        {
            StartEndSwiping();
        }
        else
        {
            StartNextSwipe();
        }
    }

    public void EndSwiping()
    {
        OnSwipingEnd?.Invoke();
    }

    public void StartEndSwiping()
    {
        profileAnimator.SetTrigger("End");
    }

    public void FillProfileInfo()
    {
        UpdateHugDisplayOpacities(0f, 0f);
        UpdatePicturePosition(0f);

        profileImage.sprite = allMatches[currentProfileIndex].picture;
        profileNameText.text = NamesAndLines.Instance.GetAnimalName(allMatches[currentProfileIndex].animalType);
        pickUpLineText.text = NamesAndLines.Instance.GetAnimalLine(allMatches[currentProfileIndex].animalType);

    }

    public void EndNewProfileIntro()
    {
        inputBlocker.SetActive(false);
        inputDetection.InitializeChoiceSwipe(ChoiceType.Match);
    }


    public void UpdatePicturePosition(float _difference)
    {
        if (_difference > 0)
        {
            _difference = Mathf.Clamp(_difference, 0, maxSwipeDistance);
        }

        if (_difference < 0)
        {
            _difference = Mathf.Clamp(_difference, -maxSwipeDistance, 0);
        }

        pictureRect.anchoredPosition = new Vector2(startingPictureXPosition + _difference, pictureRect.anchoredPosition.y);
    }

    public void UpdateHugDisplayOpacities(float _left, float _right)
    {
        //leftHugImage.alpha = _left;
       // rightHugImage.alpha = _right;
        leftFeedbackSide.alpha = _left;
        rightFeedbackSide.alpha = _right;

        noHugCanvas.alpha = 1f - _right;
        hugCanvas.alpha = 1f - _left;

        float newAlpha = 0f;
        if (_left >= _right)
        {
            newAlpha = _left;
        }
        else
        {
            newAlpha = _right;
        }

        leftArrowCanvas.alpha = 1f - newAlpha;
        rightArrowCanvas.alpha = 1f - newAlpha;
        selectorCanvas.alpha = 1f - newAlpha;

    }

    public void SwipedLeft()
    {
        UpdateHugDisplayOpacities(1f, 0f);
        inputBlocker.SetActive(true);
        playerStats.AddHugRejects(allMatches[currentProfileIndex].animalType);
        currentProfileIndex++;
        feedbackAnimator.SetTrigger("NoHug");
        brokenHeart.SetActive(true);
        fullHeart.SetActive(false);
    }

    public void SwipedRight()
    {
        UpdateHugDisplayOpacities(0f, 1f);
        inputBlocker.SetActive(true);
        playerStats.AddHugAccept(allMatches[currentProfileIndex].animalType);
        currentProfileIndex++;
        feedbackAnimator.SetTrigger("Hug");
        fullHeart.SetActive(true);
        brokenHeart.SetActive(false);
    }

    public void FeedbackEnd()
    {
        CheckForNextPicture();
    }



}
