using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

	Rigidbody _rigidB;

	public float speed = 5f;
	public float rotationSpeed = 5;
	Vector3 _velocity;
	Quaternion _rotation; 
	Vector3 _directionAlt;

	public GameObject mesh;
	// Use this for initialization
	void Start () {
		_rigidB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float _horizontal = Input.GetAxis ("L_XAxis_1");
		float _vertical = Input.GetAxis ("L_YAxis_1");

		float _altHorizontal = Input.GetAxis("L_XAxis_1");
		float _altVertical = Input.GetAxis("L_YAxis_1");


		Vector3 _movHorizontal = transform.right * _horizontal;
		Vector3 _movVertical = transform.forward * _vertical;

		_velocity = (_movHorizontal + _movVertical).normalized * speed;

		/*************/

		_directionAlt = new Vector3 (_altHorizontal, 0f, _altVertical);
		_directionAlt.Normalize ();
		_directionAlt = Vector3.ClampMagnitude (_directionAlt, 1.0f);
		_directionAlt.y = 0f;

		Debug.DrawRay (transform.position, _directionAlt, Color.red);
		_rotation = Quaternion.LookRotation (_directionAlt, transform.up);
		Debug.Log (_directionAlt);

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
}

/*
 * 
 * 
 * 
 * */
