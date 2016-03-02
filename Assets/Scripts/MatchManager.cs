using UnityEngine;
using System.Collections;

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

	public GameObject player1;
	public GameObject player2;

	public GameObject spawnBall1;
	public GameObject spawnBall2;
	public GameObject respawnGoal1;
	public GameObject respawnGoal2;
	public GameObject spawnPlayer1;
	public GameObject spawnPlayer2;
	public GameObject center;

	public GameObject testPlayer;

	public float width = 3f;
	public float height = 3f;
	public float respawnSpeed = 3f;

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
        timer = timerDuration - (Time.time - _timerStart);

        if(timer < 0)
        {
			
        }

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			//Respawn (1);
			RespawnPlayer(testPlayer);
		}
    }

    public void AddPoint(int id, int score)
    {
        Debug.Log(id + " " + score);

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
		GameObject firstBall = Instantiate (Resources.Load ("Prefabs/Ball"), spawnBall1.transform.position, Quaternion.identity) as GameObject;
		BallsManager.instance.balls.Add (firstBall);
		GameObject secondBall = Instantiate (Resources.Load ("Prefabs/Ball"), spawnBall2.transform.position, Quaternion.identity) as GameObject;
		BallsManager.instance.balls.Add (secondBall);

		player1.transform.position = spawnPlayer1.transform.position;
		player1.transform.LookAt (new Vector3(center.transform.position.x, 0f, center.transform.position.z));
		player1.GetComponent<PlayerActions> ().teamId = 1;

		player2.transform.position = spawnPlayer2.transform.position;
		player2.transform.LookAt (new Vector3(center.transform.position.x, 0f, center.transform.position.z));
		player2.GetComponent<PlayerActions> ().teamId = 2;

	}

	public void Respawn(int _id)
	{
		GameObject myGo;

		if (_id == 1) {
			myGo = Instantiate (Resources.Load ("Prefabs/Ball"), respawnGoal1.transform.position, respawnGoal1.transform.rotation) as GameObject;
		} 
		else 
		{
			myGo = Instantiate (Resources.Load ("Prefabs/Ball"), respawnGoal2.transform.position, respawnGoal1.transform.rotation) as GameObject;
		}

		BallsManager.instance.AddBall(myGo);
		myGo.GetComponent<Ball> ().respawning = true;
		//myGo.GetComponent<Ball> ().LaunchCoroutine ();
		myGo.GetComponent<Rigidbody>().AddForce(myGo.transform.right * 300f);
	}

	public void RespawnPlayer(GameObject _player)
	{
		
		_player.SetActive (false);
		if (_player.GetComponent<PlayerActions> ().teamId == 1) {
			_player.transform.position = spawnPlayer1.transform.position;
		} 
		else 
		{
			_player.transform.position = spawnPlayer2.transform.position;
		}

		StartCoroutine (CountDownRespawnPlayer (_player));

	}

	IEnumerator CountDownRespawnPlayer(GameObject _player)
	{
		yield return new WaitForSeconds (1.0f);
		_player.transform.LookAt (new Vector3(center.transform.position.x, 0f, center.transform.position.z));
		_player.SetActive (true);
	}

}
