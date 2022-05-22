using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class PictureManager : MonoBehaviour
{
    [Serializable]
    public struct SwipeProfiles
    {
        public string name;
        public Sprite picture;
        public Animals animalType;
    }

    [SerializeField] private List<SwipeProfiles> allProfiles = new List<SwipeProfiles>();

    private int currentProfileIndex = 0;

    [SerializeField] private RectTransform pictureRect;
    private float startingPictureXPosition;
    [SerializeField] private TextMeshProUGUI profileNameText;

    [SerializeField] private Image profileImage;

    [SerializeField] private GameObject inputBlocker;
    [SerializeField] private Animator profileAnimator;

    [SerializeField] private CanvasGroup leftHugImage;
    [SerializeField] private CanvasGroup rightHugImage;

    [SerializeField] private InputDetection inputDetection;

    [SerializeField] private float maxSwipeDistance;

    [SerializeField] private SafeArea safeArea;

    [SerializeField] private RectTransform leftFeedbackRect;
    [SerializeField] private RectTransform rightFeedbackRect;
    [SerializeField] private CanvasGroup leftFeedbackSide;
    [SerializeField] private CanvasGroup rightFeedbackSide;

    [SerializeField] private Animator feedbackAnimator;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private AnimatorCallback feedbackAnimatorCallback;

    [SerializeField] private PlayerStats playerStats;

    public Action OnSwipingEnd;

    private void Awake()
    {
        startingPictureXPosition = pictureRect.anchoredPosition.x;

    }

    private void Start()
    {
        float safeAreaWidth = safeArea.GetSafeAreaWidth();
        leftFeedbackRect.sizeDelta = new Vector2(safeAreaWidth / 2f, leftFeedbackRect.rect.height);
        rightFeedbackRect.sizeDelta = new Vector2(safeAreaWidth / 2f, rightFeedbackRect.rect.height);
        feedbackAnimatorCallback.OnAnimatorComplete = FeedbackEnd;

        //randomize animal list
        for (int i = 0; i < allProfiles.Count; i++)
        {
            SwipeProfiles tempProfile = allProfiles[i];
            int randomIndex = UnityEngine.Random.Range(i, allProfiles.Count);
            allProfiles[i] = allProfiles[randomIndex];
            allProfiles[randomIndex] = tempProfile;
        }
    }

    public void StartSwiping()
    {
        FillProfileInfo();
        gameObject.SetActive(true);
        inputBlocker.SetActive(true);
    }

    public void HidePictureHolder()
    {
        gameObject.SetActive(false);
    }

    public void StartNextSwipe()
    {
        profileAnimator.SetTrigger("Next");
    }

    public void CheckForNextPicture()
    {
        if (currentProfileIndex >= allProfiles.Count)
        {
            StartEndSwiping();
        }
        else if (allProfiles.Count == 0)
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

        profileImage.sprite = allProfiles[currentProfileIndex].picture;
        profileNameText.text = allProfiles[currentProfileIndex].name;

    }

    public void EndNewProfileIntro()
    {
        inputBlocker.SetActive(false);
        inputDetection.InitializeChoiceSwipe(ChoiceType.Photo);
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
        leftHugImage.alpha = _left;
        rightHugImage.alpha = _right;
        leftFeedbackSide.alpha = _left;
        rightFeedbackSide.alpha = _right;

    }

    public void SwipedLeft()
    {
        UpdateHugDisplayOpacities(1f, 0f);
        inputBlocker.SetActive(true);
        currentProfileIndex++;
        feedbackAnimator.SetTrigger("NoHug");
        feedbackText.text = "No Hug";

    }

    public void SwipedRight()
    {
        UpdateHugDisplayOpacities(0f, 1f);
        inputBlocker.SetActive(true);
        playerStats.AddAnimalMatchAttempt(allProfiles[currentProfileIndex].animalType);
        currentProfileIndex++;
        feedbackAnimator.SetTrigger("Hug");
        feedbackText.text = "Hug";
    }

    public void FeedbackEnd()
    {
        CheckForNextPicture();
    }

}
