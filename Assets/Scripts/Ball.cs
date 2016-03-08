using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    
	public bool respawning = false;
	IEnumerator currentCoroutine;

    int[] _speedMaxByPowerLevel;
    
    public int currentPowerLevel;

    public int currentSpeed;

    public int idTeam;
    public int idPlayer;

    public bool ignoreSnap;

	public GameObject parentMeshBall;
	public GameObject meshBall;

    public bool snakeBool = false;
    int _right = 1;

    Rigidbody _rigidB;

    AnimationCurve _snakeCurve;

    float _timer;

    void Start()
    {
        _timer = 0;

        ignoreSnap = false;

        currentPowerLevel = 0;

        _speedMaxByPowerLevel = BallsManager.instance.speedMaxByPowerLevel;

        _snakeCurve = PinataManager.instance.snakeBallDirection;

        _rigidB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 1.0f) { _timer = 0; }

        RotateMesh ();
        PlayerActions pA = GetComponent<PlayerActions>();
        
        /*Debug.Log(transform.GetChild(0).name);
        Debug.DrawLine(transform.GetChild(0).position, transform.GetChild(0).position + transform.GetChild(0).up,Color.red,1000);*/

        if (snakeBool && GetComponent<Rigidbody>().velocity.magnitude > BallsManager.instance.throwMinVelocity && !respawning)
        {
            //_rigidB.AddForce(transform.GetChild(0).up * ( _snakeCurve.Evaluate(_timer)) * 100, ForceMode.Force);
            transform.Translate(transform.GetChild(0).up * (_snakeCurve.Evaluate(_timer)) * 100);
            
        }

        if (!pA || pA.state != PlayerActions.State.THROWBALL || GetComponent<Rigidbody>().velocity.magnitude == 0) return;

		if (GetComponent<Rigidbody> ().velocity.magnitude <= BallsManager.instance.throwMinVelocity) {

			GetComponent<Rigidbody> ().velocity = Vector3.zero;
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

    void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;
        
        if (gameObject.GetComponent<PlayerActions>() && gameObject.GetComponent<PlayerActions>().dashing)
        {
            if (other.gameObject.GetComponent<PlayerActions>() && other.gameObject.GetComponent<PlayerActions>().teamId != gameObject.GetComponent<PlayerActions>().teamId)
            {
                other.gameObject.GetComponent<PlayerActions>().Stun();
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
		
		if (GetComponent<PlayerActions> ()) {
			Debug.Log ("test");
			Debug.Log (Vector3.Dot(transform.position, transform.position + GetComponent<Rigidbody> ().velocity));
			meshBall.transform.Rotate (meshBall.transform.right*Mathf.Sign(Vector3.Dot(transform.position, transform.position + GetComponent<Rigidbody> ().velocity))*100f * GetComponent<Rigidbody> ().velocity.magnitude * Time.deltaTime);

		} else {
			parentMeshBall.transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody> ().velocity, Vector3.forward);
			meshBall.transform.Rotate (Vector3.forward*Mathf.Sign(Vector3.Dot(transform.position, transform.position + GetComponent<Rigidbody> ().velocity))*500f * GetComponent<Rigidbody> ().velocity.magnitude * Time.deltaTime);
		}



	}
}
