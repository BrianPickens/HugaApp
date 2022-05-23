using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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

    //intro Text 5
    [SerializeField] private Animator introText5Animator;
    [SerializeField] private AnimatorCallback introText5AnimatorCallback;
    [SerializeField] private GameObject introText5;

    //match manager
    [SerializeField] private MatchManager matchManger;

    //intro Text 6a
    [SerializeField] private Animator introText6aAnimator;
    [SerializeField] private AnimatorCallback introText6aAnimatorCallback;
    [SerializeField] private GameObject introText6a;

    //intro Text 6b
    [SerializeField] private Animator introText6bAnimator;
    [SerializeField] private AnimatorCallback introText6bAnimatorCallback;
    [SerializeField] private GameObject introText6b;

    //statsScreen
    [SerializeField] private StatsScreen statsScreen;

    [SerializeField] private PlayerStats playerStats;


    private void Start()
    {
        introText1AnimatorCallback.OnAnimatorComplete = EndIntroText1;
        introText2AnimatorCallback.OnAnimatorComplete = EndIntroText2;
        introText3AnimatorCallback.OnAnimatorComplete = EndIntroText3;
        questions.OnQuestionnaireEnded = QuestionnaireEnd;
        introText4AnimatorCallback.OnAnimatorComplete = EndIntroText4;
        pictureManager.OnSwipingEnd = SwipingEnd;
        introText5AnimatorCallback.OnAnimatorComplete = EndIntroText5;
        matchManger.OnSwipingEnd = MatchesEnd;
        introText6aAnimatorCallback.OnAnimatorComplete = EndIntroText6a;
        introText6bAnimatorCallback.OnAnimatorComplete = EndIntroText6b;

        introText1.SetActive(true);
        //EndIntroText4();
       // EndIntroText5();
    }

    public void ClickThroughIntroText1()
    {
        introText1Animator.SetTrigger("Exit");
        SoundManager.Instance.PlayClickSound();
    }

    private void EndIntroText1()
    {
        introText1.SetActive(false);
        introText2.SetActive(true);
    }

    public void ClickThroughIntroText2()
    {
        introText2Animator.SetTrigger("Exit");
        SoundManager.Instance.PlayClickSound();
    }

    private void EndIntroText2()
    {
        introText2.SetActive(false);
        introText3.SetActive(true);
    }

    public void ClickThroughIntroText3()
    {
        introText3Animator.SetTrigger("Exit");
        SoundManager.Instance.PlayClickSound();
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
        SoundManager.Instance.PlayClickSound();
    }

    private void SwipingEnd()
    {
        pictureManager.HidePictureHolder();
        introText5.SetActive(true);
    }

    public void ClickThroughIntroText5()
    {
        introText5Animator.SetTrigger("Exit");
        SoundManager.Instance.PlayClickSound();
    }

    private void EndIntroText5()
    {
        introText5.SetActive(false);
        matchManger.StartMatches();
    }

    private void EndIntroText4()
    {
        introText4.SetActive(false);
        pictureManager.StartSwiping();
    }

    private void MatchesEnd()
    {
        matchManger.HideMatches();
        if (playerStats.GetNumHugAccepts() > 0)
        {
            introText6a.SetActive(true);
        }
        else
        {
            introText6b.SetActive(true);
        }
    }

    public void ClickThroughIntroText6a()
    {
        introText6aAnimator.SetTrigger("Exit");
        SoundManager.Instance.PlayClickSound();
    }

    public void ClickThroughIntroText6b()
    {
        introText6bAnimator.SetTrigger("Exit");
        SoundManager.Instance.PlayClickSound();
    }

    private void EndIntroText6a()
    {
        introText6a.SetActive(false);
        statsScreen.ShowStatsScreen();
    }

    private void EndIntroText6b()
    {
        introText6a.SetActive(false);
        statsScreen.ShowStatsScreen();
    }

    public void RestartGame()
    {
        SoundManager.Instance.PlayClickSound();
        SceneManager.LoadScene("SampleScene");
    }
}
