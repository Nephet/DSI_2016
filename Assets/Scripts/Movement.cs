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
	public Vector3 _directionAlt;
	public Vector3 _lastDirectionAlt;
    
	public GameObject mesh;
    public GameObject ballMesh;
	public GameObject arrowDirection;

	public float smoothMove = 0;

    int id;

    bool _speedUp = false;

	// Use this for initialization
	void Start () {

		_rigidB = GetComponent<Rigidbody> ();

        id = GetComponent<PlayerActions>().id;

		_speed = PlayerManager.instance.speed;
		_speedInBall = PlayerManager.instance.speedInBall;
		_rotationSpeed = PlayerManager.instance.rotationSpeed;
		mesh.transform.localPosition = new Vector3 (0f, -1.0f, 0f);
		arrowDirection.transform.parent = mesh.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (MatchManager.Instance.pause || MatchManager.Instance.endGame)
			return;
		
        _velocity = Vector3.zero;

		float _altHorizontal = Input.GetAxis("L_XAxis_"+id);
		float _altVertical = Input.GetAxis("L_YAxis_"+id);

		_directionAlt = new Vector3 (_altHorizontal, 0f, _altVertical);
		_directionAlt.Normalize ();
		_directionAlt = Vector3.ClampMagnitude (_directionAlt, 1.0f);
		_directionAlt.y = 0f;

		if (_directionAlt.x != 0.0f || _directionAlt.z != 0.0f) {
			_lastDirectionAlt = _directionAlt;
		}

        if (GetComponent<PlayerActions>().state == PlayerActions.State.TAKENBALL || GetComponent<PlayerActions>().state == PlayerActions.State.THROWBALL) return;

        float _horizontal = Input.GetAxis ("L_XAxis_"+id);
		float _vertical = Input.GetAxis ("L_YAxis_"+id);


        
		Vector3 _movHorizontal = transform.right * _horizontal;
		Vector3 _movVertical = transform.forward * _vertical;

        float modifier = GetComponent<PlayerActions>().state == PlayerActions.State.HUMAN ? _speed : _speedInBall;

		_velocity = (_movHorizontal + _movVertical).normalized * modifier;

		/*************/
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
		if (MatchManager.Instance.pause)
			return;
		PerformMovement ();
		PerformRotation ();

	}

    public void SetSpeedUp()
    {
        _speedUp = true;

        Invoke("StopSpeedUp", PinataManager.instance.speedUpDelay);
    }

    void StopSpeedUp()
    {
        _speedUp = false;
    }

	void PerformMovement()
	{
		if (MatchManager.Instance.pause)
			return;
		
		if (_velocity != Vector3.zero) 
		{
			smoothMove = Mathf.Clamp01(smoothMove+(2f * Time.deltaTime));
			if (_speedUp) 
			{
				_rigidB.MovePosition (_rigidB.position + _velocity * smoothMove * Time.fixedDeltaTime * PinataManager.instance.speedMultiplicator);
			} 
			else 
			{
				_rigidB.MovePosition (_rigidB.position + _velocity* smoothMove * Time.fixedDeltaTime);
			}
			
		} else 
		{
			smoothMove = 0;
		}
	}

	void PerformRotation()
	{
		mesh.transform.localRotation = Quaternion.Lerp(mesh.transform.localRotation, _rotation, 10f * Time.fixedDeltaTime);
	}

	void Feedback()
	{
		arrowDirection.SetActive (true);
		arrowDirection.transform.localPosition = Vector3.zero;
		Vector3 _targetPoint = transform.position + _lastDirectionAlt;

		Vector3 _midPoint = transform.position + (_targetPoint - transform.position) / 2.0f;
		_midPoint = new Vector3 (_midPoint.x, 2.5f, _midPoint.z);
		arrowDirection.transform.position = _midPoint;
		arrowDirection.transform.localRotation = Quaternion.Euler (new Vector3 (90f,0f,0f));

	}
}

