using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerActions : MonoBehaviour {

	public GameObject currentBall;

	public List<GameObject> balls;

    GameObject mesh;

    public float throwPower = 5f;

    public static int nbPlayers;
    public int id;

	bool Snap;
	public float rangeSnap = 1.0f;
	float nearestdistance = Mathf.Infinity;
	GameObject nearestBall;

    void Awake()
    {
        nbPlayers++;

        id = nbPlayers;
    }

    void Start()
    {
        mesh = GetComponent<Movement>().mesh;

        if (currentBall)
        {
            currentBall.GetComponent<Rigidbody>().isKinematic = true;
            currentBall.transform.parent = mesh.transform;
            currentBall.transform.position = transform.position + mesh.transform.forward/2;
        }
    }

    void Update()
    {
		Snap = Input.GetButtonDown ("A_Button_"+id);

		if (Snap && currentBall)
        {
            Throw(throwPower);
        }
		else if (Snap) {
			DistanceBalls ();
			if (nearestBall != null) {
				currentBall = nearestBall;
				currentBall.GetComponent<Rigidbody> ().isKinematic = true;
				currentBall.transform.parent = mesh.transform;
				currentBall.transform.position = transform.position + currentBall.transform.forward /2;
			}

		}
    }

    void Throw(float power)
    {
        currentBall.GetComponent<Rigidbody>().isKinematic = false;
        currentBall.transform.parent = null;
        currentBall.GetComponent<Rigidbody>().AddForce(mesh.transform.forward * power, ForceMode.Impulse);
    }

	void DistanceBalls()
	{
		for (int i = 0; i < BallsManager.instance.balls.Count; i++) 
		{
			float distance = Vector3.Distance (transform.position, BallsManager.instance.balls [i].transform.position);
			if (distance < nearestdistance && distance <= rangeSnap) {
				nearestBall = BallsManager.instance.balls [i].gameObject;
			}
		}
	}
}
