using UnityEngine;
using System.Collections;

public class PinataManager : MonoBehaviour {

    #region Singleton
    static private PinataManager s_Instance;
    static public PinataManager instance
    {
        get
        {
            return s_Instance;
        }
    }

    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad (this);
    }

    #endregion

    public Pinata[] pinatas;

	// Use this for initialization
	void Start () {
        pinatas = GameObject.FindObjectsOfType<Pinata>();
	}

    public int CheckPinatas()
    {
        int nbTeam1 = 0;
        int nbTeam2 = 0;

        int res = 0;

        foreach(Pinata p in pinatas)
        {
            if(p.teamId == 1)
            {
                nbTeam1++;
            }
            else if(p.teamId == 2)
            {
                nbTeam2++;
            }
        }

        if (nbTeam1 >= 3)
        {
            res = 1;
        }
        else if (nbTeam2 >= 3)
        {
            res = 2;
        }
        else
        {
            res = 0;
        }

        return res;

    }

    public void CheckEffect()
    {
        switch (CheckPinatas())
        {
            case 0:
                Debug.Log("nobody has it");
                break;
            case 1:
                Debug.Log("team 1 has it");
                break;
            case 2:
                Debug.Log("team 2 has it");
                break;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
