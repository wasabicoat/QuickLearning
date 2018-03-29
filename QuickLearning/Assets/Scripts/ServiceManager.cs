using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServiceManager : SingletonBehaviour<ServiceManager>
{
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

    private void Start()
    {
        if (!objCreate)
        {
            objCreate = true;
            _instance = this;
            DontDestroyOnLoad(instance);
            //GetQuiz();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    #endregion

    #region PublicMethod

    public QuizList GetQuiz()
    {
        var path = Application.dataPath + "/StreamingAssets/Quiz.json";
        string text = System.IO.File.ReadAllText(path);

        var rootQuiz = JsonUtility.FromJson<QuizList>(text);
        ApplicationManager.instance.quizQuantity = rootQuiz.result.Count;
        return rootQuiz;
    }

    #endregion

    #region PrivateMethod
    private void OnStatusChange(EventManager.EventStatus message)
    {
        switch (message)
        {
            case EventManager.EventStatus.ResetGameCountDown:
                break;
            
        }
    }
#endregion

    #region JsonClass

    [Serializable]
    public class Quiz
    {
        public string Question;
        public string Answer;
    }

    [Serializable]
    public class QuizList
    {
        public List<Quiz> result;
        public int status;
    }
    #endregion
}
