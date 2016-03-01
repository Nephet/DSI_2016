using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bounce : MonoBehaviour {

	Vector3 _oldVelocity;
	public float friction = 0.8f;

	Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		_oldVelocity = rigidBody.velocity;
	}

	void OnCollisionEnter(Collision other)
	{
		

		/**************/

		rigidBody.velocity = Vector3.Reflect (_oldVelocity, other.contacts [0].normal);
		rigidBody.velocity *= friction;

	}
}
