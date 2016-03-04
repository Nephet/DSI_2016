using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

	Rigidbody _rigidB;


	float _speed;
    float _speedInBall;
	float _rotationSpeed;

    [HideInInspector]
	public Vector3 _velocity;
	Quaternion _rotation; 
	Vector3 _directionAlt;
	Vector3 _lastDirectionAlt;
    
	public GameObject mesh;
    public GameObject ballMesh;
	public GameObject arrowDirection;

    int id;

	// Use this for initialization
	void Start () {

		_rigidB = GetComponent<Rigidbody> ();

        id = GetComponent<PlayerActions>().id;

		_speed = PlayerManager.instance.speed;
		_speedInBall = PlayerManager.instance.speedInBall;
		_rotationSpeed = PlayerManager.instance.rotationSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        _velocity = Vector3.zero;
        
        if (GetComponent<PlayerActions>().state == PlayerActions.State.TAKENBALL || GetComponent<PlayerActions>().state == PlayerActions.State.THROWBALL) return;

        float _horizontal = Input.GetAxis ("L_XAxis_"+id);
		float _vertical = Input.GetAxis ("L_YAxis_"+id);

		float _altHorizontal = Input.GetAxis("L_XAxis_"+id);
		float _altVertical = Input.GetAxis("L_YAxis_"+id);
        
		Vector3 _movHorizontal = transform.right * _horizontal;
		Vector3 _movVertical = transform.forward * _vertical;

        float modifier = GetComponent<PlayerActions>().state == PlayerActions.State.HUMAN ? _speed : _speedInBall;

		_velocity = (_movHorizontal + _movVertical).normalized * modifier;

		/*************/

		_directionAlt = new Vector3 (_altHorizontal, 0f, _altVertical);
		_directionAlt.Normalize ();
		_directionAlt = Vector3.ClampMagnitude (_directionAlt, 1.0f);
		_directionAlt.y = 0f;

		if (_directionAlt.x != 0.0f || _directionAlt.z != 0.0f) {
			_lastDirectionAlt = _directionAlt;
		}
		if (GetComponent<PlayerActions> ().currentBall != null) {
			Feedback ();
		} 
		else {
			arrowDirection.SetActive (false);
		}

		Debug.DrawRay (transform.position, _directionAlt, Color.red);
		_rotation = Quaternion.LookRotation (_lastDirectionAlt, transform.up);
	}
    
	void FixedUpdate()
	{
		PerformMovement ();
		PerformRotation ();

	}

	void PerformMovement()
	{
		if (_velocity != Vector3.zero) 
		{
			_rigidB.MovePosition (_rigidB.position + _velocity * Time.fixedDeltaTime);
		}
	}

	void PerformRotation()
	{
		mesh.transform.localRotation = _rotation;
	}

	void Feedback()
	{
		arrowDirection.SetActive (true);
		Vector3 _targetPoint = transform.position + _lastDirectionAlt;

		Vector3 _midPoint = transform.position + (_targetPoint - transform.position) / 2.0f;
		_midPoint = new Vector3 (_midPoint.x, 0.1f, _midPoint.z);
		arrowDirection.transform.position = _midPoint;

		arrowDirection.transform.LookAt (new Vector3(_targetPoint.x, 0.1f, _targetPoint.z));
	}
}

