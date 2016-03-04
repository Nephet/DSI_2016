using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuTest : MonoBehaviour {



	public Image circleMenu;

	public int index = 0;

	public List<float> buttonAngles = new List<float>();

	//inputs
	public float joyX;
	public float joyY;

	public bool isTurning;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		joyX = Input.GetAxis("L_XAxis_0");
		joyY = Input.GetAxis("L_YAxis_0");

		index = (int)modulo((index), buttonAngles.Count);

		if (Input.GetKeyDown(KeyCode.A)){
			circleMenu.transform.eulerAngles = new Vector3(0, 0, -200);
		}

		if (!isTurning){
			if (joyX < 0){
				float tempRot = circleMenu.transform.eulerAngles.z;
				if (index == 0){
					tempRot = circleMenu.transform.eulerAngles.z + 360;
					Debug.Log("yo");
				}
				Debug.Log(circleMenu.transform.eulerAngles.z);
				StartCoroutine(Turn(tempRot, (float)buttonAngles[(int)modulo((index-1), buttonAngles.Count)]));
				index--;
				isTurning = true;
			}
			if (joyX > 0){

				float tempRot = circleMenu.transform.eulerAngles.z;
				if (index == buttonAngles.Count-1){
					tempRot = circleMenu.transform.eulerAngles.z - 360;
					Debug.Log("yo-");
				}
				Debug.Log(circleMenu.transform.eulerAngles.z);
				StartCoroutine(Turn(tempRot, (float)buttonAngles[(int)modulo((index+1), buttonAngles.Count)]));
				index++;
				isTurning = true;
			}
		}
	}




	public IEnumerator Turn(float start, float end){

		float temp = 0;
		while (!((int) modulo(circleMenu.transform.eulerAngles.z, 360) <= (int) modulo(end, 360) +1 
			&& (int) modulo(circleMenu.transform.eulerAngles.z, 360) >= (int) modulo(end, 360) -1 )){


			temp += Time.deltaTime * 2;
			circleMenu.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(start, end, temp));
			yield return null;
		}
		circleMenu.transform.eulerAngles = new Vector3(0, 0, end);
		isTurning = false;
	}





	public float modulo(float dividend, float divisor){

		return (((dividend) % divisor) + divisor) % divisor;
	}
	public float modulo(float dividend, int divisor){

		return (((dividend) % divisor) + divisor) % divisor;
	}
	public float modulo(int dividend, float divisor){

		return (((dividend) % divisor) + divisor) % divisor;
	}
	public float modulo(int dividend, int divisor){

		return (((dividend) % divisor) + divisor) % divisor;
	}
}
