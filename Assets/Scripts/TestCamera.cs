using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {

	public GameObject CamFIFA;
	public GameObject GoalCam1;
	public GameObject GoalCam2;

	public bool camFIFA;
	public bool goalCam;


	// Use this for initialization
	void Start () {
		camFIFA = true;
		goalCam = false;
		CamFIFA.SetActive(true);
		GoalCam1.SetActive(false);
		GoalCam2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(camFIFA)
		{
			goalCam = false;

			CamFIFA.SetActive(true);
			GoalCam1.SetActive(false);
			GoalCam2.SetActive(false);
		}
		if(goalCam)
		{
			camFIFA = false;

			CamFIFA.SetActive(false);
			GoalCam1.SetActive(true);
			GoalCam2.SetActive(true);
		}
	}
}
