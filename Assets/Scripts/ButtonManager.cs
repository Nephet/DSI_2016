using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour {

    //buttons
    public Button bPlay;
    public Button bSettings;
    public Button bSandbox;
    public Button bCredits;
    public Button bQuit;

    private const float PLAY_BUTTON_ANGLE = 0;
    private const float SETTINGS_BUTTON_ANGLE = 43;
    private const float SANDBOX_BUTTON_ANGLE = 90;
    private const float QUIT_BUTTON_ANGLE = 135;
    private const float CREDITS_BUTTON_ANGLE = 230;
   

    //private float[] buttonAngles = { 0, 43, 90, 135, 230 };


    //main menu elements
    public Image Arrow;
	public GameObject circleMenu;
    private float circleMenuAngle;

    public Button currButt;
    public float leftButt;
    public float rightButt;
    public bool isTurningLeft = false;
    public bool isTurningRight = false;
    public bool isMenuRotating = false;

    //menu backgrounds
    public Image credits;

    public bool isOnMainMenu = true;
    public bool isCreditsDisplayed = false;
    public bool isInSettings = false;
    public bool isInPlayMenu = false;
      
    //inputs
    public float joyX;
    public float joyY;

	void Start()
    {
        //TODO: verifier que l'angle et le currbutt sont synchro
    }



	void Update ()
    {
        //define currbutt and find next buttons left and right
        if (!isMenuRotating)
        {
			circleMenuAngle = circleMenu.transform.rotation.z*(180/Mathf.PI);
			Debug.Log(circleMenuAngle*2);
            if (circleMenuAngle > 300 || circleMenuAngle <= 30)
            {
                currButt = bPlay;
                Debug.Log("currButt = bPlay");
                leftButt = CREDITS_BUTTON_ANGLE;
                rightButt = SANDBOX_BUTTON_ANGLE;
            }
            else if (circleMenuAngle > 30 && circleMenuAngle <= 60)
            {
                currButt = bSandbox;
                Debug.Log("currButt = bSandbox");
                leftButt = PLAY_BUTTON_ANGLE;
                rightButt = SETTINGS_BUTTON_ANGLE;
            }
            else if (circleMenuAngle > 60 && circleMenuAngle <= 100)
            {
                currButt = bSettings;
                Debug.Log("currButt = bSettings");
                leftButt = SANDBOX_BUTTON_ANGLE;
                rightButt = QUIT_BUTTON_ANGLE;
            }
            else if (circleMenuAngle > 100 && circleMenuAngle <= 200)
            {
                currButt = bQuit;
                Debug.Log("currButt = bQuit");
                leftButt = SETTINGS_BUTTON_ANGLE;
                rightButt = CREDITS_BUTTON_ANGLE;
            }
            else
            {
                currButt = bCredits;
                Debug.Log("currButt = bCredits");
                leftButt = QUIT_BUTTON_ANGLE;
                rightButt = PLAY_BUTTON_ANGLE;
            }
        }

        // OLD SELECTION SYSTEM 

        if (isOnMainMenu)
        {
            //je choppe l'angle du joysticku
            joyX = Input.GetAxis("L_XAxis_0");
            joyY = Input.GetAxis("L_YAxis_0");

            /*
            Arrow.transform.eulerAngles = new Vector3(Arrow.transform.eulerAngles.x, Arrow.transform.eulerAngles.y, Mathf.Atan2(joyX, joyY) * Mathf.Rad2Deg);

            if (0 == joyX && 0 == joyY)
                Arrow.enabled = false;
            else
                Arrow.enabled = true;

            if (0 < joyX)
            {
                if (0 < joyY)
                {

                    //Debug.Log("TR");
                    bSettings.GetComponent<Image>().color = Color.red;
                    currButt = bSettings;

                    bPlay.GetComponent<Image>().color = Color.white;
                    bCredits.GetComponent<Image>().color = Color.white;

                }
                if (0 > joyY)
                {

                    //Debug.Log("BR");
                    bCredits.GetComponent<Image>().color = Color.red;
                    currButt = bCredits;
                    bPlay.GetComponent<Image>().color = Color.white;
                    bSettings.GetComponent<Image>().color = Color.white;

                }
            }
            if (0 > joyX)
            {
                if (0 < joyY)
                {

                    //Debug.Log("TL");
                    bPlay.GetComponent<Image>().color = Color.red;
                    currButt = bPlay;

                    bSettings.GetComponent<Image>().color = Color.white;
                    bCredits.GetComponent<Image>().color = Color.white;

                }
                if (0 > joyY)
                {

                    //Debug.Log("BL");

                }
            }
            */
        }
        

        //A Button Action in Menus
        if (Input.GetButtonDown("A_Button_1"))
        {
            //Debug.Log(currButt);


            //A button action on Main Menu
            if (isOnMainMenu)
            {
                switch (currButt.tag)
                {
                    case "Play":
                        
                        Play();
                        break;
                    case "Settings":
                        
                        Settings();
                        break;
                    case "Credits":
                        
                        ToggleCredits();
                        //Credits();

                        break;
                    case "Quit":
                       
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

            //B button action on Credits Menu
            if (isCreditsDisplayed)
            {

            }
        }

        //Circular Menu Rotation

        //Left Input
        if (joyX < 0 && !isMenuRotating)
        {

            Debug.Log("Turing Right!!!!!!!!!!!");
            StartCoroutine(CircleRotation());
            isMenuRotating = true;
            isTurningRight = true;
        }

        //Right Input
        else if (joyX > 0 && !isMenuRotating)
        {
            /*
            isMenuRotating = true;
            isTurningLeft = true;
            */
        }
    }



    public void Play()
    {
        Debug.Log("Playing");
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }

    /*
    public void Credits()
    {
        Debug.Log("Crediting");

        //centrer l'image des credits
                
    }
    */

    public IEnumerator CircleRotation()
    {
        /*
        float startRotation = circleMenuAngle;
        while(circleMenuAngle != startRotation - 90)
        {
            //circleMenu.transform.Rotate(new Vector3(0, 0, -5) * 200 * Time.deltaTime);
            circleMenu.transform.eulerAngles = new Vector3(0,0,Mathf.Lerp(startRotation, startRotation - 90, 3 * Time.deltaTime));
            yield return new WaitForSeconds(0.0000001f);
        }

        isMenuRotating = false;
        */
        if (currButt.CompareTag("Sandbox"))
        {
            
            while (circleMenuAngle > leftButt && circleMenuAngle < SANDBOX_BUTTON_ANGLE)
            {
                circleMenu.transform.Rotate(new Vector3(0, 0, -5) * 200 * Time.deltaTime);
                yield return new WaitForSeconds(0.0000001f);
            }
        }
        else if (currButt.CompareTag("Play"))
        {
            while (circleMenuAngle > leftButt || circleMenuAngle == 0)
            {
                Debug.Log("going to Credits");
                Debug.Log(leftButt);
                circleMenu.transform.Rotate(new Vector3(0, 0, -5) * 200 * Time.deltaTime);
                yield return new WaitForSeconds(0.0000001f);
            }
        }
        else
        {
            while (circleMenuAngle > leftButt)
            {
                Debug.Log("going to Quit");
                circleMenu.transform.Rotate(new Vector3(0, 0, -5) * 200 * Time.deltaTime);
                yield return new WaitForSeconds(0.0000001f);
            }
        }

        Debug.Log("Destination reached "+ currButt);
        isMenuRotating = false;
        isTurningLeft = false;
    }

        
    public IEnumerator SlideInCredits()
    {
        
        while (credits.transform.position.x > 1)
        {
            credits.transform.Translate(Vector3.left * 200 * Time.deltaTime);
            yield return new WaitForSeconds(0.0000001f);
        }
    }

    public IEnumerator SlideOutCredits()
    {

        while (credits.transform.position.x > 1)
        {
            credits.transform.Translate(Vector3.left * 200 * Time.deltaTime);
            yield return new WaitForSeconds(0.0000001f);
        }
    }

    public void ToggleMainMenu()
    {
        isOnMainMenu = !isOnMainMenu;

        if (isOnMainMenu)
        {
            bPlay.GetComponent<Image>().enabled = true;
        }
        else
        {
            Arrow.enabled = false;
            bPlay.GetComponent<Image>().enabled = false;

        }

    }

    public void ToggleCredits()
    {

        ToggleMainMenu();
        //use right coroutine depending on credit screen location
        if (credits.transform.position.x > 1)
        {
            StartCoroutine(SlideInCredits());
        }
        else
        {
            StartCoroutine(SlideOutCredits());
        }
    }
}
