using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour {


	public GameObject circleMenu;

	public int index = 0;

	public List<float> buttonAngles = new List<float>();
	public List<GameObject> buttonList = new List<GameObject>();

	//inputs
	public float joyX;
	public float joyY;

	bool axisXRight;
	bool axisXLeft;
	bool _oldTriggerHeldRight;
	bool _oldTriggerHeldLeft;

	public bool isTurning;

    //menu backgrounds
	public Image credits;
	public Image quitConf;

    public bool isOnMainMenu = true;
	public bool isTrans = false;
      
	void Start()
    {
        //TODO: verifier que l'angle et le currbutt sont synchro
		circleMenu.transform.eulerAngles = new Vector3(353, 90, 269);
    }

	void Update ()
    {

		axisXLeft = Input.GetAxis ("L_XAxis_0") < 0.0f;
		axisXRight = Input.GetAxis ("L_XAxis_0") > 0.0f;
        
		joyX = Input.GetAxis("L_XAxis_0");
		joyY = Input.GetAxis("L_YAxis_0");

		//(snapAlt && (_oldTriggerHeldRight != snapAlt))
        if (isOnMainMenu)
        {

			index = (int)modulo((index), buttonAngles.Count);

			if (Input.GetKeyDown(KeyCode.A)){
				circleMenu.transform.eulerAngles = new Vector3(353, 90, -200);
			}

			if (!isTurning){
				if (axisXLeft && (_oldTriggerHeldLeft != axisXLeft)){
					float tempRot = circleMenu.transform.eulerAngles.z;
					//if (buttonAngles [index] > buttonAngles [(int)modulo ((index - 1), buttonAngles.Count)] /*index == 0*/) {
						//tempRot =buttonAngles [(int)modulo ((index - 1), buttonAngles.Count)] + ((360 - circleMenu.transform.eulerAngles.z)+buttonAngles [(int)modulo ((index - 1), buttonAngles.Count)]);
						//Debug.Log (tempRot +" "+ (float)buttonAngles[(int)modulo((index-1), buttonAngles.Count)] );
						//StartCoroutine (Turn ((float)buttonAngles [(int)modulo ((index - 1), buttonAngles.Count)], tempRot));
						//tempRot = /*circleMenu.transform.eulerAngles.z + */(circleMenu.transform.eulerAngles.z - buttonAngles[(int)modulo((index+1), buttonAngles.Count)]);

					//} else {
						//Debug.Log (tempRot +" "+ (float)buttonAngles[(int)modulo((index-1), buttonAngles.Count)] );
						StartCoroutine(Turn(tempRot, (float)buttonAngles[(int)modulo((index-1), buttonAngles.Count)]));
					//}

					index--;
					isTurning = true;
				}
				if (axisXRight && (_oldTriggerHeldRight != axisXRight)){

					float tempRot = circleMenu.transform.eulerAngles.z;
					if (buttonAngles[index] < buttonAngles[(int)modulo((index+1), buttonAngles.Count)]/*index == buttonAngles.Count-1*/){
						tempRot = -1f*(360 - buttonAngles [(int)modulo ((index + 1), buttonAngles.Count)]);
						//tempRot = 360 - circleMenu.transform.eulerAngles.z;
						StartCoroutine(Turn(circleMenu.transform.eulerAngles.z, tempRot));
					}else{
						StartCoroutine(Turn(tempRot, (float)buttonAngles[(int)modulo((index+1), buttonAngles.Count)]));

					}
					
					index++;
					isTurning = true;
				}
			}
			_oldTriggerHeldLeft = axisXLeft;
			_oldTriggerHeldRight = axisXRight;
        }
        

        //A Button Action in Menus
        if (Input.GetButtonDown("A_Button_1"))
        {

			Debug.Log (index);
            //A button action on Main Menu
			if (isOnMainMenu && ! isTurning && !isTrans){
                switch (index)
                {
                    case 0:
                        
                       //Play
					SceneManager.LoadSceneAsync("TeamSelection");
                        break;
                    case 1:
                        
                        //Sandbox
                        break;
                    case 2:
						//Credits
                       
						ToggleCredits();
                       break;
                    case 3:
                       //Quit
						ToggleQuit();
                        break;
					case 4:
						//Settings
						//ToggleCredits();
						break;
                    default:
                        break;

                }
				isOnMainMenu = false;
				isTrans = true;
            }

			if (!isOnMainMenu  && !isTrans){
				switch (index)
				{
				case 0:

					//Play
					break;
				case 1:

					//Sandbox
					break;
				case 2:

					//Settings
					break;
				case 3:
					//Quit
					Application.Quit();
					break;
				case 4:
					//Credits
					break;
				default:
					break;

				}

			}

            //A button action on Credits Menu

            //A button action on Play Menu

            //A button action on Settings Menu

            //A button action on Sandbox Menu

            //A button Action on Quit menu


        }

        //B Button Action in Menus
        if (Input.GetButtonDown("B_Button_1"))
        {
			if (!isOnMainMenu && !isTrans){
				switch (index){
					case 0:

						//Play
						break;
					case 1:

						//Sandbox
						break;
					case 2:

						//Credits
						ToggleCredits();
						break;
					case 3:
						//Quit
						ToggleQuit();
						break;
					case 4:
						
						//Settings
						break;
					default:
						break;
							
				}
				isOnMainMenu = true;
				isTrans = true;
			}           
        }

       
    }



	public IEnumerator Turn(float start, float end){

		float temp = 0;
		while (!((int) modulo(circleMenu.transform.eulerAngles.z, 360) <= (int) modulo(end, 360) +1 && (int) modulo(circleMenu.transform.eulerAngles.z, 360) >= (int) modulo(end, 360) -1 )){


			temp += Time.deltaTime * 250 / (modulo(Mathf.Abs(end-start), 360));
			Debug.Log (start+","+ end);
			Debug.Log (new Quaternion(353, 90, end, transform.rotation.w));
			//circleMenu.transform.localRotation = Quaternion.RotateTowards (transform.localRotation, new Quaternion(transform.localRotation.x, 90, end, transform.localRotation.w), temp);
			circleMenu.transform.eulerAngles = new Vector3(353, 90, Mathf.Lerp(start, end, temp));
			yield return null;
		}
		circleMenu.transform.eulerAngles = new Vector3(353, 90, end);
		isTurning = false;
	}


        
    public IEnumerator SlideInCredits()
    {
		float temp = 0;
		while (credits.transform.localPosition.x > 1)
		{
			temp += Time.deltaTime *1;
			credits.transform.localPosition = new Vector3(Mathf.Lerp(1200, 0, temp), 0, 0);
			yield return null;
        }
		isTrans = false;
    }

	public IEnumerator SlideOutCredits()
    {
		float temp = 0;
		while (credits.transform.localPosition.x <1200)
		{
			temp += Time.deltaTime *1;
			credits.transform.localPosition = new Vector3(Mathf.Lerp(0, 1200, temp), 0, 0);
			yield return null;
		}
		isTrans = false;
    }

	public IEnumerator SlideInQuit()
	{
		float temp = 0;
		while (quitConf.transform.localScale.x < 0.95)
		{
			temp += Time.deltaTime * 10;
			quitConf.transform.localScale = new Vector3(Mathf.Lerp(0, 1, temp), Mathf.Lerp(0, 1, temp), 1);
			yield return null;
		}
		isTrans = false;
	}

	public IEnumerator SlideOutQuit()
	{
		Debug.Log("cya?");
		float temp = 0;
		while (quitConf.transform.localScale.x > 0.05)
		{
			temp += Time.deltaTime * 10;
			quitConf.transform.localScale = new Vector3(Mathf.Lerp(1, 0, temp), Mathf.Lerp(1, 0, temp), 1);
			yield return null;
		}
		isTrans = false;
	}

    public void ToggleMainMenu()
    {
        isOnMainMenu = !isOnMainMenu;

        /*if (isOnMainMenu)
        {
            bPlay.GetComponent<Image>().enabled = true;
        }
        else
        {
            bPlay.GetComponent<Image>().enabled = false;

        }*/

    }

    public void ToggleCredits(){

        ToggleMainMenu();
        //use right coroutine depending on credit screen location
        if (!isOnMainMenu){
            StartCoroutine(SlideInCredits());
        }
        else{
            StartCoroutine(SlideOutCredits());
        }

    }

	public void ToggleQuit(){

		ToggleMainMenu();
		//use right coroutine depending on credit screen location
		if (!isOnMainMenu){
			StartCoroutine(SlideInQuit());
		}
		else{
			StartCoroutine(SlideOutQuit());
		}
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
