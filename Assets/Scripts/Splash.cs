using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (SwitchScene());
	}

	IEnumerator SwitchScene()
	{
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadSceneAsync ("TeamSelection");
	}
}
