using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public int teamId;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            Debug.Log("buuuuut");

            MatchManager.Instance.AddPoint(teamId == 1 ? 2 : 1, 1);

        }
    }
}
