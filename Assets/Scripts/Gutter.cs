using UnityEngine;
using System.Collections;

public class Gutter : MonoBehaviour {


	Vector3 _startPosition;
	Vector3 _endPosition;
	Vector3 _bending;
	float _timeToTravel = 10.0f;

	float angle = 0;

	// Use this for initialization
	void Start () {
		_startPosition = new Vector3(-10f,0f,0f);
		_endPosition = new Vector3(10f,0f,0f);
		_bending = new Vector3(0f,10f,0f);
		StartCoroutine(MoveToPosition ());
	}

	IEnumerator MoveToPosition()
	{
		float _timeStamp = Time.time;
		while(transform.position != _startPosition)
		{
			Debug.Log ("test");
			transform.RotateAround (Vector3.zero, Vector3.forward, 35f*Time.deltaTime);
			//Vector3 currentPosition = Vector3.Lerp (_startPosition, _endPosition, (Time.time - _timeStamp)/_timeToTravel);

			//currentPosition.x += _bending.x * Mathf.Sin (Mathf.Clamp01((Time.time - _timeStamp)/_timeToTravel)*Mathf.PI);

			//currentPosition.y += _bending.y * Mathf.Sin (Mathf.Clamp01((Time.time - _timeStamp)/_timeToTravel)*Mathf.PI);
		


			//transform.position = currentPosition;

			yield return null;
		}

	}

}
