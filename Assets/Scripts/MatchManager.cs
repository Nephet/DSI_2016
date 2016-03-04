using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public float publicFeverTeam1 = 0f;
    public float publicFeverTeam2 = 0f;
    public float feverMax = 100f;
    public float feverIncreaseDelay = 0.1f;
    float lastTeam1Increase = 0f;
    float lastTeam2Increase = 0f;

	public bool pause = false;
	bool _pauseButton;

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

	float width = 3f;
	float height = 3f;
	float respawnSpeed = 3f;

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
    }

	void Start()
	{
		Spawn ();
	}

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Space)) {
			pause = !pause;
		}

		if (pause) {
			Time.timeScale = 0f;
			panelVictory.SetActive (true);

		} else {
			panelVictory.SetActive (false);
			Time.timeScale = 1f;
		}


		if (pause)
			return;
		
        timer = timerDuration - (Time.time - _timerStart);

		UpdateUI ();

        if(timer < 0)
        {
			EndGame ();
        }
    }

	void UpdateUI()
	{
		int _tempMin = (int)timer / 60;
		int _tempSec = ((int)timer % 60);


		timerUI.GetComponent<Text>().text = _tempMin+":"+_tempSec;
		scoreTeam1UI.GetComponent<Text>().text = teamOneScore+"";
		scoreTeam2UI.GetComponent<Text> ().text = teamTwoScore+"";
	}

	void EndGame()
	{
		panelVictory.SetActive (true);
		pause = true;
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
		GameObject secondBall = Instantiate (Resources.Load ("Prefabs/Ball"), spawnBall2.transform.position, Quaternion.identity) as GameObject;
		BallsManager.instance.balls.Add (secondBall);

		for (int i = 1; i < 5; i++) 
		{
			GameObject _player = Instantiate (prefabPlayer) as GameObject;
			GameObject _mask = Instantiate (SelectionManager.instance.currentMask [i]) as GameObject;
			_mask.transform.localScale *= 0.3f;
			_mask.transform.parent = _player.transform;
			_mask.transform.localPosition = Vector3.zero - (Vector3.up * 0.71f);

            _player.GetComponent<Movement>().mesh = _mask;

			_player.GetComponent<PlayerActions> ().id = i;
			_player.GetComponent<PlayerActions> ().teamId = SelectionManager.instance.currentTeam[i];
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

	public void Respawn(int _id)
	{
		StartCoroutine (CountDownRespawnBall (_id));
	}

	public void RespawnPlayer(GameObject _player)
	{
		
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

	IEnumerator CountDownRespawnBall(int _id)
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
	}

	IEnumerator CountDownRespawnPlayer(GameObject _player)
	{
		yield return new WaitForSeconds (2.0f);
		_player.transform.LookAt (new Vector3(center.transform.position.x, 0f, center.transform.position.z));
		_player.SetActive (true);
	}

    public void IncreaseFever(int id)
    {
        if(id == 1 && Time.time - lastTeam1Increase > feverIncreaseDelay)
        {
            publicFeverTeam1++;
            lastTeam1Increase = Time.time;

            if (publicFeverTeam1 >= feverMax)
            {
                publicFeverTeam1 = 0;
                PinataManager.instance.CheckEffect(id);
            }
        }
        else if(id == 2 && Time.time - lastTeam2Increase > feverIncreaseDelay)
        {
            publicFeverTeam2++;
            lastTeam2Increase = Time.time;

            if (publicFeverTeam2 >= feverMax)
            {
                publicFeverTeam2 = 0;
                PinataManager.instance.CheckEffect(id);
            }
        }
    }

}
