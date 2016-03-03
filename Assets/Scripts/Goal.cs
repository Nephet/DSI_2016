using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public int teamId;

    MatchManager _mM;

	// Use this for initialization
	void Start () {
        _mM = MatchManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerActions pA = other.GetComponent<PlayerActions>();
        
        if(pA && pA.state != PlayerActions.State.HUMAN)
        {
           MatchManager.Instance.AddPoint(teamId == 1 ? 2 : 1, pA.teamId == teamId ? _mM.ennemyBallPoints : _mM.playerBallPoints);
			MatchManager.Instance.RespawnPlayer (other.gameObject);
			other.GetComponent<Rigidbody> ().velocity = Vector3.zero;
        }
        else if (other.tag == "Ball")
        {
            Debug.Log("buuuuut");
			BallsManager.instance.RemoveBall (other.gameObject);
			Destroy (other.gameObject);

            MatchManager.Instance.AddPoint(teamId == 1 ? 2 : 1, _mM.normalBallPoints);
			MatchManager.Instance.Respawn (teamId);


        }
    }
}
