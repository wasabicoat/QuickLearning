using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ApplicationManager : SingletonBehaviour<ApplicationManager>
{

    #region PublicParameter
    public int actualScore;
    public float countdownQuestion;
    public bool isIngame;
    public float prepareCountdown;
    public int quizIndex;
    public int quizQuantity;
    //public List<Quiz> quizList;
    #endregion

    #region PrivateParameter

    private bool objCreate = false;

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
        if (!objCreate)
        {
            objCreate = true;
            _instance = this;
            DontDestroyOnLoad(instance);
            ResetParameter();
            InitQuestion();
            EventManager.instance.ChangeStatus(EventManager.EventStatus.OnGamePrepare);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
#endif

        if (isIngame)
        {
            if (countdownQuestion < 0)
            {
                EventManager.instance.ChangeStatus(EventManager.EventStatus.ResetGameCountDown);
            }
            else
            {
                countdownQuestion -= Time.deltaTime;
            }

        }
        else if (prepareCountdown < 0 && !isIngame)
        {
            EventManager.instance.ChangeStatus(EventManager.EventStatus.OnGameStart);
        }
        else
        {
            prepareCountdown -= Time.deltaTime;
        }
    }
    #endregion

    #region PublicMethod


    #endregion

    #region PrivateMethod

    private void InitQuestion()
    {
        
    }

    private void ResetParameter()
    {
        actualScore = 0;
        countdownQuestion = 1f;
        isIngame = false;
        prepareCountdown = 3f;
        quizIndex = 0;
    }

    private void ResetGamecountdown()
    {
        countdownQuestion = 1f;
        EventManager.instance.ChangeStatus(EventManager.EventStatus.OnQuizTimeout);
    }

    private void OnStatusChange(EventManager.EventStatus message)
    {
        switch (message)
        {
            case EventManager.EventStatus.ResetGameCountDown:
                ResetGamecountdown();
                break;
            case EventManager.EventStatus.OnGamePrepare:
                OnGamePrepare();
                break;
            case EventManager.EventStatus.OnGameStart:
                OnGameStart();
                break;
            case EventManager.EventStatus.OnGameEnd:
                OnGameEnd();
                break;
            case EventManager.EventStatus.OnQuizTimeout:
                OnQuizTimeout();
                break;
        }
    }

    private void OnQuizTimeout()
    {
        if (quizIndex < quizQuantity)
        {
            quizIndex += 1;
        }else{
            quizIndex = 0;
        }
    }

    private void OnGamePrepare()
    {
        ResetParameter();
    }

    private void OnGameEnd()
    {
        isIngame = false;
    }

    private void OnGameStart()
    {
        isIngame = true;
    }

    #endregion

}



