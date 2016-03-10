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
	public int speedVolley = 450;
	public float modifierPass;

    public float powerLevelDropDelay = 2f;

    public float speedDropDelay = .5f;
    public int speedDropAmount = 5;

    public float throwMinVelocity = .5f;

    void Start()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Ball");
		foreach (GameObject go in temp)
		{
            if (!balls.Contains(go))
            {
                balls.Add(go);
            }
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
