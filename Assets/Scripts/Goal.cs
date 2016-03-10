using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour 
{
    public int teamId;

    MatchManager _mM;

	//**** PARTICLES ****
	public GameObject partGoalFire1;
	public GameObject partGoalFire2;
	public GameObject partConfettis;
	public GameObject partFireworks;

	// Use this for initialization
	void Start () 
	{
        _mM = MatchManager.Instance;
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerActions pA = other.GetComponent<PlayerActions>();
        
		if(pA && (pA.state == PlayerActions.State.THROWBALL || pA.state == PlayerActions.State.PRISONNERBALL || pA.state == PlayerActions.State.FREEBALL))
        {
			// particles
			StartParticles ();
            MatchManager.Instance.AddPoint(teamId == 1 ? 2 : 1, pA.teamId == teamId ? _mM.ennemyBallPoints : _mM.playerBallPoints);
			MatchManager.Instance.RespawnPlayer (other.gameObject);
			other.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			ShakeManager.instance.LetsShake (3);
        }

        else if (other.tag == "Ball" && !pA && !other.GetComponent<Rigidbody>().isKinematic)
        {
			// particles
			StartParticles ();

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

	void StartParticles()
	{
		//print ("anything");

		// Fire_1
		//print(transform.GetChild(0).transform.position + " << FIRE1");
		GameObject _partClone1 = Instantiate (partGoalFire1, transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
		Destroy (_partClone1, 5f);

		// Fire_2
		//print(transform.GetChild(1).transform.position + " << FIRE2");
		GameObject _partClone2 = Instantiate (partGoalFire2, transform.GetChild(1).transform.position, Quaternion.identity) as GameObject;
		Destroy (_partClone2, 5f);

		// Confettis
		GameObject _partClone3 = Instantiate (partConfettis, transform.GetChild(2).transform.position, Quaternion.identity) as GameObject;
		Destroy (_partClone3, 5f);

		// Fireworks
		GameObject _partClone4 = Instantiate (partFireworks, transform.GetChild(3).transform.position, Quaternion.identity) as GameObject;
		Destroy (_partClone4, 1.5f);
	}
}