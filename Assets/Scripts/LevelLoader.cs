using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
        /*if (s_Instance == null)
            s_Instance = this;
        DontDestroyOnLoad(this);

        if (SceneManager.GetActiveScene().buildIndex != 0 && !b)
        {
            SceneManager.LoadSceneAsync(0);
            b = true;
        }*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
