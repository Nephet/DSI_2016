using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {

	public GameObject CamFIFA;
	public GameObject GoalCam1;
	public GameObject GoalCam2;
	public GameObject ThirdCam;

	public bool camFIFA;
	public bool goalCam;
	public bool thirdCam;


	// Use this for initialization
	void Start () {
		camFIFA = true;
		goalCam = false;
		thirdCam = false;
		CamFIFA.SetActive(true);
		GoalCam1.SetActive(false);
		GoalCam2.SetActive(false);
		ThirdCam.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(camFIFA)
		{
			goalCam = false;
			thirdCam = false;

			CamFIFA.SetActive(true);
			GoalCam1.SetActive(false);
			GoalCam2.SetActive(false);
			ThirdCam.SetActive(false);
		}
		if(goalCam)
		{
			camFIFA = false;
			thirdCam = false;

			CamFIFA.SetActive(false);
			GoalCam1.SetActive(true);
			GoalCam2.SetActive(true);
			ThirdCam.SetActive(false);
		}
		if(thirdCam)
		{
			camFIFA = false;
			goalCam = false;

			CamFIFA.SetActive(false);
			GoalCam1.SetActive(false);
			GoalCam2.SetActive(false);
			ThirdCam.SetActive(true);
		}
	}
}
