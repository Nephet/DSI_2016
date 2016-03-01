using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerActions : MonoBehaviour {

	public GameObject currentBall;

	float nearestdistance = Mathf.Infinity;
	GameObject nearestBall;
	GameObject mesh;

	bool Snap;
	public float rangeSnap = 1.0f;


	void Start()
	{
		mesh = transform.GetComponent<Movement> ().mesh;
	}

	void Update()
	{
		Snap = Input.GetButtonDown ("A_Button_1");
		Debug.Log (Snap);
		if (Snap) {
			DistanceBalls ();
			if (nearestBall != null) {
				currentBall = nearestBall;
				currentBall.GetComponent<Rigidbody> ().isKinematic = true;
				currentBall.transform.parent = mesh.transform;
			}

		}
			
		if(currentBall!=null){
			
			Debug.Log (currentBall);
			currentBall.transform.position = transform.position + currentBall.transform.forward /2;

		}


	}

	void DistanceBalls()
	{
		for (int i = 0; i < BallsManager.instance.balls.Count; i++) 
		{
			float distance = Vector3.Distance (transform.position, BallsManager.instance.balls [i].transform.position);
			if (distance < nearestdistance && distance <= rangeSnap) {
				nearestBall = BallsManager.instance.balls [i].gameObject;
			}
		}
	}
}
