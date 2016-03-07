using UnityEngine;
using System.Collections;

public class NewBall : Bonus
{

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Execute(PlayerActions pA)
    {
        Debug.Log("Execute NewBall");

        MatchManager.Instance.Respawn(idTeam, true);
        MatchManager.Instance.Respawn(idTeam, true);
    }

}
