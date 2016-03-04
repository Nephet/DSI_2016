using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (MatchManager.Instance.pause) 
		{
			if(Input.GetButtonDown("A_Button_1"))
			{
				Destroy (FindObjectOfType<SelectionManager> ().gameObject);
				SceneManager.LoadSceneAsync ("Splash");
			}

		}
	}
}
