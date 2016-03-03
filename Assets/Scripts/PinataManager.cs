using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public List<GameObject> bonusList;

    public float reinitDelay = 5f;

    public Pinata[] pinatas;

    public Bonus BonusTeam1;
    public Bonus BonusTeam2;

    public int currentTeam;

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
        currentTeam = CheckPinatas();

        if (currentTeam == 0) return;
        
        GameObject bonus = bonusList[Random.Range(0, bonusList.Count)];

        bonus = Instantiate(bonus) as GameObject;

        bonus.GetComponent<Bonus>().idTeam = currentTeam;

        if(currentTeam == 1)
        {
            BonusTeam1 = bonus.GetComponent<Bonus>();
        }
        else
        {
            BonusTeam2 = bonus.GetComponent<Bonus>();
        }
    }

    public void ApplyBonus(PlayerActions pA)
    {
        int id = pA.teamId;

        if(id == 1 && BonusTeam1)
        {
            BonusTeam1.Execute(pA);
            BonusTeam1 = null;
        }
        else if(id == 2 && BonusTeam2)
        {
            BonusTeam2.Execute(pA);
            BonusTeam2 = null;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
