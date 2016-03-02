using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float SpeedModifier = 1f;
	public bool respawning = false;
	IEnumerator currentCoroutine;
    
    void OnCollisionEnter(Collision other)
    {
        if (!enabled) return;
        SpeedModifier = 1f;
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
