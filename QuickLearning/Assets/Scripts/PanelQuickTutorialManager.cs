using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelQuickTutorialManager : MonoBehaviour
{
    #region PublicParameter
    public Text title;
    public Text answer;

    #endregion

    #region PrivateParameter
    private ServiceManager.QuizList quizList;
    #endregion

    #region LifeCycle
    private void OnEnable()
    {
        EventManager.OnStatusChange += OnStatusChange;
    }

    private void OnDisable()
    {
        EventManager.OnStatusChange -= OnStatusChange;
    }

    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region PrivateMethod
    private void OnStatusChange(EventManager.EventStatus message)
    {
        switch (message)
        { 
            case EventManager.EventStatus.OnGamePrepare:
                quizList = ServiceManager.instance.GetQuiz();
                break;
            case EventManager.EventStatus.OnGameStart:
                SetQuizByIndex(ApplicationManager.instance.quizIndex);
                break;
            case EventManager.EventStatus.OnQuizTimeout:
                StartCoroutine(OnQuizTimeout(0.01f));
                break;
        }
    }

    IEnumerator OnQuizTimeout(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SetQuizByIndex(ApplicationManager.instance.quizIndex);
    }

    private void SetQuizByIndex(int quizIndex)
    {
        title.text = quizList.result[quizIndex].Question;
        answer.text = "";
        StartCoroutine(ShowAnswer(0.5f,quizIndex));
    }

    private IEnumerator ShowAnswer(float waitTime,int quizIndex)
    {
        yield return new WaitForSeconds(waitTime);
        answer.text = quizList.result[quizIndex].Answer;
    }
    #endregion
}
