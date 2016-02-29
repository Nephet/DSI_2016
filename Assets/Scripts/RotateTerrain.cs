using UnityEngine;
using System.Collections;

public class RotateTerrain : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0f,speed * Time.deltaTime, 0f));
	}
}
