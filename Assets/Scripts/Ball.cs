using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    
	public bool respawning = false;
	IEnumerator currentCoroutine;

    int[] _speedMaxByPowerLevel;
    
    public int currentPowerLevel;

    public int currentSpeed;

    public int idTeam;

    public bool ignoreSnap;

	public GameObject parentMeshBall;
	public GameObject meshBall;

    void Start()
    {
        ignoreSnap = false;

        currentPowerLevel = 0;

        _speedMaxByPowerLevel = BallsManager.instance.speedMaxByPowerLevel;
    }

    void Update()
    {
		
		RotateMesh ();
        PlayerActions pA = GetComponent<PlayerActions>();

        if (!pA || pA.state != PlayerActions.State.THROWBALL || GetComponent<Rigidbody>().velocity.magnitude == 0) return;

		if (GetComponent<Rigidbody> ().velocity.magnitude <= BallsManager.instance.throwMinVelocity) {

			pA.state = pA.currentZone == pA.teamId ? PlayerActions.State.FREEBALL : PlayerActions.State.PRISONNERBALL;
		} 

    }

    void OnCollisionEnter(Collision other)
    {
        if (!enabled) return;
        
        if (respawning) {
			respawning = false;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			//GetComponent<Rigidbody> ().freezeRotation = true;
		} else if(!gameObject.GetComponent<PlayerActions>()) {
			//GetComponent<Rigidbody> ().freezeRotation = false;
		}

		if (gameObject.GetComponent<PlayerActions>() && gameObject.GetComponent<PlayerActions>().dashing) 
		{
			
			if (other.gameObject.GetComponent<PlayerActions> () && other.gameObject.GetComponent<PlayerActions> ().teamId != gameObject.GetComponent<PlayerActions>().teamId ) 
			{
				other.gameObject.GetComponent<PlayerActions> ().Stun ();
			}
		}


    }

    public void StartPowerDrop()
    {
		
		StartCoroutine(PowerDrop ());
    }

    public void StopPowerDrop()
    {
		StopAllCoroutines ();
		//StopCoroutine(PowerDrop ());
    }

    IEnumerator PowerDrop()
    {
        while (currentPowerLevel > 1)
        {
            yield return new WaitForSeconds(BallsManager.instance.powerLevelDropDelay);

            currentPowerLevel--;
        }

        yield return null;
    }

    public void StartSpeedDrop()
    {
		StartCoroutine(SpeedDrop());
    }
    
    public void StopSpeedDrop()
    {
		StopAllCoroutines ();
		//StopCoroutine(SpeedDrop() );
    }

    IEnumerator SpeedDrop()
    {
        currentSpeed = BallsManager.instance.speedMaxByPowerLevel[currentPowerLevel-1];
        
        while (currentPowerLevel > 0)
        {
            yield return new WaitForSeconds(BallsManager.instance.speedDropDelay);

            currentSpeed -= BallsManager.instance.speedDropAmount;

            UpdatePowerLevel();
        }

        yield return null;
    }

    public void UpdatePowerLevel()
    {
        int[] tab = BallsManager.instance.speedMaxByPowerLevel;

        for (int i = 0; i < tab.Length - 1; i++)
        {
            if (currentSpeed <= tab[i] && currentSpeed > 0)
            {
                currentPowerLevel = i+1;
                break;
            }
            else if (currentSpeed <= 0)
            {
                currentSpeed = 0;
                currentPowerLevel = 0;
                break;
            }
        }
    }

	void RotateMesh()
	{
		parentMeshBall.transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody> ().velocity, Vector3.forward);
		meshBall.transform.Rotate (Vector3.forward*Mathf.Sign(Vector3.Dot(transform.position, transform.position + GetComponent<Rigidbody> ().velocity))*500f * GetComponent<Rigidbody> ().velocity.magnitude * Time.deltaTime);

	}
}
