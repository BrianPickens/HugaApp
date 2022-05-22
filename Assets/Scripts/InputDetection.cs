using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public enum ChoiceType { Question, Photo }

public class InputDetection : MonoBehaviour, IPointerDownHandler
{

    protected enum PositionCheck { Height, Width }

    protected float startingXPosition;
    protected float startingYPosition;

    protected bool allowChoice;

    [SerializeField] protected RectTransform mainCanvasRect;

    [SerializeField] [Range(0.1f, 0.9f)] protected float HorizontalSelectPercentageThreshold;

    [SerializeField] private PictureManager pictureManager;
    [SerializeField] private Questionnaire questions;

    private ChoiceType currentChoiceType;

    private bool startedTouch;

    [Header("AUDIO CLIPS")]
    [SerializeField] protected AudioClip tapSwipeStartAudioClip;
    [SerializeField] protected AudioClip completeSwipeAudioClip;
    [SerializeField] protected AudioClip cancelSwipeAudioClip;

    public void InitializeChoiceSwipe(ChoiceType _type)
    {
        allowChoice = true;
        currentChoiceType = _type;

        if (currentChoiceType == ChoiceType.Photo)
        {
            pictureManager.UpdatePicturePosition(0f);
            pictureManager.UpdateHugDisplayOpacities(0f, 0f);
        }
        else if (currentChoiceType == ChoiceType.Question)
        {
            questions.UpdateSelectorPosition(0f);
            questions.UpdateAnswerDisplayOpacities(0f, 0f);
        }

    }

    public virtual void Update()
    {
        if (allowChoice)
        {

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startedTouch = true;
                        startingXPosition = ConvertMousePositionToCanvasPosition(touch.position.x, PositionCheck.Width);
                        startingYPosition = ConvertMousePositionToCanvasPosition(touch.position.y, PositionCheck.Height);
                        break;

                    case TouchPhase.Moved:
                        if (allowChoice)
                        {

                            if (!startedTouch)
                            {
                                return;
                            }

                            float canvasXPosition = ConvertMousePositionToCanvasPosition(touch.position.x, PositionCheck.Width);
                            float canvasWidth = mainCanvasRect.rect.width;

                            if (currentChoiceType == ChoiceType.Photo)
                            {
                                pictureManager.UpdatePicturePosition(canvasXPosition - startingXPosition);
                            }
                            else if (currentChoiceType == ChoiceType.Question)
                            {
                                questions.UpdateSelectorPosition(canvasXPosition - startingXPosition);
                            }

                            float targetMovementNeeded = 0f;

                            targetMovementNeeded = canvasWidth * HorizontalSelectPercentageThreshold;

                            float backgroundPercentage = Mathf.Abs((canvasXPosition - startingXPosition) / targetMovementNeeded);
                            // storyUIController.UpdateChoiceBackgroundOpacities(Mathf.Clamp01(1f - backgroundPercentage));

                            if (canvasXPosition > startingXPosition)
                            {
                                float percentage = Mathf.Abs((canvasXPosition - startingXPosition) / targetMovementNeeded);
                                // storyUIController.UpdateRightChoiceOpacity(Mathf.Clamp01(1f - percentage));
                                if (currentChoiceType == ChoiceType.Photo)
                                {
                                    pictureManager.UpdateHugDisplayOpacities(0f, percentage);
                                }
                                else if (currentChoiceType == ChoiceType.Question)
                                {
                                    questions.UpdateAnswerDisplayOpacities(0f, percentage);
                                }
                            }
                            else if (canvasXPosition < startingXPosition)
                            {
                                float percentage = Mathf.Abs((canvasXPosition - startingXPosition) / targetMovementNeeded);
                                // storyUIController.UpdateLeftChoiceOpacity(Mathf.Clamp01(1f - percentage));
                                if (currentChoiceType == ChoiceType.Photo)
                                {
                                    pictureManager.UpdateHugDisplayOpacities(percentage, 0f);
                                }
                                else if (currentChoiceType == ChoiceType.Question)
                                {
                                    questions.UpdateAnswerDisplayOpacities(percentage, 0f);
                                }
                            }
                            else
                            {
                                if (currentChoiceType == ChoiceType.Photo)
                                {
                                    pictureManager.UpdateHugDisplayOpacities(0f, 0f);
                                }
                                else if (currentChoiceType == ChoiceType.Question)
                                {
                                    questions.UpdateAnswerDisplayOpacities(0f, 0f);
                                }
                            }
                        }
                        break;

                    case TouchPhase.Ended:

                        if (allowChoice)
                        {
                            if (!startedTouch)
                            {
                                return;
                            }

                            startedTouch = false;

                            bool choiceMade = false;

                            choiceMade = CheckChoiceThreshold(touch.position);
                            //  storyUIController.UpdateRightChoiceOpacity(1f);
                            //  storyUIController.UpdateLeftChoiceOpacity(1f);
                            //   storyUIController.UpdateChoiceBackgroundOpacities(1f);

                            if (!choiceMade)
                            {
                                if (currentChoiceType == ChoiceType.Photo)
                                {
                                    pictureManager.UpdatePicturePosition(0);
                                    pictureManager.UpdateHugDisplayOpacities(0f, 0f);
                                }
                                else if (currentChoiceType == ChoiceType.Question)
                                {
                                    questions.UpdateSelectorPosition(0);
                                    questions.UpdateAnswerDisplayOpacities(0f, 0f);
                                }

                            }
                            else
                            {
                                //pictureManager.UpdatePicturePosition(0);
                            }
                        }
                        break;
                }
            }
        }
    }

    protected virtual bool CheckChoiceThreshold(Vector2 _currentPosition)
    {
        float endXPosition = ConvertMousePositionToCanvasPosition(_currentPosition.x, PositionCheck.Width);
        float endYPosition = ConvertMousePositionToCanvasPosition(_currentPosition.y, PositionCheck.Height);

        float targetMovementNeeded = 0f;
        float canvasWidth = mainCanvasRect.rect.width;
        float canvasHeight = mainCanvasRect.rect.height;

        bool choiceMade = false;

        targetMovementNeeded = canvasWidth * HorizontalSelectPercentageThreshold;

        if (endXPosition > startingXPosition + targetMovementNeeded)
        {
            allowChoice = false;
            if (currentChoiceType == ChoiceType.Photo)
            {
                pictureManager.SwipedRight();
            }
            else if (currentChoiceType == ChoiceType.Question)
            {
                questions.SwipedRight();
            }
                choiceMade = true;
           // SoundManager.Instance.PlaySFX(completeSwipeAudioClip);
        }
        else if (endXPosition < startingXPosition - targetMovementNeeded)
        {
            allowChoice = false;
            if (currentChoiceType == ChoiceType.Photo)
            {
                pictureManager.SwipedLeft();
            }
            else if (currentChoiceType == ChoiceType.Question)
            {
                questions.SwipedLeft();
            }
            choiceMade = true;
          //  SoundManager.Instance.PlaySFX(completeSwipeAudioClip);
        }
        else
        {
              //  SoundManager.Instance.PlaySFX(cancelSwipeAudioClip);
        }

        return choiceMade;

    }

    protected float ConvertMousePositionToCanvasPosition(float _mousePosition, PositionCheck _checkType)
    {
        if (_checkType == PositionCheck.Width)
        {
            float canvasWidth = mainCanvasRect.rect.width;
            float screenWidth = Screen.width;
            float screenXPos = _mousePosition;
            float canvasXPos = 0f;

            canvasXPos = (canvasWidth * screenXPos) / screenWidth;

            return canvasXPos;
        }
        else if (_checkType == PositionCheck.Height)
        {
            float canvasHeight = mainCanvasRect.rect.height;
            float screenHeight = Screen.height;
            float screenYPos = _mousePosition;
            float canvasYPos = 0f;

            canvasYPos = (canvasHeight * screenYPos) / screenHeight;

            return canvasYPos;
        }
        else
        {
            return 0;
        }



    }

    public void OnPointerDown(PointerEventData eventData)
    {
       // SoundManager.Instance.PlaySFX(tapSwipeStartAudioClip);
    }
    
}
