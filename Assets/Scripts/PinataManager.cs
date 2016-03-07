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

    public List<GameObject> bonusListWeak;
    public List<GameObject> bonusListStrong1;
    public List<GameObject> bonusListStrong2;

    public float reinitDelay = 5f;

    public Pinata[] pinatas;

    public Bonus BonusTeam1;
    public Bonus BonusTeam2;

    public int currentTeam;

    public int weakPercentageEgality = 75;
    public int strong1PercentageEgality = 15;
    public int strong2PercentageEgality = 10;

    public int weakPercentageIncrease = -14;
    public int strong1PercentageIncrease = 6;
    public int strong2PercentageIncrease = 8;

    public int weakPercentageMax = 5;
    public int strong1PercentageMax = 45;
    public int strong2PercentageMax = 50;

    public int destroyBallDelay = 20;

    public float speedMultiplicator = 1.15f;
    public float speedUpDelay = 10f;

    public float timeBeforeDestroy = 10f;

    public AnimationCurve snakeBallDirection;

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

    public void CheckEffect(int id)
    {
        if (id > 0)
        {
            currentTeam = id;
        }
        else
        {
            currentTeam = CheckPinatas();
        }

        if (currentTeam == 0) return;
        
        GameObject bonus = GetBonus(currentTeam);

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

        StartCoroutine(PrepareDelete(currentTeam));
    }

    IEnumerator PrepareDelete(int id)
    {
        yield return new WaitForSeconds(timeBeforeDestroy);

        DeleteBonus(id);
    }

    void DeleteBonus(int id)
    {
        if (id == 1 && BonusTeam1)
        {
            Destroy(BonusTeam1.gameObject);
            BonusTeam1 = null;
        }
        else if(BonusTeam2)
        {
            Destroy(BonusTeam2.gameObject);
            BonusTeam2 = null;
        }
    }

    GameObject GetBonus(int idTeam)
    {
        GameObject bonus = null;
        
        int score = idTeam == 1 ? MatchManager.Instance.teamOneScore : MatchManager.Instance.teamTwoScore;
        int scoreEnemy = idTeam == 2 ? MatchManager.Instance.teamOneScore : MatchManager.Instance.teamTwoScore;

        int prcFaible = Mathf.Clamp(weakPercentageEgality + (scoreEnemy - score) * weakPercentageIncrease, weakPercentageMax,weakPercentageEgality);
        int prcFort1 = Mathf.Clamp(strong1PercentageEgality + (scoreEnemy - score) * strong1PercentageIncrease, strong1PercentageEgality, strong1PercentageMax);
        int prcFort2 = Mathf.Clamp(strong2PercentageEgality + (scoreEnemy - score) * strong2PercentageIncrease, strong2PercentageEgality, strong2PercentageMax);

        int randomNumber = ((int)(Random.Range(0, 10000) * Time.time))%101;


        if(randomNumber < prcFaible)
        {
            Debug.Log("Faible : " + prcFaible + " |Fort1 : " + (prcFaible+prcFort1) + " |Fort2 : " + (prcFaible+ prcFort1+prcFort2) + " |Random : " + randomNumber + " -> faible");
            bonus = bonusListWeak[Random.Range(0, bonusListWeak.Count)];
        }
        else if(randomNumber < prcFaible + prcFort1)
        {
            Debug.Log("Faible : " + prcFaible + " |Fort1 : " + (prcFaible + prcFort1) + " |Fort2 : " + (prcFaible + prcFort1 + prcFort2) + " |Random : " + randomNumber + " -> fort1");
            bonus = bonusListStrong1[Random.Range(0, bonusListWeak.Count)];
        }
        else
        {
            Debug.Log("Faible : " + prcFaible + " |Fort1 : " + (prcFaible + prcFort1) + " |Fort2 : " + (prcFaible + prcFort1 + prcFort2) + " |Random : " + randomNumber + " -> fort2");
            bonus = bonusListStrong2[Random.Range(0, bonusListWeak.Count)];
        }

        return bonus;
    }

    public void ApplyBonus(PlayerActions pA)
    {
        int id = pA.teamId;

        if(id == 1 && BonusTeam1)
        {
            BonusTeam1.Execute(pA);
            Destroy(BonusTeam1.gameObject);
            BonusTeam1 = null;
        }
        else if(id == 2 && BonusTeam2)
        {
            BonusTeam2.Execute(pA);
            Destroy(BonusTeam2.gameObject);
            BonusTeam2 = null;
        }
    }


}
