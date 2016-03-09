using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, GetComponent<ParticleSystem>().duration);
    }
}
