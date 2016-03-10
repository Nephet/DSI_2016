﻿using UnityEngine;
using System.Collections;
using ParticlePlayground;

public class Goal : MonoBehaviour 
{
    public int teamId;

    MatchManager _mM;

    //**** PARTICLES ****
    //public PlaygroundParticlesC partGoalFire1;
    //public PlaygroundParticlesC partGoalFire2;
    public GameObject partGoalFire1;
    public GameObject partGoalFire2;
    public GameObject partConfettis;
	public GameObject partFireworks;

	// Use this for initialization
	void Start () 
	{
        _mM = MatchManager.Instance;

        // Particles
        //partGoalFire1.emit = false;
        //partGoalFire2.emit = false;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerActions pA = other.GetComponent<PlayerActions>();
        
		if(pA && (pA.state == PlayerActions.State.THROWBALL || pA.state == PlayerActions.State.PRISONNERBALL || pA.state == PlayerActions.State.FREEBALL))
        {
            // particles BALLE PLAYER
            ParticlesPlayerScore();
            
            MatchManager.Instance.AddPoint(teamId == 1 ? 2 : 1, pA.teamId == teamId ? _mM.ennemyBallPoints : _mM.playerBallPoints);
			MatchManager.Instance.RespawnPlayer (other.gameObject);
			other.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			ShakeManager.instance.LetsShake (3);
        }

        else if (other.tag == "Ball" && !pA && !other.GetComponent<Rigidbody>().isKinematic)
        {
            // particles BALLE NORMALE
            ParticlesNormalScore();
     

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

	void ParticlesNormalScore()
	{
        // Confettis
        GameObject _partClone3 = Instantiate (partConfettis, transform.GetChild(2).transform.position, Quaternion.identity) as GameObject;
		Destroy (_partClone3, 5f);
	}

    void ParticlesPlayerScore()
    {
        // Fire_1
        
        GameObject _partClone1 = Instantiate (partGoalFire1, transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
        Destroy (_partClone1, 1.25f);
       

        // Fire_2
        GameObject _partClone2 = Instantiate (partGoalFire2, transform.GetChild(1).transform.position, Quaternion.identity) as GameObject;
        Destroy (_partClone2, 1.25f);
        
 
        // Confettis
        GameObject _partClone3 = Instantiate(partConfettis, transform.GetChild(3).transform.position, Quaternion.identity) as GameObject;
        Destroy(_partClone3, 5f);
    }
}