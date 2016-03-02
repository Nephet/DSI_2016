using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerActions : MonoBehaviour {

	public GameObject currentBall;

	public List<GameObject> balls;

    GameObject _mesh;

    public float throwPower = 5f;

    public static int nbPlayers;
    public int id;
    
    public int teamId;

    public bool isBall;

	bool snap;
	public float rangeSnap = 1.0f;
	float _nearestdistance = Mathf.Infinity;
	GameObject _nearestBall;

    void Awake()
    {
        nbPlayers++;

        id = nbPlayers;
    }

    void Start()
    {
        _mesh = GetComponent<Movement>().mesh;

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


		if (snap && currentBall != null)
        {
            Throw(throwPower);
        }
        else if (snap)
        {
            DistanceBalls();
			if (_nearestBall != null) {
				currentBall = _nearestBall;
				currentBall.GetComponent<Rigidbody> ().isKinematic = true;
				currentBall.transform.parent = _mesh.transform;
				currentBall.transform.position = transform.position + _mesh.transform.forward /2;
			}

		}
    }

    void Throw(float power)
    {
        currentBall.GetComponent<Rigidbody>().isKinematic = false;
        currentBall.transform.parent = null;
        currentBall.GetComponent<Rigidbody>().AddForce(_mesh.transform.forward * power * currentBall.GetComponent<Ball>().SpeedModifier, ForceMode.Impulse);
		currentBall = null;
		_nearestBall = null;
		_nearestdistance = Mathf.Infinity;

        currentBall.GetComponent<Ball>().SpeedModifier += 0.2f;
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
        isBall = b;
    }
}
