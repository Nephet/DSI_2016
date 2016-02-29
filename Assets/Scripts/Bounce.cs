using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {


	Vector3 lastBallPosition;
	Vector3 currentBallPosition;
	Vector3 direction;
	Vector3 reflectDirection;

	Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (rigidBody.velocity);
		if (currentBallPosition != null) 
		{
			lastBallPosition = currentBallPosition;
		}
		currentBallPosition = transform.position;
		if (reflectDirection != null) {
			transform.Translate (reflectDirection);
		}

	}

	void OnCollisionEnter(Collision other)
	{
		//reflect = -v + 2*(v*n)*n;
		//reflect *= 0.8;
		direction = lastBallPosition - transform.position;
		float angleCollision = Vector3.Angle (direction, other.contacts [0].normal);
		Debug.DrawRay (other.contacts [0].point, other.contacts [0].normal, Color.red, 5.0f);
		Debug.Log (angleCollision);

		reflectDirection = -Vector3.Reflect (direction *5, other.contacts [0].normal);
		Debug.DrawRay (transform.position, reflectDirection, Color.blue, 5.0f);

	}
}
