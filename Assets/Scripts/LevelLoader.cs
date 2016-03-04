using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    static private LevelLoader s_Instance;
    static public LevelLoader instance
    {
        get
        {
            return s_Instance;
        }
    }

    static bool b = false;

    // Use this for initialization
    void Awake () {
        if (s_Instance == null)
            s_Instance = this;
        DontDestroyOnLoad(this);

        if (Application.loadedLevel != 0 && !b)
        {
            Application.LoadLevel(0);
            b = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
