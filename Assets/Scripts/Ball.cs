using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float SpeedModifier = 1f;
    
    void OnCollisionEnter(Collision other)
    {
        SpeedModifier = 1f;
    }
}
