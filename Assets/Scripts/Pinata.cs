using UnityEngine;
using System.Collections;

public class Pinata : MonoBehaviour {

    public int teamId;

	// Use this for initialization
	void Start () {
        teamId = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        PlayerActions pA = other.transform.GetComponent<PlayerActions>();

        if (!pA || pA.state == PlayerActions.State.HUMAN) return;

        ChangeTeam(other.transform.GetComponent<Ball>().idTeam);
        
    }

    void ChangeTeam(int id)
    {
        teamId = id;

        PinataManager.instance.CheckEffect();
        
        ChangeColor(id == 1 ? Color.red : id == 2 ? Color.blue : Color.white);
    }

    void ChangeColor(Color c)
    {
        Debug.Log(GetComponentInChildren<MeshRenderer>().material.color + " " + c);
        GetComponentInChildren<MeshRenderer>().material.color = c;
    }
}
