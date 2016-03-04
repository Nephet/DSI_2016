using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
    
	// Use this for initialization
	void Awake () {
	    if(Application.loadedLevel != 0)
        {
            Application.LoadLevel(0);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
