using UnityEngine;
using System.Collections;

public class RotateTerrain : MonoBehaviour {

	public float speed;

    int _direction = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		_direction = MatchManager.Instance.direction;
		transform.Rotate(new Vector3(0f,speed * Time.deltaTime * _direction, 0f));
	}
}
