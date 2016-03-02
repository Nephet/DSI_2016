using UnityEngine;
using System.Collections;

public class TeamZone : MonoBehaviour {

    public int id;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerActions pA = other.GetComponent<PlayerActions>();
        
        if (!pA) return;

        pA.SetToBall(pA.teamId != id);
        
    }
}
