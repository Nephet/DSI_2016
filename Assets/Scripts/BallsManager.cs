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
	public float friction = 0.8f;

    public int[] speedMaxByPowerLevel;

    public float powerLevelDropDelay = 2f;

    public float speedDropDelay = .5f;
    public int speedDropAmount = 5;

    void Start()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Ball");
		foreach (GameObject go in temp)
		{
			balls.Add (go); 
		}

	}

    public void AddBall(GameObject ball)
    {
        balls.Add(ball);
    }

    public void RemoveBall(GameObject ball)
    {
        balls.Remove(ball);
    }

    
}
