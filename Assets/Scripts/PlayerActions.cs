using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerActions : MonoBehaviour {

    public enum State
    {
        HUMAN,
        FREEBALL,
        PRISONNERBALL,
        TAKENBALL,
        THROWBALL,
    }

    public bool dashing;

    public State state = State.HUMAN;

	public GameObject currentBall;

	//public List<GameObject> balls;

    GameObject _mesh;

    GameObject _ballMesh;

    float _throwPower;

    float _dashPower;
    float _dashDuration;
    float _dashCooldown;
    float _lastDash;
	float _suicideRange;

    public static int nbPlayers;
    public int id;
    
    public int teamId;

	bool snap;
    bool _transfo;
    bool _dash;
	bool _suicide;
    bool _bonus;
    bool _dance;

    float _smashButtonCount;
	public float _maxTimerSmashButton = 0.5f;
	float _currentTimerSmashButton;

	public float rangeSnap = 1.0f;
	float _nearestdistance = Mathf.Infinity;
	GameObject _nearestBall;

    public float passSpeedModifier = 0.2f;

    Ball _ballScript;
    Bounce _bounceScript;

    public int currentZone;

	List<GameObject> _listPlayers;
    
	bool _oldTriggerHeld;

    [HideInInspector]
    public bool willIgnoreSnap = false;
    [HideInInspector]
    public bool doubleDash = false;
    [HideInInspector]
    public bool maxSpeed = false;
    [HideInInspector]
    public bool instantExplosion = false;

    void Awake()
    {
        dashing = false;

        nbPlayers++;

        //id = nbPlayers;

        _ballScript = GetComponent<Ball>();
        _ballScript.enabled = false;

        _bounceScript = GetComponent<Bounce>();
        _bounceScript.enabled = false;
    }

    void Start()
    {
        _mesh = GetComponent<Movement>().mesh;
        _ballMesh = GetComponent<Movement>().ballMesh;

		_throwPower = PlayerManager.instance.throwPower;
		_dashPower = PlayerManager.instance.dashPower;
		_dashDuration = PlayerManager.instance.dashDuration;
		_dashCooldown = PlayerManager.instance.dashCooldown;
		_suicideRange = PlayerManager.instance.suicideRange;

        if (currentBall)
        {
            currentBall.GetComponent<Rigidbody>().isKinematic = true;
            currentBall.transform.parent = _mesh.transform;
            currentBall.transform.position = transform.position + _mesh.transform.forward/2;
        }
    }

    void Update()
    {
		if (MatchManager.Instance.pause)
			return;

        snap = Input.GetAxis ("Fire_"+id) < 0.0f;

        _transfo = Input.GetButtonDown("B_Button_" + id);

		_dash = Input.GetAxis("Fire_" + id) < 0.0f;

		_suicide = Input.GetButtonDown ("A_Button_"+id);

        _bonus = Input.GetButtonDown("X_Button_" + id);

        _dance = Input.GetButton("A_Button_" + id);

        if (_oldTriggerHeld != snap && snap && currentBall != null && state == State.HUMAN) {
			Throw (_throwPower);

		} else if (_oldTriggerHeld != snap && snap && state == State.HUMAN) {
			DistanceBalls ();
			if (_nearestBall != null) {
				currentBall = _nearestBall;
				currentBall.GetComponent<Rigidbody> ().isKinematic = true;
				//currentBall.GetComponent<SphereCollider> ().enabled = false;
				currentBall.transform.parent = _mesh.transform;
				currentBall.transform.position = transform.position + _mesh.transform.forward / 2;

                currentBall.GetComponent<Ball>().currentPowerLevel++;
                
                currentBall.GetComponent<Ball>().idTeam = id;

                currentBall.GetComponent<Ball>().StopSpeedDrop();

                currentBall.GetComponent<Ball>().StartPowerDrop();

                if (currentBall.GetComponent<PlayerActions> ()) {
					currentBall.GetComponent<PlayerActions> ().state = State.TAKENBALL;
				}

			}
		} else if (_transfo && (state == State.HUMAN || state == State.FREEBALL)) {
			SetToBall (state == State.HUMAN);
		} else if (_oldTriggerHeld != snap && snap && (state == PlayerActions.State.THROWBALL)) {
			StartDash ();
		} else if (_suicide && state == PlayerActions.State.TAKENBALL) 
		{
			if (_currentTimerSmashButton > 0f && _smashButtonCount > 0f) 
			{
				_currentTimerSmashButton -= Time.deltaTime;
				SmashButton ();
			} 
			else if(_currentTimerSmashButton <= 0f) 
			{
				_smashButtonCount -=0.5f;
				SmashButton ();
			}
		}
        else if (_bonus)
        {
            PinataManager.instance.ApplyBonus(this);
        }
        else if(_dance && state == State.HUMAN && GetComponent<Movement>()._velocity == Vector3.zero)
        {
            Dance();
        }

		_oldTriggerHeld = snap;
    }

    void Throw(float power)
    {
        if (!currentBall) return;

        currentBall.GetComponent<Rigidbody>().isKinematic = false;
		currentBall.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionY;;
		//currentBall.GetComponent<SphereCollider> ().enabled = true;
        currentBall.transform.parent = null;

		if (currentBall.GetComponent<Ball> ().currentPowerLevel <= 0) {
			currentBall.GetComponent<Ball> ().currentPowerLevel = 1;
		}
        float speedModifier = BallsManager.instance.speedMaxByPowerLevel[maxSpeed ? 4 : currentBall.GetComponent<Ball>().currentPowerLevel-1] / BallsManager.instance.speedMaxByPowerLevel[0];

        maxSpeed = false;

        currentBall.GetComponent<Ball>().StartSpeedDrop();

        currentBall.GetComponent<Ball>().StopPowerDrop();
        
        currentBall.GetComponent<Ball>().ignoreSnap = willIgnoreSnap;

        willIgnoreSnap = false;

        currentBall.GetComponent<Rigidbody>().velocity = Vector3.zero;

        currentBall.GetComponent<Rigidbody>().AddForce(_mesh.transform.forward * power * speedModifier, ForceMode.Impulse);
        
        if (currentBall.GetComponent<PlayerActions>())
        {
            currentBall.GetComponent<PlayerActions>().state = power == 0 ? State.FREEBALL : State.THROWBALL;
        }

        currentBall = null;
		_nearestBall = null;
		_nearestdistance = Mathf.Infinity;
    }

	void DistanceBalls()
	{
		for (int i = 0; i < BallsManager.instance.balls.Count; i++) 
		{
			float distance = Vector3.Distance (transform.position, BallsManager.instance.balls [i].transform.position);
			if (distance < _nearestdistance && distance <= rangeSnap) {
                if (!BallsManager.instance.balls[i].GetComponent<Ball>().ignoreSnap)
                {
                    _nearestBall = BallsManager.instance.balls[i].gameObject;
                }
                else
                {
                    BallsManager.instance.balls[i].GetComponent<Ball>().ignoreSnap = false;
                }
			}
		}
	}

    public void SetToBall(bool b)
    {
		if (!_mesh)
			return;
		
        state = b ? State.FREEBALL : State.HUMAN;

        tag = b ? "Ball" : "Player";

        GetComponent<Rigidbody>().mass = b ? 1 : 70;
        //GetComponent<Rigidbody>().freezeRotation = !b;
        
        _ballScript.enabled = b;
        _bounceScript.enabled = b;

        _ballMesh.SetActive(b);
        _mesh.SetActive(!b);

        GetComponent<CapsuleCollider>().enabled = !b;
        GetComponent<SphereCollider>().enabled = b;

        if (b)
        {
            BallsManager.instance.AddBall(gameObject);

            _ballScript.idTeam = teamId;

            Throw(0);
        }
        else
        {
            transform.transform.localEulerAngles = Vector3.zero;
            BallsManager.instance.RemoveBall(gameObject);
        }
    }

    void StartDash()
    {
        if (doubleDash)
        {
            doubleDash = false;
            _lastDash = 0f;
        }

        if (Time.time - _lastDash < _dashCooldown) return;
        
        Debug.Log("Start Dash !");

		dashing = true;


        Debug.Log(Time.deltaTime);


        GetComponent<Rigidbody>().AddForce(_mesh.transform.forward * _dashPower, ForceMode.Impulse);

        _lastDash = Time.time;

        Invoke("StopDash",_dashDuration);
    }

    void StopDash()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
		state = currentZone == teamId ? State.FREEBALL : State.PRISONNERBALL;
		dashing = false;
    }

	public void Stun ()
	{
		ActiveStun ();
	}

	void ActiveStun ()
	{
		GetComponent<Movement> ().enabled = false;
		GetComponent<PlayerActions> ().enabled = false;
		Throw (0);
		Invoke ("DisableStun", 2.0f);
	}

	void DisableStun()
	{
		GetComponent<Movement> ().enabled = true;
		GetComponent<PlayerActions> ().enabled = true;
	}

	void SmashButton()
	{
		_currentTimerSmashButton = _maxTimerSmashButton;
		_smashButtonCount++;
		if (_smashButtonCount >= 10 || instantExplosion) 
		{
			Suicide ();
            instantExplosion = false;
        }
	}

	void Suicide()
	{
		_smashButtonCount = 0;
		_listPlayers = PlayerManager.instance.listPlayers;
		//transform.parent.parent.GetComponent<PlayerActions> ().ActiveStun ();
		for (int i = 0; i < _listPlayers.Count; i++) 
		{
			if (_listPlayers[i].GetComponent<PlayerActions> ().teamId != GetComponent<PlayerActions> ().teamId) 
			{
				if(Vector3.Distance(_listPlayers[i].transform.position, transform.position) <= _suicideRange)
				{
					_listPlayers [i].GetComponent<PlayerActions> ().ActiveStun ();
				}
			}
		}
		MatchManager.Instance.RespawnPlayer (this.gameObject);

	}

    void Dance()
    {
        MatchManager.Instance.IncreaseFever(teamId);
    }

}
