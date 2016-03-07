using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour {


	Vector3 _centerPlayer;
	Vector3 _centerCam;
	public GameObject center;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateViewCam ();
	}

	void UpdateViewCam()
	{
		Vector3 _betweenPlayer12 = (PlayerManager.instance.listPlayers [0].transform.position + PlayerManager.instance.listPlayers [1].transform.position) / 2;
		Vector3 _betweenPlayer34 = (PlayerManager.instance.listPlayers [2].transform.position + PlayerManager.instance.listPlayers [3].transform.position) / 2;
	
		_centerPlayer = (_betweenPlayer12 + _betweenPlayer34) / 2;
		_centerCam = (_centerPlayer + center.transform.position)/4;
		transform.LookAt (_centerCam);
	
	}
}
