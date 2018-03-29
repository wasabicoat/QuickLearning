using UnityEngine;
using System.Collections;

public class ApplicationManager : SingletonBehaviour<ApplicationManager>
{

	#region PublicParameter


	#endregion

	#region PrivateParameter

	private bool objCreate = false;

	#endregion

	#region LifeCycle

	// Use this for initialization
	void Start ()
	{
		if (!objCreate) {
			objCreate = true;
			_instance = this;
			DontDestroyOnLoad (instance);
		} else {
			DestroyImmediate (gameObject);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.R)) {
			PlayerPrefs.DeleteAll ();
		}
		#endif
	}

	#endregion

	#region PublicMethod


	#endregion

}
