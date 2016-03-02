using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float SpeedModifier = 1f;
    
    void OnCollisionEnter(Collision other)
    {
        if (!enabled) return;
        SpeedModifier = 1f;
    }
}
