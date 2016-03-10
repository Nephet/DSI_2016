﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ParticlePlayground;

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

	float _throwTimer = Mathf.Infinity;

    public static int nbPlayers;
    public int id;
    
    public int teamId;

	bool snap;
    bool _snap;
    bool snapAlt;
    bool _transfo;
    bool _transfoAlt;
    bool _dash;
	bool _suicide;
    bool _bonus;
    bool _dance;
    bool _shoot;
    bool _lose;

    bool _soloThrow = false;

    public Animator _anim;

    float _smashButtonCount;
	public float _maxTimerSmashButton = 0.5f;
	float _currentTimerSmashButton;
    public int nbSuicideInput = 10;

    public float rangeSnap = 1.0f;
	float _nearestdistance = Mathf.Infinity;
	GameObject _nearestBall;

    public float passSpeedModifier = 0.2f;

    Ball _ballScript;
    Bounce _bounceScript;

    public int currentZone;

	List<GameObject> _listPlayers;
	float _lastMagnitude;
	Vector3 _dirAlt;
	Vector3 _shootDirection;
    Vector3 _moveDir;

    public float snapDelay = 0.5f;
	float _currentSnapDelay = Mathf.Infinity;

    bool _isMoving;
	bool _oldTriggerHeldRight;
	bool _oldTriggerHeldLeft;

    [HideInInspector]
    public bool willIgnoreSnap = false;
    [HideInInspector]
    public bool doubleDash = false;
    [HideInInspector]
    public bool maxSpeed = false;
    [HideInInspector]
    public bool instantExplosion = false;
    [HideInInspector]
    public bool snakeBall = false;
    [HideInInspector]
    public bool frenzy = false;

	//****
	//PARTICLES
	//****

	[Header("Particles")]

	public GameObject partSuicideTeam_1;
	public GameObject partSuicideTeam_2;
	GameObject partSuicide;

	public GameObject partDance;

	public GameObject partStun;

	public GameObject partTransfoBall;

	public GameObject partMovement;

    public GameObject trailColor;

    /*
	public GameObject partPossession;
    public GameObject partCurrentPossession;
	bool _partPossession;
    */

    void Awake()
    {
        dashing = false;

        nbPlayers++;

        //id = nbPlayers;

        _ballScript = GetComponent<Ball>();
        _ballScript.enabled = false;

        _bounceScript = GetComponent<Bounce>();
        _bounceScript.enabled = false;

		// Particles
		_isMoving = false;
        //_partPossession = false;

     }

    void Start()
    {
		// Particles
		if (teamId == 1) 
		{
			partSuicide = partSuicideTeam_1;
            GetComponent<Movement>().body.GetComponent<SkinnedMeshRenderer>().material.SetColor("_teamColor", PlayerManager.instance.colorTeam1);
        }

        else 
		{
			partSuicide = partSuicideTeam_2;
            GetComponent<Movement>().body.GetComponent<SkinnedMeshRenderer>().material.SetColor("_teamColor", PlayerManager.instance.colorTeam2);

        }

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

        // Change trail color
        if (teamId == 1)
        {
            trailColor.GetComponent<Renderer>().material.SetColor("_TintColor", PlayerManager.instance.colorTeam1);
        }

        else if (teamId == 2)
        {
            trailColor.GetComponent<Renderer>().material.SetColor("_TintColor", PlayerManager.instance.colorTeam2);
        }
        
    }

    void Update()
    {
        if (MatchManager.Instance.endGame == true)
        {
            if (MatchManager.Instance.teamOneScore < MatchManager.Instance.teamTwoScore && gameObject.tag == "Player")
            {
                if (teamId == 1 && _anim.GetBool("lose") != true)
                {
                    _anim.SetBool("lose", true);
                    _dance = true;
                }
                else if (teamId == 2)
                    _anim.SetBool("win", true);
            }
            else if (MatchManager.Instance.teamOneScore > MatchManager.Instance.teamTwoScore)
            {
                if (teamId == 1)
                    _anim.SetBool("win", true);
                else if (teamId == 2 && _anim.GetBool("lose") != true)
                    _anim.SetBool("lose", true);
            }
        }

        if (MatchManager.Instance.pause || MatchManager.Instance.endGame)
			return;


        _moveDir = new Vector3(Input.GetAxis("L_XAxis_" + id), 0.0f, Input.GetAxis("L_YAxis_" + id));
        _throwTimer += Time.deltaTime;
		_currentSnapDelay += Time.deltaTime;
		float _altHorizontal = Input.GetAxis("R_XAxis_"+id);
		float _altVertical = Input.GetAxis("R_YAxis_"+id);

		transform.rotation = Quaternion.Euler (Vector3.zero);
        snap = Input.GetButtonDown("A_Button_" + id);
        snapAlt = Input.GetAxis ("Fire_"+id) < -0.1f;
        
        _transfo = Input.GetButtonDown("Y_Button_" + id);
        _transfoAlt = Input.GetAxis ("Fire_"+id) > 0.1f;

		//_dash = Input.GetAxis("Fire_" + id) < 0.0f;

		_suicide = Input.GetButtonDown ("A_Button_"+id);

        _bonus = Input.GetButtonDown("Y_Button_" + id);

		_dance = Input.GetButton("B_Button_" + id) || Input.GetButton("Bump_Left_" + id) || Input.GetButton("Bump_Right_" + id);
        if (_anim != null && gameObject.tag == "Player" )
            _anim.SetBool("win", _dance);
        //_anim.SetBool("lose", _lose);

        _shoot = Input.GetButton("X_Button_" + id);
        _shootDirection = new Vector3 (_altHorizontal,0.0f, _altVertical);

        if (_shoot && currentBall != null && state == State.HUMAN)
        {

            Throw(_throwPower, false, false);

        }
        

            if (_shoot && state == State.HUMAN && _throwTimer >= 0.5f)
        {
            DistanceBalls();

			if(_nearestBall && _nearestBall.GetComponent<Ball>().idTeam == teamId 
                && !_nearestBall.GetComponent<Ball>().bounce && currentBall == null/* && _nearestBall.GetComponent<Ball>().idPlayer != id*/)
            {
				
                _nearestBall.GetComponent<Ball>().currentPowerLevel = Mathf.Clamp(_nearestBall.GetComponent<Ball>().currentPowerLevel + 2, 1, 2);

                MatchManager.Instance.StartSlowMo(MatchManager.Instance.slowMoDuration);

                Snap();

				Throw(_throwPower,true, false);
                
                //Invoke("StopSlowMo", MatchManager.Instance.slowMoDuration * MatchManager.Instance.slowMoPower);
            }

            else
            {
                _nearestBall = null;
            }
        }
	else if ((snap || (snapAlt && (_oldTriggerHeldRight != snapAlt)))  && (_currentSnapDelay >= snapDelay) && currentBall == null && state == State.HUMAN)
        {
            _currentSnapDelay = 0f;

            DistanceBalls();
            if (_nearestBall != null)
            {
                Snap();
            }
        }
        else if ((snap || (snapAlt && (_oldTriggerHeldRight != snapAlt))) && currentBall != null && state == State.HUMAN)
        {

            Throw(_throwPower * 0.8f, false, true);

        }
        else if ((_transfo || (_transfoAlt && (_oldTriggerHeldLeft != _transfoAlt))) && (state == State.HUMAN || state == State.FREEBALL))
        {
            SetToBall(state == State.HUMAN);
        }
	else if (_shoot 
            && (state == PlayerActions.State.THROWBALL || state == PlayerActions.State.FREEBALL || state == PlayerActions.State.PRISONNERBALL) 
            && GetComponent<Ball>().idTeam == teamId && !_soloThrow)
        {
            StartDash();
        }

        else if (_suicide && state != PlayerActions.State.HUMAN)
        {
            if (_currentTimerSmashButton > 0f && _smashButtonCount > 0f)
            {
                _currentTimerSmashButton -= Time.deltaTime;
                SmashButton();
            }
            else if (_currentTimerSmashButton <= 0f)
            {
                //_smashButtonCount -=0.5f;
                SmashButton();
            }
        }

        else if (_bonus)
        {
            PinataManager.instance.ApplyBonus(this);
        }

        else if (_dance && state == State.HUMAN && GetComponent<Movement>()._velocity == Vector3.zero)
        {
            Dance();
        }

		_oldTriggerHeldRight = snapAlt;
		_oldTriggerHeldLeft = _transfoAlt;
    

		// ****
		// PARTICLES
		// ****

		// Movement
		if (GetComponent<Movement> ()._velocity != Vector3.zero) 
		{
			if (_isMoving == false) 
			{
				StartCoroutine (ParticleIsMoving ());
			}
		} 

		else 
		{
			_isMoving = false;
		}

		// Possession
        /*
		if (currentBall != null && state == State.HUMAN)
		{
			if (_partPossession == false) 
			{
                _partPossession = true;
                partCurrentPossession = ParticlePossession();
			}
		} 

		else if (currentBall == null)
		{
            // print("possessionOFF");
            Destroy(partCurrentPossession, 0.1f);

            partCurrentPossession = null;
            
			_partPossession = false;
		}
        */

    }
			
    void StopSlowMo()
    {
        MatchManager.Instance.StopSlowMo();
    }

    void Snap()
    {

        GetComponent<Movement>().body.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_glowIntensity", 2);

        _anim.SetTrigger("snap");
        currentBall = _nearestBall;
        BallsManager.instance.RemoveBall(currentBall);
        currentBall.GetComponent<Rigidbody>().isKinematic = true;
        //currentBall.GetComponent<SphereCollider> ().enabled = false;
        currentBall.transform.parent = _mesh.transform;
        currentBall.transform.position = transform.position + _mesh.transform.forward / 2;

        currentBall.GetComponent<Ball>().currentPowerLevel = Mathf.Clamp(currentBall.GetComponent<Ball>().currentPowerLevel + 1, 1, 2);

        currentBall.GetComponent<Ball>().idTeam = teamId;
        currentBall.GetComponent<Ball>().idPlayer = id;

        currentBall.GetComponent<Ball>().StopSpeedDrop();

        currentBall.GetComponent<Ball>().StartPowerDrop();

        ChangeTrailColor();

        if (currentBall.GetComponent<PlayerActions>())
        {
            currentBall.GetComponent<PlayerActions>().state = State.TAKENBALL;
        }
    }

	void Throw(float power, bool volley, bool pass)
    {
        if (!currentBall) return;

        GetComponent<Movement>().body.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_glowIntensity", 0);

        _throwTimer = 0;
		_soloThrow = false;

        if (_anim != null && gameObject.tag == "Player")
            _anim.SetTrigger("shoot");
        
		BallsManager.instance.AddBall (currentBall);
		currentBall.GetComponent<Ball> ().bounce = false;
        currentBall.GetComponent<Rigidbody>().isKinematic = false;
		currentBall.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
		//currentBall.GetComponent<SphereCollider> ().enabled = true;
        currentBall.transform.parent = null;

		if (currentBall.GetComponent<Ball> ().currentPowerLevel <= 0) {
			currentBall.GetComponent<Ball> ().currentPowerLevel = 1;
		}

		float speedModifier;

		if (volley) 
		{
			speedModifier = (BallsManager.instance.speedVolley)*1.0f / 199*1.0f;
            currentBall.GetComponent<Ball>().volleyParticles.emit = true;
            if (teamId == 1)
            {
                currentBall.GetComponent<Ball>().volleyParticles.GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor", PlayerManager.instance.colorTeam1);
            }
                
            else if (teamId == 2)
            {
                currentBall.GetComponent<Ball>().volleyParticles.GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor", PlayerManager.instance.colorTeam2);
            }
                
        }
        else 
		{
			speedModifier = (BallsManager.instance.speedMaxByPowerLevel[maxSpeed ? 1 : currentBall.GetComponent<Ball>().currentPowerLevel-1])*1.0f / 199 *1.0f;
		}
				
        if (frenzy)
        {
            speedModifier *= 1.15f;
        }

        maxSpeed = false;

		currentBall.GetComponent<Ball>().StopPowerDrop();

        currentBall.GetComponent<Ball>().StartSpeedDrop();

        currentBall.GetComponent<Ball>().ignoreSnap = willIgnoreSnap;

        willIgnoreSnap = false;

        currentBall.GetComponent<Rigidbody>().velocity = Vector3.zero;


        /*
		GetComponent<Movement> ()._lastDirectionAlt = _shootDirection;
		GetComponent<Movement> ()._directionAlt = _shootDirection;

		transform.rotation = Quaternion.LookRotation (_shootDirection);
		_mesh.transform.rotation = Quaternion.LookRotation (_shootDirection);
		_ballMesh.transform.rotation = Quaternion.LookRotation (_shootDirection);
        */

        

        GetComponent<Movement>()._lastDirectionAlt = _mesh.transform.forward;
        GetComponent<Movement>()._directionAlt = _mesh.transform.forward;


        transform.rotation = Quaternion.LookRotation(_mesh.transform.forward);
        _mesh.transform.rotation = Quaternion.LookRotation(_mesh.transform.forward);
        _ballMesh.transform.rotation = Quaternion.LookRotation(_mesh.transform.forward);
        if (_moveDir != Vector3.zero)
        {
            GetComponent<Movement>()._lastDirectionAlt = _moveDir;
            GetComponent<Movement>()._directionAlt = _moveDir;


            transform.rotation = Quaternion.LookRotation(_moveDir);
            _mesh.transform.rotation = Quaternion.LookRotation(_moveDir);
            _ballMesh.transform.rotation = Quaternion.LookRotation(_moveDir);
        }
        if (pass)
        {

            Vector3 tempPlayer = new Vector3();

            _listPlayers = PlayerManager.instance.listPlayers;
            for (int i = 0; i < _listPlayers.Count; i++)
            {
                if (_listPlayers[i].GetComponent<PlayerActions>().teamId == GetComponent<PlayerActions>().teamId
                    && _listPlayers[i].GetComponent<PlayerActions>() != GetComponent<PlayerActions>())
                {

                    tempPlayer = _listPlayers[i].transform.position;
                }
            }

            GetComponent<Movement>()._lastDirectionAlt = tempPlayer - transform.position;
            GetComponent<Movement>()._directionAlt = tempPlayer - transform.position;


            transform.rotation = Quaternion.LookRotation(tempPlayer - transform.position);
            _mesh.transform.rotation = Quaternion.LookRotation(tempPlayer - transform.position);
            _ballMesh.transform.rotation = Quaternion.LookRotation(tempPlayer - transform.position);

        }
        if (teamId == 1 && !pass)
        {
            _ballMesh.transform.rotation = Quaternion.LookRotation(_mesh.transform.forward);

            if (_moveDir == Vector3.zero)
            {
                if (Vector3.Angle((MatchManager.Instance.respawnGoal2.transform.position - transform.position), _mesh.transform.forward) < 30)
                {
                    GetComponent<Movement>()._lastDirectionAlt = MatchManager.Instance.respawnGoal2.transform.position - transform.position;
                    GetComponent<Movement>()._directionAlt = MatchManager.Instance.respawnGoal2.transform.position - transform.position;


                    transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal2.transform.position - transform.position);
                    _mesh.transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal2.transform.position - transform.position);
                    _ballMesh.transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal2.transform.position - transform.position);
                }
            }
            else
            {
                if (Vector3.Angle((MatchManager.Instance.respawnGoal2.transform.position - transform.position), _moveDir) < 30)
                {
                    GetComponent<Movement>()._lastDirectionAlt = MatchManager.Instance.respawnGoal2.transform.position - transform.position;
                    GetComponent<Movement>()._directionAlt = MatchManager.Instance.respawnGoal2.transform.position - transform.position;


                    transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal2.transform.position - transform.position);
                    _mesh.transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal2.transform.position - transform.position);
                    _ballMesh.transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal2.transform.position - transform.position);
                }
            }
        }
        else
        {
            _ballMesh.transform.rotation = Quaternion.LookRotation(_mesh.transform.forward);
            if (Vector3.Angle((MatchManager.Instance.respawnGoal1.transform.position - transform.position), _moveDir) < 30)
            {
                GetComponent<Movement>()._lastDirectionAlt = MatchManager.Instance.respawnGoal1.transform.position - transform.position;
                GetComponent<Movement>()._directionAlt = MatchManager.Instance.respawnGoal1.transform.position - transform.position;


                transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal1.transform.position - transform.position);
                _mesh.transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal1.transform.position - transform.position);
                _ballMesh.transform.rotation = Quaternion.LookRotation(MatchManager.Instance.respawnGoal1.transform.position - transform.position);
            }

        }
        if (_moveDir == Vector3.zero)
        {
            GetComponent<Movement>()._lastDirectionAlt = transform.forward;
            GetComponent<Movement>()._directionAlt = transform.forward;


            transform.rotation = Quaternion.LookRotation(transform.forward);
            _mesh.transform.rotation = Quaternion.LookRotation(transform.forward);
            _ballMesh.transform.rotation = Quaternion.LookRotation(transform.forward);
        }




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
			if (BallsManager.instance.balls [i] != null) {
				float distance = Vector3.Distance (transform.position, BallsManager.instance.balls [i].transform.position);
				if (distance < _nearestdistance && distance <= rangeSnap && BallsManager.instance.balls[i] != gameObject) {
					if (!BallsManager.instance.balls[i].GetComponent<Ball>().ignoreSnap)
					{
						_nearestBall = BallsManager.instance.balls[i].gameObject;
					}
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
        
		gameObject.layer = b ? 8 : 10;

        _ballScript.enabled = b;
        _bounceScript.enabled = b;

        _ballMesh.SetActive(b);
        _mesh.SetActive(!b);

        GetComponent<CapsuleCollider>().enabled = !b;
        GetComponent<SphereCollider>().enabled = b;

        if (b)
        {
            

            StartParticles(partTransfoBall, 2f, Vector3.up);

            BallsManager.instance.AddBall(gameObject);

            _ballScript.idTeam = teamId;
			if (GetComponent<Movement> ()._velocity != Vector3.zero)
            {
                //_soloThrow = true;
                if (currentZone == teamId)
                    state = State.FREEBALL;
                else
				    state = State.THROWBALL;
			}
			gameObject.GetComponent<Rigidbody> ().AddForce (GetComponent<Movement> ()._velocity , ForceMode.Impulse);

            if (currentBall != null)
            {
                GameObject tempGo = currentBall;
                Throw(0.1f, false, false);
                tempGo.GetComponent<Ball>().currentPowerLevel = 0;
                if (tempGo.GetComponent<PlayerActions>() != null)
                {
                    tempGo.GetComponent<PlayerActions>().state = State.FREEBALL;
                    tempGo.GetComponent<Movement>().meshBall.GetComponent<MeshRenderer>().material.SetFloat("_glowIntensity", 0);
                }
                else
                {
                    tempGo.GetComponent<Ball>().meshBall.GetComponent<MeshRenderer>().material.SetFloat("_glowIntensity", 0);
                }
            }

        }

        else
        {
            StartParticles(partTransfoBall, 2f, Vector3.up);
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
        
		dashing = true;
        
		_lastMagnitude = GetComponent<Rigidbody> ().velocity.magnitude;
		_dirAlt = _shootDirection;
        //GetComponent<Rigidbody>().AddForce(_mesh.transform.forward * _dashPower, ForceMode.Impulse);
        float tempMag = GetComponent<Rigidbody>().velocity.magnitude;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().AddForce(_moveDir * _dashPower + (_moveDir * _dashPower * tempMag), ForceMode.Impulse);

        _lastDash = Time.time;

        Invoke("StopDash",_dashDuration);
    }

    void StopDash()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().AddForce(_dirAlt * _lastMagnitude, ForceMode.Impulse);
		//state = currentZone == teamId ? State.FREEBALL : State.PRISONNERBALL;
		dashing = false;
    }

	public void Stun ()
	{
		ActiveStun ();
	}

	void ActiveStun ()
	{
		StartParticles (partStun, 2.0f, Vector3.up);

		GetComponent<Movement> ().enabled = false;
		GetComponent<PlayerActions> ().enabled = false;
		Throw (0,false, false);
		Invoke ("DisableStun", 2.0f);
        _anim.SetBool("stun", true);
	}

	void DisableStun()
	{
		GetComponent<Movement> ().enabled = true;
		GetComponent<PlayerActions> ().enabled = true;
        _anim.SetBool("stun", false);
	}

	void SmashButton()
	{
		_currentTimerSmashButton = _maxTimerSmashButton;
		_smashButtonCount++;
		if (_smashButtonCount >= nbSuicideInput || instantExplosion) 
		{
			Suicide ();
            instantExplosion = false;
        }
	}

	void Suicide()
	{
		// Particles
		StartParticles(partSuicide, 1.5f, Vector3.zero);

		_smashButtonCount = 0;
		_currentTimerSmashButton = 0;
		_listPlayers = PlayerManager.instance.listPlayers;
        if (transform.parent != null)
        {
            transform.parent.parent.GetComponent<PlayerActions>().currentBall = null;
            transform.parent.parent.GetComponent<PlayerActions>()._nearestBall = null;
        }
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
        //state = State.HUMAN;
		MatchManager.Instance.RespawnPlayer (this.gameObject);

	}

    void Dance()
    {
	    //StartParticles (partDance, 1.5f, Vector3.zero);
        MatchManager.Instance.IncreaseFever(teamId);
    }

	// **** 
	// PARTICLES
	// ****

    /*
    GameObject ParticlePossession()
    {
        GameObject _partClone = Instantiate(partPossession, transform.position, Quaternion.identity) as GameObject;
        _partClone.transform.parent = this.transform;
        return _partClone;
    }
    */

	IEnumerator ParticleIsMoving()
	{
		_isMoving = true;

        while (_isMoving == true && state == State.HUMAN) 
		{
            StartParticles(partMovement, 0.25f, Vector3.zero);
            yield return new WaitForSeconds(0.1f);	
		}

		_isMoving = false;
	}

	void StartParticles(GameObject _part, float _deathTimer, Vector3 _position)
	{
		GameObject _partClone = Instantiate (_part, this.transform.position + _position, Quaternion.identity) as GameObject;

		if (_partClone.GetComponent<ParticleFollowPlayer>()) 
		{
            _partClone.GetComponent<ParticleFollowPlayer>().target = this.gameObject;
            _partClone.transform.parent = this.transform;
        }

		Destroy (_partClone, _deathTimer);
	}

    void ChangeTrailColor()
    {
        if (teamId == 1)
        {
            currentBall.GetComponent<Ball>().trailLvl1.GetComponent<Renderer>().material.SetColor("_TintColor", PlayerManager.instance.colorTeam1);
            currentBall.GetComponent<Ball>().trailLvl2.GetComponent<Renderer>().material.SetColor("_TintColor", PlayerManager.instance.colorTeam1);
            currentBall.GetComponent<Ball>().meshBall.GetComponent<Renderer>().material.SetColor("_teamColor", PlayerManager.instance.colorTeam1);
        }
        
        else if (teamId == 2)
        {
            currentBall.GetComponent<Ball>().trailLvl1.GetComponent<Renderer>().material.SetColor("_TintColor", PlayerManager.instance.colorTeam2);
            currentBall.GetComponent<Ball>().trailLvl2.GetComponent<Renderer>().material.SetColor("_TintColor", PlayerManager.instance.colorTeam2);
            currentBall.GetComponent<Ball>().meshBall.GetComponent<Renderer>().material.SetColor("_teamColor", PlayerManager.instance.colorTeam2);
        }
    }
}