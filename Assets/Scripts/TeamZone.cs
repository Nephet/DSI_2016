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

        pA.currentZone = id;

        switch (pA.state)
        {
            case PlayerActions.State.HUMAN:
                pA.SetToBall(pA.teamId != id);
                break;
            case PlayerActions.State.FREEBALL:
                pA.state = pA.teamId != id ? PlayerActions.State.PRISONNERBALL : PlayerActions.State.FREEBALL;
                break;
            case PlayerActions.State.PRISONNERBALL:
                pA.state = pA.teamId != id ? PlayerActions.State.PRISONNERBALL : PlayerActions.State.FREEBALL;
                break;
            case PlayerActions.State.TAKENBALL:
                
                break;
            case PlayerActions.State.THROWBALL:
                
                break;
        }
    }
}
