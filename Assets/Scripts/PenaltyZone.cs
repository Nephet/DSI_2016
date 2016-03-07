using UnityEngine;
using System.Collections;

public class PenaltyZone : MonoBehaviour {

    float timeInGoal;
    float enterTime;

    int nbPlayerInGoal = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerActions>())
        {
            nbPlayerInGoal++;
        }

        if(nbPlayerInGoal == 2)
        {
            enterTime = Time.time;
        }
    }

    void OnTriggerStay(Collider other)
    {
        timeInGoal = Time.time - enterTime;

        if(timeInGoal > MatchManager.Instance.timeBeforeBooing)
        {
            Debug.Log("Boooo");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            nbPlayerInGoal++;
        }
        if (nbPlayerInGoal == 0)
        {
            timeInGoal = 0;
        }
    }
}
