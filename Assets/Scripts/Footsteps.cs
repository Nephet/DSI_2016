using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

    public GameObject leftFootprint;
    public GameObject rightFootprint;

    public float offset = 0.05f;

    public Transform leftFootLocation;
    public Transform rightFootLocation;

  /*  public AudioSource leftFootAudio;
    public AudioSource rightFootAudio;*/
    

    void LeftFootstep()
    {
        //leftFootAudio.Play();

        RaycastHit hit;

        if (Physics.Raycast(leftFootLocation.position, leftFootLocation.forward, out hit))
        {
            Instantiate(leftFootprint, hit.point + hit.normal*offset, Quaternion.LookRotation(hit.normal, leftFootLocation.up));
        }
    }

    void RightFootstep()
    {
        //rightFootAudio.Play();

        RaycastHit hit;

        if (Physics.Raycast(rightFootLocation.position, rightFootLocation.forward, out hit))
        {
            Instantiate(rightFootprint, hit.point + hit.normal * offset, Quaternion.LookRotation(hit.normal, rightFootLocation.up));
        }
    }

}
