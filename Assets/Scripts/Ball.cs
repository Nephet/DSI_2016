using UnityEngine;
using System.Collections;
using ParticlePlayground;

public class Ball : MonoBehaviour {
    
	public bool respawning = false;
	IEnumerator currentCoroutine;

    int[] _speedMaxByPowerLevel;
    
    public int currentPowerLevel;

	public int maxPowerLevel;

    public int currentSpeed;

    public int idTeam;
    public int idPlayer;

    public bool ignoreSnap;

	public GameObject parentMeshBall;
	public GameObject meshBall;

    public GameObject trailLvl1;
    public GameObject trailLvl2;
    public PlaygroundParticlesC volleyParticles;

    public bool snakeBool = false;
    int _right = 1;

	public bool bounce = false;

    Rigidbody _rigidB;

    AnimationCurve _snakeCurve;

    float _timer;

	// **** PARTICLES ****
	public GameObject partBallDust;
    
    void Start()
    {
        _timer = 0;

        ignoreSnap = false;

        currentPowerLevel = 0;

        _speedMaxByPowerLevel = BallsManager.instance.speedMaxByPowerLevel;

        _snakeCurve = PinataManager.instance.snakeBallDirection;

        _rigidB = GetComponent<Rigidbody>();

        volleyParticles.emit = false;
        // Change color of trail
        

        
    }

    void Update()
    {
        ChangeTrail();
        //Debug.DrawLine(parentMeshBall.transform.position, parentMeshBall.transform.position + parentMeshBall.transform.forward, Color.red, 100);

        /*RaycastHit hit;

        if(Physics.Raycast(parentMeshBall.transform.position, parentMeshBall.transform.forward,out hit, 1f))
        {

            PlayerActions pAc = GetComponent<PlayerActions>();

            if (hit.transform.tag == "But" && pAc && pAc.state == PlayerActions.State.THROWBALL)
            {
                MatchManager.Instance.StartSlowMo(.5f);
            }
            
        }*/

        _timer += Time.deltaTime;
        if (_timer > 1.0f) { _timer = 0; }

        
        PlayerActions pA = GetComponent<PlayerActions>();
        
        /*Debug.Log(transform.GetChild(0).name);
        Debug.DrawLine(transform.GetChild(0).position, transform.GetChild(0).position + transform.GetChild(0).up,Color.red,1000);*/

        if (snakeBool && GetComponent<Rigidbody>().velocity.magnitude > BallsManager.instance.throwMinVelocity && !respawning)
        {
            //_rigidB.AddForce(transform.GetChild(0).up * ( _snakeCurve.Evaluate(_timer)) * 100, ForceMode.Force);
            transform.Translate(transform.GetChild(0).up * (_snakeCurve.Evaluate(_timer)) * 100);
            
        }

        if (!pA || pA.state != PlayerActions.State.THROWBALL || GetComponent<Rigidbody>().velocity.magnitude == 0) return;

		if (GetComponent<Rigidbody> ().velocity.magnitude <= BallsManager.instance.throwMinVelocity && (Mathf.Abs(Input.GetAxis("L_XAxis_" + pA.id)) + Mathf.Abs(Input.GetAxis("L_YAxis_" + pA.id))) > .1f) {

			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			pA.state = pA.currentZone == pA.teamId ? PlayerActions.State.FREEBALL : PlayerActions.State.PRISONNERBALL;
		}


    }

	void FixedUpdate()
	{

		RotateMesh ();
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

		if (other.gameObject.tag == "Wall") 
		{
			GameObject _partClone = Instantiate (partBallDust, this.transform.position, Quaternion.identity) as GameObject;
			Destroy (_partClone, 5.0f);
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

            //UpdatePowerLevel();
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

		ChangeTrail ();
    }

	void RotateMesh()
	{
		

		if (GetComponent<PlayerActions> ()) {
			//parentMeshBall.transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody> ().velocity, Vector3.up);
			if (!GetComponent<Movement> ().moving && _rigidB.velocity!=Vector3.zero) {
				
				parentMeshBall.transform.rotation = Quaternion.LookRotation (_rigidB.velocity, Vector3.up);
			}

			meshBall.transform.Rotate (Vector3.right*100f* GetComponent<Rigidbody> ().velocity.magnitude * Time.deltaTime);
			//meshBall.transform.Rotate (meshBall.transform.right*Mathf.Sign(Vector3.Dot(transform.position, transform.position + GetComponent<Rigidbody> ().velocity))*100f * GetComponent<Rigidbody> ().velocity.magnitude * Time.deltaTime);

		} else {
            if(GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                parentMeshBall.transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity, Vector3.forward);
            }
			meshBall.transform.Rotate (Vector3.forward*Mathf.Sign(Vector3.Dot(transform.position, transform.position + GetComponent<Rigidbody> ().velocity))*500f * GetComponent<Rigidbody> ().velocity.magnitude * Time.deltaTime);
		}
	}

	void ChangeTrail()
	{
        if (currentPowerLevel == 0)
        {
            trailLvl1.SetActive(false);
            trailLvl2.SetActive(false);
            meshBall.GetComponent<MeshRenderer>().material.SetFloat("_glowIntensity", 0f);
        }

        if (currentPowerLevel == 1) 
		{
            trailLvl1.SetActive (true);
            trailLvl2.SetActive (false);
            meshBall.GetComponent<MeshRenderer>().material.SetFloat("_glowIntensity", 0.8f);
		} 

		else if (currentPowerLevel == 2)
		{
            trailLvl1.SetActive (false);
            trailLvl2.SetActive (true);
            meshBall.GetComponent<MeshRenderer>().material.SetFloat("_glowIntensity", 2f);
        }
    }
}
