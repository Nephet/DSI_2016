﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bounce : MonoBehaviour {

	Vector3 _oldVelocity;
	float _friction = 0.8f;

	Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		_friction = BallsManager.instance.friction;
	}

	void FixedUpdate()
	{
		_oldVelocity = rigidBody.velocity;
	}

	void OnCollisionEnter(Collision other)
	{
		if (!enabled) return;

		if (!GetComponent<Ball> ().respawning && rigidBody.velocity!=Vector3.zero) {
			if (other.contacts.Length == 0)
				return;
			rigidBody.velocity = Vector3.Reflect (_oldVelocity, other.contacts [0].normal);
			rigidBody.velocity *= _friction;
			rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
		} 

	}
}
