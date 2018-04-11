using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region PublicParameter
    public AudioClip impact;
    #endregion

    #region PrivateParameter
    AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }
    #endregion

    #region PublicMethod

    #endregion

    #region PrivateMethod
    private void OnStatusChange(EventManager.EventStatus message)
    {
        switch (message)
        {
            case EventManager.EventStatus.PlayAnswerSound:
                PlayAnswerSound();
                break;
        }
    }

    private void PlayCollectSound() {
    
    }

    private void PlayAnswerSound() { 
        audioSource.PlayOneShot(impact, 0.7F);
    }
    #endregion
}