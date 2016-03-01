using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour {

    static MatchManager instance;

    public int teamOneScore = 0;
    public int teamTwoScore = 0;

    public float timerDuration = 5f;
    public float timer;
    float timerStart;

    public static MatchManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;

        timer = timerDuration;

        timerStart = Time.time;
    }

    void Update()
    {
        timer = timerDuration - (Time.time - timerStart);

        if(timer < 0)
        {
            Debug.Log("end");
        }
    }

    public void AddPoint(int id, int score)
    {
        Debug.Log(id + " " + score);

        if(id == 1)
        {
            teamOneScore += score;
        }
        else if(id == 2)
        {
            teamTwoScore += score;
        }
    }

}
