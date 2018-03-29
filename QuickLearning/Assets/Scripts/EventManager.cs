using UnityEngine;
using System.Collections;

public class EventManager : SingletonBehaviour<EventManager>
{

    #region Enum
    public enum EventStatus{
        Idle,
        ResetGameCountDown,
        OnGamePrepare,
        OnGameStart,
        OnQuizTimeout,
        OnGameEnd
    }
#endregion

    #region Parameter

    private static bool objCreated = false;

    public delegate void StatusChange(EventStatus message);

	public static event StatusChange OnStatusChange;

	#endregion

	#region LifeCycle

	void Awake ()
	{
		if (!objCreated) {
			objCreated = true;
			_instance = this;
			DontDestroyOnLoad (_instance);
		} else {
			DestroyImmediate (gameObject);
		}
	}

	#endregion

	#region GameMethod

    public void ChangeStatus (EventStatus status)
	{
        Debug.Log(status.ToString());
		if (OnStatusChange != null)
            OnStatusChange (status);
	}

	#endregion
}
