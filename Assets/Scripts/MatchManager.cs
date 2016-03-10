﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ParticlePlayground;

public class MatchManager : MonoBehaviour {

    static MatchManager instance;

    public int teamOneScore = 0;
    public int teamTwoScore = 0;

    public int normalBallPoints = 1;
    public int playerBallPoints = 5;
    public int ennemyBallPoints = 8;

    public float timerDuration = 5f;
    public float timer;
    float _timerStart;
	public bool respawning;
	public int direction = 1;

    public float publicFeverTeam1 = 50f;
    public float publicFeverTeam2 = 50f;
    public float feverMax = 100f;
    public float feverIncreaseDelay = 0.1f;
    public float feverDecreaseDelay = 0.1f;
    float lastTeam1Increase = 0f;
    float lastTeam2Increase = 0f;
    float lastTeamDecrease = 0f;

    public bool pause = false;
	public bool endGame = false;
	bool _pauseButton;

	public float slow;

    public bool slowmo = false;

    //[HideInInspector]
    public GameObject player1;
	//[HideInInspector]
	public GameObject player2;
	//[HideInInspector]
	public GameObject spawnBall1;
	//[HideInInspector]
	public GameObject spawnBall2;
	//[HideInInspector]
	public GameObject respawnGoal1;
	//[HideInInspector]
	public GameObject respawnGoal2;
	//[HideInInspector]
	public GameObject spawnPlayer11;
	//[HideInInspector]
	public GameObject spawnPlayer21;
	//[HideInInspector]
	public GameObject spawnPlayer12;
	//[HideInInspector]
	public GameObject spawnPlayer22;
	//[HideInInspector]
	public GameObject center;

	public GameObject prefabPlayer;

	public GameObject panelVictory;

	public GameObject panelPause;

	public GameObject timerUI;
	public GameObject scoreTeam1UI;
	public GameObject scoreTeam2UI;

	bool _playMusic = false;

	float width = 3f;
	float height = 3f;
	float respawnSpeed = 3f;

    public float timeBeforeBooing = 5f;

    public float slowMoDuration = 0.9f;
    public float slowMoPower = 0.1f;

    [HideInInspector]
    public bool prolongation = false;

	[Header("Particles")]
	public GameObject partRespawn;
    public GameObject partVictory;
    public GameObject victoryTriggerPosition1;
    public GameObject victoryTriggerPosition2;
    public GameObject victoryTriggerPosition3;
    public GameObject victoryTriggerPosition4;
    public GameObject victoryTriggerPosition5;
    public GameObject playgroundManager;

    public static MatchManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;

        timer = timerDuration;

        _timerStart = Time.time;

		publicFeverTeam1 = 50;
		publicFeverTeam2 = 50;


    }

	void Start()
	{
		Spawn ();
	}

    void Update()
    {
		if (!_playMusic) {

			SoundManagerEvent.emit (SoundManagerType.GAMEMUSIC);
			SoundManagerEvent.emit (SoundManagerType.CROWDMUSIC);
			_playMusic = true;
		}
		
		if (Input.GetButtonDown ("Start_1") || Input.GetButtonDown ("Start_2") || Input.GetButtonDown ("Start_3") ||Input.GetButtonDown ("Start_4")) {
			pause = !pause;
		}

		if (pause) {
			Time.timeScale = 0f;
			panelPause.SetActive (true);

		}
        else if(!slowmo)
        {
			panelPause.SetActive (false);
			Time.timeScale = 1f;
		}
        
		if (pause || endGame)
			return;
		
        timer = timerDuration - (Time.time - _timerStart);

		UpdateUI ();

        if(timer <= 0 && teamOneScore != teamTwoScore)
        {
			EndGame();
        }
        else if(timer <= 0 && teamOneScore == teamTwoScore)
        {
            timer = 0;
            
            if (!prolongation)
            {
                prolongation = true;

                GameObject go = null;

				foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
                {
                    if (!ball.GetComponent<PlayerActions>())
                    {
                        go = ball;
                    }
                }

                BallsManager.instance.RemoveBall(go);
                Destroy(go);
            }
            
        }
    }

    public void StartSlowMo(float duration)
    {
        if (!MatchManager.Instance.slowmo)
        {
            slowmo = true;
            Time.timeScale *= slowMoPower;
            Time.fixedDeltaTime *= slowMoPower;
            Invoke("StopSlowMo", duration * slowMoPower);
        }
    }

    public void StopSlowMo()
    {
        slowmo = false;

        Time.timeScale /= slowMoPower;
        Time.fixedDeltaTime /= slowMoPower;
    }

    void UpdateUI()
	{
		int _tempMin = prolongation ? 0 : (int)timer / 60;
		int _tempSec = prolongation ? 0 : ((int)timer % 60);


		timerUI.GetComponent<Text>().text = _tempMin+" : "+_tempSec;
		scoreTeam1UI.GetComponent<Text>().text = teamOneScore+"";
		scoreTeam2UI.GetComponent<Text> ().text = teamTwoScore+"";
	}

	void EndGame()
	{
		SoundManagerEvent.emit (SoundManagerType.ENDGAME);
        // Particles victory
        StartCoroutine(VictoryParticles());
        panelVictory.SetActive (true);
		SoundManagerEvent.emit (SoundManagerType.WHISTLEEND);
		SoundManagerEvent.emit (SoundManagerType.BASSEND);
		//pause = true;
		endGame = true;
		CheckTeamVictory ();
	}

	void CheckTeamVictory()
	{
		panelVictory.GetComponentInChildren<Text>().text = teamOneScore +" / "+ teamTwoScore;
	}

    public void AddPoint(int id, int score)
    {
        if(id == 1)
        {
            teamOneScore += score;
        }
        else if(id == 2)
        {
            teamTwoScore += score;
        }
    }

	void Spawn()
	{
		panelVictory.SetActive (false);
		panelPause.SetActive (false);

		GameObject firstBall = Instantiate (Resources.Load ("Prefabs/Ball"), spawnBall1.transform.position, Quaternion.identity) as GameObject;
		BallsManager.instance.balls.Add (firstBall);

		for (int i = 1; i < 5; i++) 
		{
			GameObject _player = Instantiate (prefabPlayer) as GameObject;
			GameObject _mask = Instantiate (SelectionManager.instance.chooseMask [i]) as GameObject;
			_player.transform.localScale *= 0.3f;

			_mask.transform.parent = _player.GetComponent<Movement>().head.transform;

			_mask.transform.localPosition = Vector3.zero;
            _mask.transform.localEulerAngles = Vector3.zero;
            _mask.transform.localScale = Vector3.one;

            _mask.GetComponentInChildren<Renderer>().material.mainTexture = SelectionManager.instance.currentTexture[i];

            _player.GetComponent<PlayerActions> ().id = i;
			_player.GetComponent<PlayerActions> ().teamId = SelectionManager.instance.currentTeam[i];
			_player.GetComponent<Movement>().meshBall.GetComponent<Renderer>().material.mainTexture = _player.GetComponent<PlayerActions>().teamId == 1 ? SelectionManager.instance.textureBallTeam1 : SelectionManager.instance.textureBallTeam2;

            _player.GetComponent<Movement>().body.GetComponent<Renderer>().material.mainTexture = _player.GetComponent<PlayerActions>().teamId == 1 ? SelectionManager.instance.textureTeam1 : SelectionManager.instance.textureTeam2;

            PlayerManager.instance.AddPlayer(_player);

		}

		int nbSpawn1 = 0;
		int nbSpawn2 = 0;

		for (int i = 0; i < 4; i++) {
			if (PlayerManager.instance.listPlayers [i].GetComponent<PlayerActions> ().teamId == 1) {
				if (nbSpawn1 == 0) {
					PlayerManager.instance.listPlayers [i].transform.position = spawnPlayer11.transform.position;
				} else {
					PlayerManager.instance.listPlayers [i].transform.position = spawnPlayer12.transform.position;
				}
				nbSpawn1++;
			}else{
				if (nbSpawn2 == 0) {
					PlayerManager.instance.listPlayers [i].transform.position = spawnPlayer21.transform.position;
				} else {
					PlayerManager.instance.listPlayers [i].transform.position = spawnPlayer22.transform.position;
				}
				nbSpawn2++;
			}
			PlayerManager.instance.listPlayers [i].GetComponent<Movement>().mesh.transform.LookAt (new Vector3(center.transform.position.x, PlayerManager.instance.listPlayers [i].transform.position.y, center.transform.position.z));
		}

	}

	public void Respawn(int _id, bool b)
    {
        if (prolongation) return;

        StartCoroutine (CountDownRespawnBall (_id,b));
	}

	public void RespawnPlayer(GameObject _player)
	{

		_player.transform.parent = null;
		_player.SetActive (false);
		if (_player.GetComponent<PlayerActions> ().teamId == 1) {
			if (direction > 0) {
				_player.transform.position = spawnPlayer11.transform.position;
			} 
			else 
			{
				_player.transform.position = spawnPlayer12.transform.position;
			}

		} 
		else 
		{
			if (direction > 0) {
				_player.transform.position = spawnPlayer21.transform.position;
			} 
			else 
			{
				_player.transform.position = spawnPlayer22.transform.position;
			}

		}



		_player.GetComponent<PlayerActions> ().SetToBall (false);
		StartCoroutine (CountDownRespawnPlayer (_player));

	}

	IEnumerator CountDownRespawnBall(int _id, bool b)
	{
		yield return new WaitForSeconds (2.0f);
		GameObject myGo;

		if (_id == 1) {
			myGo = Instantiate (Resources.Load ("Prefabs/Ball"), respawnGoal1.transform.position, respawnGoal1.transform.rotation) as GameObject;
		} 
		else 
		{
			myGo = Instantiate (Resources.Load ("Prefabs/Ball"), respawnGoal2.transform.position, respawnGoal2.transform.rotation) as GameObject;
		}

		BallsManager.instance.AddBall(myGo);
		myGo.GetComponent<Ball> ().respawning = true;
		myGo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		//myGo.GetComponent<Ball> ().LaunchCoroutine ();
		myGo.GetComponent<Rigidbody>().AddForce(myGo.transform.right * 300f);

        if (b)
        {
            Destroy(myGo, PinataManager.instance.destroyBallDelay);
        }
	}

	IEnumerator CountDownRespawnPlayer(GameObject _player)
	{
		yield return new WaitForSeconds (2.0f);

		// Particles
		GameObject _partClone = Instantiate(partRespawn, _player.transform.position, Quaternion.identity) as GameObject;
        _partClone.transform.SetParent(playgroundManager.transform);
		Destroy (_partClone, 5f);

		_player.transform.LookAt (new Vector3(center.transform.position.x, 0f, center.transform.position.z));
		_player.GetComponent<Ball> ().currentPowerLevel = 0;
		_player.GetComponent<PlayerActions> ().state = PlayerActions.State.HUMAN;
		_player.SetActive (true);
        _player.GetComponent<Rigidbody>().isKinematic = false;
	}

    public void IncreaseFever(int id)
    {
        if(id == 1 && Time.time - lastTeam1Increase > feverIncreaseDelay)
        {
            publicFeverTeam1++;
			publicFeverTeam2--;
            lastTeam1Increase = Time.time;

            if (publicFeverTeam1 >= feverMax)
            {
                publicFeverTeam1 = 50;
				publicFeverTeam2 = 50;
                PinataManager.instance.CheckEffect(id);
				SoundManagerEvent.emit (SoundManagerType.FEVERMAX);
            }
        }
        else if(id == 2 && Time.time - lastTeam2Increase > feverIncreaseDelay)
        {
            publicFeverTeam2++;
			publicFeverTeam1--;
            lastTeam2Increase = Time.time;

            if (publicFeverTeam2 >= feverMax)
            {
                publicFeverTeam2 = 50;
				publicFeverTeam1 = 50;
                PinataManager.instance.CheckEffect(id);
				SoundManagerEvent.emit (SoundManagerType.FEVERMAX);
            }
        }
    }

    IEnumerator VictoryParticles()
    {
        TriggerParticles();
        yield return new WaitForSeconds(1.5f);
        TriggerParticles();
        yield return new WaitForSeconds(1.0f);
        TriggerParticles();
        yield return new WaitForSeconds(0.5f);
        TriggerParticles();
    }

    void TriggerParticles()
    {
        GameObject _partClone1 = Instantiate(partVictory, victoryTriggerPosition1.transform.position, Quaternion.identity) as GameObject;
        GameObject _partClone2 = Instantiate(partVictory, victoryTriggerPosition2.transform.position, Quaternion.identity) as GameObject;
        GameObject _partClone3 = Instantiate(partVictory, victoryTriggerPosition3.transform.position, Quaternion.identity) as GameObject;
        GameObject _partClone4 = Instantiate(partVictory, victoryTriggerPosition4.transform.position, Quaternion.identity) as GameObject;
        GameObject _partClone5 = Instantiate(partVictory, victoryTriggerPosition5.transform.position, Quaternion.identity) as GameObject;

        Destroy(_partClone1, 2f);
        Destroy(_partClone2, 2f);
        Destroy(_partClone3, 2f);
        Destroy(_partClone4, 2f);
        Destroy(_partClone5, 2f);
    }
}