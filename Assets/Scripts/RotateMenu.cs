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
			Vector3 lookPos = transform.position;
			lookPos.z = 0;
			Quaternion _rotation = Quaternion.LookRotation(lookPos);
			_rotation *= Quaternion.Euler (0f,0f,-90f);
			transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * 10f);
		} 
		else if (right) 
		{
			Vector3 lookPos = transform.position;
			lookPos.z = 0;
			Quaternion _rotation = Quaternion.LookRotation(lookPos);
			_rotation *= Quaternion.Euler (0f,0f,90f);
			transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * 10f);
		}
	}

		
}
