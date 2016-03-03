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
	public int direction = 1;

	[HideInInspector]
	public GameObject player1;
	[HideInInspector]
	public GameObject player2;
	[HideInInspector]
	public GameObject spawnBall1;
	[HideInInspector]
	public GameObject spawnBall2;
	[HideInInspector]
	public GameObject respawnGoal1;
	[HideInInspector]
	public GameObject respawnGoal2;
	[HideInInspector]
	public GameObject spawnPlayer11;
	[HideInInspector]
	public GameObject spawnPlayer21;
	[HideInInspector]
	public GameObject spawnPlayer12;
	[HideInInspector]
	public GameObject spawnPlayer22;
	[HideInInspector]
	public GameObject center;

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
        timer = timerDuration - (Time.time - _timerStart);

        if(timer < 0)
        {
			
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

		player1.transform.position = spawnPlayer11.transform.position;
		player1.transform.LookAt (new Vector3(center.transform.position.x, 0f, center.transform.position.z));
		player1.GetComponent<PlayerActions> ().teamId = 1;

		player2.transform.position = spawnPlayer21.transform.position;
		player2.transform.LookAt (new Vector3(center.transform.position.x, 0f, center.transform.position.z));
		player2.GetComponent<PlayerActions> ().teamId = 2;

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
		//myGo.GetComponent<Ball> ().LaunchCoroutine ();
		myGo.GetComponent<Rigidbody>().AddForce(myGo.transform.right * 300f);
	}

	IEnumerator CountDownRespawnPlayer(GameObject _player)
	{
		yield return new WaitForSeconds (2.0f);
		_player.transform.LookAt (new Vector3(center.transform.position.x, 0f, center.transform.position.z));
		_player.SetActive (true);
	}

}
