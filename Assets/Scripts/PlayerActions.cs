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

    public int teamId;

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
        if (Input.GetButtonDown("A_Button_"+id) && currentBall)
        {
            Throw(throwPower);
        }
    }

    void Throw(float power)
    {
        currentBall.GetComponent<Rigidbody>().isKinematic = false;
        currentBall.transform.parent = null;
        currentBall.GetComponent<Rigidbody>().AddForce(mesh.transform.forward * power, ForceMode.Impulse);
    }
}
