using UnityEngine;
using System.Collections;
using ParticlePlayground;

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
			
			SoundManagerEvent.emit (SoundManagerType.BOUNCE);
			gameObject.GetComponent<Ball> ().currentPowerLevel = 0;
			gameObject.GetComponent<Ball> ().bounce = true;
			_rigidBody.velocity = Vector3.Reflect (_oldVelocity, other.contacts [0].normal);
			_rigidBody.velocity *= _friction;
			_rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            GetComponent<Ball>().volleyParticles.emit = false;

            if (other.gameObject.GetComponent<Ball>())
            {
				
                other.gameObject.GetComponent<Rigidbody>().AddForce(_oldVelocity - _rigidBody.velocity, ForceMode.Impulse);

                if (other.gameObject.GetComponent<PlayerActions>())
                {
                    other.gameObject.GetComponent<PlayerActions>().state = PlayerActions.State.THROWBALL;
                }
            }

		} 

	}
}
