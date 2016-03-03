using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	
	#region Singleton
	static private PlayerManager s_Instance;
	static public PlayerManager instance
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

	public float throwPower = 5f;

	public float dashPower = 7.5f;
	public float dashDuration = 0.5f;
	public float dashCooldown = 1f;

	public float speed = 5f;
	public float speedInBall = .5f;

	public float rotationSpeed = 5;

	public List<GameObject> listPlayers;

	public void AddPlayer(GameObject _player)
	{
		listPlayers.Add(_player);
	}

	public void RemovePlayer(GameObject _player)
	{
		listPlayers.Remove(_player);
	}
}
