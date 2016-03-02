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

        Debug.Log(pA.GetComponent<Rigidbody>().velocity);

        if (Mathf.Abs(pA.GetComponent<Rigidbody>().velocity.x) < 0.01f || Mathf.Abs(pA.GetComponent<Rigidbody>().velocity.z) < 0.01f)
        {
            pA.SetToBall(pA.teamId != id);
        }
    }
}
