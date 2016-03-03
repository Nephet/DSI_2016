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

        StartCoroutine(Reinit());
    }

    IEnumerator Reinit()
    {
        yield return new WaitForSeconds(PinataManager.instance.reinitDelay);

        ChangeTeam(0);
    }

    void ChangeTeam(int id)
    {
        teamId = id;
        
        ChangeColor(id == 1 ? Color.red : id == 2 ? Color.blue : Color.white);

        if (id != 0)
        {
            PinataManager.instance.CheckEffect(0);
        }

    }

    void ChangeColor(Color c)
    {
        Debug.Log(GetComponentInChildren<MeshRenderer>().material.color + " " + c);
        GetComponentInChildren<MeshRenderer>().material.color = c;
    }
}
