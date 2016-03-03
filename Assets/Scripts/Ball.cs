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

    void Start()
    {
        ignoreSnap = false;

        currentPowerLevel = 0;

        _speedMaxByPowerLevel = BallsManager.instance.speedMaxByPowerLevel;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!enabled) return;
        
        if (respawning) {
			respawning = false;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Rigidbody> ().freezeRotation = true;
		} else if(!gameObject.GetComponent<PlayerActions>()) {
			GetComponent<Rigidbody> ().freezeRotation = false;
		}

		if (gameObject.GetComponent<PlayerActions>() && gameObject.GetComponent<PlayerActions>().dashing) 
		{
			
			if (other.gameObject.GetComponent<PlayerActions> () && other.gameObject.GetComponent<PlayerActions> ().teamId != gameObject.GetComponent<PlayerActions>().teamId ) 
			{
				other.gameObject.GetComponent<PlayerActions> ().Stun ();
			}
		}

		/*if (currentCoroutine != null) 
		{
			StopCoroutine (currentCoroutine);
			currentCoroutine = null;
		}*/


    }

    public void StartPowerDrop()
    {
        StartCoroutine(PowerDrop());
    }

    public void StopPowerDrop()
    {
        StopCoroutine(PowerDrop());
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
        StopCoroutine(SpeedDrop());
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

    /*public void LaunchCoroutine()
	{
		currentCoroutine = MoveRespawn (this.gameObject, MatchManager.Instance.width, MatchManager.Instance.height, MatchManager.Instance.respawnSpeed, MatchManager.Instance.respawnGoal1.transform.right);
		StartCoroutine (currentCoroutine);
	}*/

    /*IEnumerator MoveRespawn(GameObject _go, float _width, float _height, float _respawnSpeed, Vector3 _dir)
	{

		if (_go.GetComponent<Ball>().respawning)
			yield break;

		_go.GetComponent<Ball>().respawning = true;
		Vector3 startPos = _go.transform.localPosition;
		float timer = 0.0f;
		_dir.Normalize ();

		while (_go.GetComponent<Ball>().respawning) 
		{
			float currentHeight = ((4f / _width) * _height * (1+ timer)) - ((4f / (_width * _width)) * _height * ((1+ timer)*(1+ timer)));
			_go.transform.localPosition = new Vector3 ((startPos.x + timer) * _go.transform.right.x, currentHeight, _go.transform.localPosition.z * _go.transform.right.z);
			timer += Time.deltaTime * (_respawnSpeed * _width / 7f);
			yield return null;
		}
	}*/
}
