using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour {

    static MatchManager instance;

    public int teamOneScore = 0;
    public int teamTwoScore = 0;

    public int normalBallPoints = 1;
    public int playerBallPoints = 5;
    public int ennemyBallPoints = 8;

    public float timerDuration = 5f;
    public float timer;
    float _timerStart;

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

        _timerStart = Time.time;
    }

    void Update()
    {
        timer = timerDuration - (Time.time - _timerStart);

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
