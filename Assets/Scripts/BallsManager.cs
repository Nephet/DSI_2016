using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallsManager : MonoBehaviour {

	#region Singleton
	static private BallsManager s_Instance;
	static public BallsManager instance
	{
		get
		{
			return s_Instance;
		}
	}

	void Awake()
	{
		if (s_Instance == null)
			s_Instance = this;
		//DontDestroyOnLoad (this);
	}

	#endregion

	public List<GameObject> balls;

	void Start()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Ball");
		foreach (GameObject go in temp)
		{
			balls.Add (go); 
		}

	}
}
