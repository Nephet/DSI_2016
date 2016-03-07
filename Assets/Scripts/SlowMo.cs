using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowMo : MonoBehaviour {

	List<GameObject> listOfBalls;

	void OnTriggerEnter(Collider _ball)
	{
		Debug.Log (_ball.gameObject.GetComponent<PlayerActions> ());
		if (_ball.gameObject.tag == "Ball" || _ball.gameObject.GetComponent<PlayerActions> ().state == PlayerActions.State.THROWBALL) 
		{
			listOfBalls.Add (_ball.gameObject);
			//MatchManager.Instance.slow = 3;
			Time.timeScale = 0.3f;
		}

	}


	void OntriggerExit(Collider _ball)
	{
		if (_ball.gameObject.tag == "Ball" || _ball.gameObject.GetComponent<PlayerActions> ().state == PlayerActions.State.THROWBALL) 
		{
			
			listOfBalls.Remove (_ball.gameObject);
			if (listOfBalls.Count <= 0) 
			{
				//MatchManager.Instance.slow = 0;
				Time.timeScale = 1.0f;
			}
		}
	}
}
