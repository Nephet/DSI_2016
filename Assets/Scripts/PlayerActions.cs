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

    public State state = State.HUMAN;

	public GameObject currentBall;

	public List<GameObject> balls;

    GameObject _mesh;

    GameObject _ballMesh;

    public float throwPower = 5f;

    public static int nbPlayers;
    public int id;
    
    public int teamId;

	bool snap;
    bool _transfo;

	public float rangeSnap = 1.0f;
	float _nearestdistance = Mathf.Infinity;
	GameObject _nearestBall;

    public float passSpeedModifier = 0.2f;

    Ball _ballScript;
    Bounce _bounceScript;

    Movement _movementScript;

    public int currentZone;
    
    void Awake()
    {
        nbPlayers++;

        id = nbPlayers;

        _ballScript = GetComponent<Ball>();
        _ballScript.enabled = false;

        _bounceScript = GetComponent<Bounce>();
        _bounceScript.enabled = false;

        _movementScript = GetComponent<Movement>();
    }

    void Start()
    {
        _mesh = GetComponent<Movement>().mesh;
        _ballMesh = GetComponent<Movement>().ballMesh;

        if (currentBall)
        {
            currentBall.GetComponent<Rigidbody>().isKinematic = true;
            currentBall.transform.parent = _mesh.transform;
            currentBall.transform.position = transform.position + _mesh.transform.forward/2;
        }
    }

    void Update()
    {
        snap = Input.GetButtonDown ("A_Button_"+id);

        _transfo = Input.GetButtonDown("B_Button_" + id);

        if (snap && currentBall != null && state == State.HUMAN)
        {
            Throw(throwPower);
        }
        else if (snap && state == State.HUMAN)
        {
            DistanceBalls();
            if (_nearestBall != null)
            {
                currentBall = _nearestBall;
                currentBall.GetComponent<Rigidbody>().isKinematic = true;
                currentBall.transform.parent = _mesh.transform;
                currentBall.transform.position = transform.position + _mesh.transform.forward / 2;

                if (currentBall.GetComponent<PlayerActions>())
                {
                    currentBall.GetComponent<PlayerActions>().state = State.TAKENBALL;
                }

            }
        }
        else if (_transfo && (state == State.HUMAN || state == State.FREEBALL))
        {
            SetToBall(state == State.HUMAN);
        }
    }

    void Throw(float power)
    {
        if (!currentBall) return;

        currentBall.GetComponent<Rigidbody>().isKinematic = false;
        currentBall.transform.parent = null;
        currentBall.GetComponent<Rigidbody>().AddForce(_mesh.transform.forward * power * currentBall.GetComponent<Ball>().SpeedModifier, ForceMode.Impulse);
        
        currentBall.GetComponent<Ball>().SpeedModifier += passSpeedModifier;

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
				_nearestBall = BallsManager.instance.balls [i].gameObject;
			}
		}
	}

    public void SetToBall(bool b)
    {
        state = b ? State.FREEBALL : State.HUMAN;

        tag = b ? "Ball" : "Player";

        GetComponent<Rigidbody>().mass = b ? 1 : 70;
        GetComponent<Rigidbody>().freezeRotation = !b;
        
        _ballScript.enabled = b;
        _bounceScript.enabled = b;
        _movementScript.enabled = !b;

        _ballMesh.SetActive(b);
        _mesh.SetActive(!b);

        GetComponent<CapsuleCollider>().enabled = !b;
        GetComponent<SphereCollider>().enabled = b;

        if (b)
        {
            BallsManager.instance.AddBall(gameObject);

            Throw(0);
        }
        else
        {
            transform.transform.localEulerAngles = Vector3.zero;
            BallsManager.instance.RemoveBall(gameObject);
        }
    }
}
