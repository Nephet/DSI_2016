using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    public int idTeam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Execute(PlayerActions pA)
    {
        Debug.Log("Execute Bonus");
    }
}
