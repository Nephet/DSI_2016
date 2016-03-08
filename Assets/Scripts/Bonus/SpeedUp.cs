using UnityEngine;
using System.Collections;

public class SpeedUp : Bonus {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Execute(PlayerActions pA)
    {
        Debug.Log("Execute SpeedUp");

        pA.frenzy = true;
    }
}
