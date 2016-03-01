using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

	public float speed = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float _horizontal = Input.GetAxisRaw ("Horizontal");
		float _vertical = Input.GetAxisRaw ("Vertical");

		Vector3 _movHorizontal = transform.right * _horizontal;
		Vector3 _movVertical = transform.right * _vertical;

		float _velocity = (_movHorizontal * _movVertical).normalized * speed;
	}

	void FixedUpdate()
	{
		PerformMovement ();

	}

	void PerformMovement()
	{

	}
}
