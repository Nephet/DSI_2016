using UnityEngine;
using System.Collections;

public class DestroyFootstep : MonoBehaviour {

    float _alpha;

    void Start()
    {
        _alpha = GetComponent<SpriteRenderer>().material.color.a;
    }
	
	// Update is called once per frame
	void Update () {

        if (_alpha > 0)
        {
            _alpha -= 0.01f;
            GetComponent<SpriteRenderer>().material.color = new Color(GetComponent<SpriteRenderer>().material.color.r, GetComponent<SpriteRenderer>().material.color.g, GetComponent<SpriteRenderer>().material.color.b, _alpha);
        }
        else
            Destroy(gameObject);
	
	}
}
