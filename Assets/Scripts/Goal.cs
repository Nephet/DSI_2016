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
        
		if(pA && (pA.state == PlayerActions.State.THROWBALL || pA.state == PlayerActions.State.PRISONNERBALL || pA.state == PlayerActions.State.FREEBALL))
        {
           MatchManager.Instance.AddPoint(teamId == 1 ? 2 : 1, pA.teamId == teamId ? _mM.ennemyBallPoints : _mM.playerBallPoints);
			MatchManager.Instance.RespawnPlayer (other.gameObject);
			other.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			ShakeManager.instance.LetsShake (3);
        }
        else if (other.tag == "Ball" && !pA && !other.GetComponent<Rigidbody>().isKinematic)
        {
            Debug.Log("buuuuut");
			BallsManager.instance.RemoveBall (other.gameObject);
			Destroy (other.gameObject);

            if (other.GetComponent<Ball>().ignoreSnap)
            {
                int nbPoint = other.GetComponent<Ball>().idTeam == 1 ? (_mM.teamTwoScore - _mM.teamOneScore)/2 : (_mM.teamOneScore - _mM.teamTwoScore) /2;

                nbPoint = Mathf.Clamp(nbPoint, 1, 50);

                MatchManager.Instance.AddPoint(teamId == 1 ? 2 : 1, nbPoint);
            }
            else
            {
                MatchManager.Instance.AddPoint(teamId == 1 ? 2 : 1, _mM.normalBallPoints);
            }

			MatchManager.Instance.Respawn (teamId,false);
			ShakeManager.instance.LetsShake (3);

        }


    }
}
