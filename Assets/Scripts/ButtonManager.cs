using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour {


	public Image circleMenu;

	public int index = 0;

	public List<float> buttonAngles = new List<float>();
	public List<Button> buttonList = new List<Button>();

	//inputs
	public float joyX;
	public float joyY;

	public bool isTurning;

    //menu backgrounds
	public Image credits;
	public Image quitConf;

    public bool isOnMainMenu = true;
	public bool isTrans = false;
      
	void Start()
    {
        //TODO: verifier que l'angle et le currbutt sont synchro
    }

	void Update ()
    {
        
		joyX = Input.GetAxis("L_XAxis_0");
		joyY = Input.GetAxis("L_YAxis_0");

        if (isOnMainMenu)
        {

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
        

        //A Button Action in Menus
        if (Input.GetButtonDown("A_Button_1"))
        {


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
                        
                       //Settings
                       break;
                    case 3:
                       //Quit
						ToggleQuit();
                        break;
					case 4:
						//Credits
						ToggleCredits();
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

						//Settings
						
						break;
					case 3:
						//Quit
						ToggleQuit();
						break;
					case 4:
						//Credits
						ToggleCredits();
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
		while (!((int) modulo(circleMenu.transform.eulerAngles.z, 360) <= (int) modulo(end, 360) +1 
			&& (int) modulo(circleMenu.transform.eulerAngles.z, 360) >= (int) modulo(end, 360) -1 )){


			temp += Time.deltaTime * 250 / (modulo(Mathf.Abs(end-start), 360));
			circleMenu.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(start, end, temp));
			yield return null;
		}
		circleMenu.transform.eulerAngles = new Vector3(0, 0, end);
		isTurning = false;
	}


        
    public IEnumerator SlideInCredits()
    {
		float temp = 0;
		while (credits.transform.localPosition.x > 1)
		{
			temp += Time.deltaTime *1;
			credits.transform.localPosition = new Vector3(Mathf.Lerp(600, 0, temp), 0, 0);
			yield return null;
        }
		isTrans = false;
    }

	public IEnumerator SlideOutCredits()
    {
		float temp = 0;
		while (credits.transform.localPosition.x <555)
		{
			temp += Time.deltaTime *1;
			credits.transform.localPosition = new Vector3(Mathf.Lerp(0, 600, temp), 0, 0);
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
