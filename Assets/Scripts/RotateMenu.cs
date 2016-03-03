using UnityEngine;
using System.Collections;

public class RotateMenu : MonoBehaviour {

	bool left;
	bool right;

	float angle = 90f;

	IEnumerator currentCoroutine;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		left = Input.GetKeyDown (KeyCode.LeftArrow);
		right = Input.GetKeyDown (KeyCode.RightArrow);

		if (left) 
		{

			if (currentCoroutine != null) {
				StopCoroutine (currentCoroutine);
			}
			currentCoroutine = MenuRotate ( -1f);
			StartCoroutine(currentCoroutine);
		} 
		else if (right) 
		{
			float startZ = transform.rotation.z;
			if (currentCoroutine != null) {
				StopCoroutine (currentCoroutine);
			}
			currentCoroutine = MenuRotate ( 1f);
			StartCoroutine(currentCoroutine);
		}
	}

	IEnumerator MenuRotate(float _direction)
	{
		float startZ = transform.rotation.z;
		startZ = ClampAngle (startZ, 0, 360);
		Debug.Log (startZ);
		while (startZ != ClampAngle(startZ + (angle * _direction), 0, 360)) 
		{
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, startZ + (angle * _direction));
			//transform.eulerAngles =new Vector3(0.0f, 0.0f, Mathf.Lerp (transform.rotation.z, _start + (angle * _direction), 10 * Time.deltaTime));
			yield return new WaitForSeconds (0.1f);
		}

	}

	float ClampAngle(float _angle, float _min, float _max)
	{
		if (_angle < 90 || angle > 270) {
			if (_angle > 180)
				_angle -= 360;
			if (_max > 180)
				_max -= 360;
			if (_min > 180)
				_min -= 360;
		}
		return _angle;
	}
}
