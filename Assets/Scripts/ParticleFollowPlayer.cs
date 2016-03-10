using UnityEngine;
using System.Collections;

public class ParticleFollowPlayer : MonoBehaviour 
{
	[HideInInspector]
	public GameObject target;

	void Update () 
	{
        if (target != null)
        {
            this.transform.position = target.transform.position + Vector3.up;
        }
	}
}
