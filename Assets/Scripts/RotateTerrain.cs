using UnityEngine;
using System.Collections;

public class RotateTerrain : MonoBehaviour {

	public float speed;

    public int direction = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (MatchManager.Instance.pause)
			return;
		//_direction = MatchManager.Instance.direction;
		transform.Rotate(new Vector3(0f,speed * Time.deltaTime * direction, 0f));
	}
}
