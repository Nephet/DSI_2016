using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bounce : MonoBehaviour {

	Vector3 _oldVelocity;
	float _friction = 0.8f;

	Rigidbody _rigidBody;
	// Use this for initialization
	void Start () {
		_rigidBody = GetComponent<Rigidbody> ();
		_friction = BallsManager.instance.friction;
	}

	void FixedUpdate()
	{
		_oldVelocity = _rigidBody.velocity;
	}

	void OnCollisionEnter(Collision other)
	{
		if (!enabled) return;

		if (!GetComponent<Ball> ().respawning && _rigidBody.velocity!=Vector3.zero) {
			if (other.contacts.Length == 0)
				return;
			_rigidBody.velocity = Vector3.Reflect (_oldVelocity, other.contacts [0].normal);
			_rigidBody.velocity *= _friction;
			_rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            if (other.gameObject.GetComponent<Ball>())
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(_oldVelocity - _rigidBody.velocity, ForceMode.Impulse);
            }

		} 

	}
}
