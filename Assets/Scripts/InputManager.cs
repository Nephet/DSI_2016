using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	bool pressButton = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (MatchManager.Instance.pause) {
			

		} 
		else if (MatchManager.Instance.endGame) {
			StartCoroutine(Wait (2.0f));
			if (pressButton) {

				if (Input.GetButtonDown ("A_Button_1")) {
					MatchManager.Instance.pause = false;
					Time.timeScale = 1.0f;
					Destroy (FindObjectOfType<SelectionManager> ().gameObject);
					pressButton = false;
					SceneManager.LoadSceneAsync ("MainMenu");
				}
			}

		}
	}

	IEnumerator Wait(float _time)
	{
		yield return new WaitForSeconds (_time);
		pressButton = true;
	}
}
