using UnityEngine;
using System.Collections;

public class PiercingBall : Bonus {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Execute(PlayerActions pA)
    {
        Debug.Log("Execute PiercingBall");

        pA.GetComponent<SphereCollider>().isTrigger = true;
    }

}
