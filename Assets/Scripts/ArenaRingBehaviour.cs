using UnityEngine;
using System.Collections;

public class ArenaRingBehaviour : MonoBehaviour {


	void Update () {

        //GetComponent<MeshRenderer>().material.SetFloat("_glowRythm", - Mathf.Clamp(Remap(MatchManager.Instance.timerDuration / Mathf.Abs(MatchManager.Instance.timer), 0, MatchManager.Instance.timerDuration, 0, 1), 0, 0.35f));
        GetComponent<MeshRenderer>().material.SetFloat("_glowRythm", - Remap(MatchManager.Instance.timerDuration / (Mathf.Abs(MatchManager.Instance.timer)+1), 0, MatchManager.Instance.timerDuration, 0.1f, 0.4f));
        //GetComponent<MeshRenderer>().material.SetFloat("_glowRythm", -Mathf.Clamp(1- (MatchManager.Instance.timer/MatchManager.Instance.timerDuration), 0, 0.5f));
        Debug.Log(GetComponent<MeshRenderer>().material.GetFloat("_glowRythm"));
    }


    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
