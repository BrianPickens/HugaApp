using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    //intro Text 1
    [SerializeField] private Animator introText1Animator;
    [SerializeField] private AnimatorCallback introText1AnimatorCallback;
    [SerializeField] private GameObject introText1;

    //intro Text 2
    [SerializeField] private Animator introText2Animator;
    [SerializeField] private AnimatorCallback introText2AnimatorCallback;
    [SerializeField] private GameObject introText2;

    //intro Text 3
    [SerializeField] private Animator introText3Animator;
    [SerializeField] private AnimatorCallback introText3AnimatorCallback;
    [SerializeField] private GameObject introText3;


    //Questionnaire
    [SerializeField] private Questionnaire questions;

    //intro Text 4
    [SerializeField] private Animator introText4Animator;
    [SerializeField] private AnimatorCallback introText4AnimatorCallback;
    [SerializeField] private GameObject introText4;


    //picture movement
    [SerializeField] private PictureManager pictureManager;

    //intro Text 4
    [SerializeField] private Animator introText5Animator;
    [SerializeField] private AnimatorCallback introText5AnimatorCallback;
    [SerializeField] private GameObject introText5;

    private void Start()
    {
        introText1AnimatorCallback.OnAnimatorComplete = EndIntroText1;
        introText2AnimatorCallback.OnAnimatorComplete = EndIntroText2;
        introText3AnimatorCallback.OnAnimatorComplete = EndIntroText3;
        questions.OnQuestionnaireEnded = QuestionnaireEnd;
        introText4AnimatorCallback.OnAnimatorComplete = EndIntroText4;
        pictureManager.OnSwipingEnd = SwipingEnd;
        introText5AnimatorCallback.OnAnimatorComplete = EndIntroText5;

        //introText1.SetActive(true);
        EndIntroText4();
    }

    public void ClickThroughIntroText1()
    {
        introText1Animator.SetTrigger("Exit");
    }

    private void EndIntroText1()
    {
        introText1.SetActive(false);
        introText2.SetActive(true);
    }

    public void ClickThroughIntroText2()
    {
        introText2Animator.SetTrigger("Exit");
    }

    private void EndIntroText2()
    {
        introText2.SetActive(false);
        introText3.SetActive(true);
    }

    public void ClickThroughIntroText3()
    {
        introText3Animator.SetTrigger("Exit");
    }

    private void EndIntroText3()
    {
        introText3.SetActive(false);
        StartQuestionnaire();
    }

    private void StartQuestionnaire()
    {
        questions.StartQuestionnaire();
    }

    private void QuestionnaireEnd()
    {
        questions.HideQuestionnaire();
        introText4.SetActive(true);
    }

    public void ClickThroughIntroText4()
    {
        introText4Animator.SetTrigger("Exit");
    }

    private void SwipingEnd()
    {
        pictureManager.HidePictureHolder();
        introText5.SetActive(true);
    }

    public void ClickThroughIntroText5()
    {
        introText5Animator.SetTrigger("Exit");
    }

    private void EndIntroText5()
    {
        introText5.SetActive(false);
        //go to matches
    }

    private void EndIntroText4()
    {
        introText4.SetActive(false);
        pictureManager.StartSwiping();
    }
}
