using UnityEngine;
using System.Collections;
using DG.Tweening;

public class dottweentest : MonoBehaviour {


    public GameObject testObject;

	// Use this for initialization
	void Start () {
        testObject.transform.DOMoveX(100, 1);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
