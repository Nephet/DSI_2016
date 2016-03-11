using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour {


	#region Singleton
	static private SelectionManager s_Instance;
	static public SelectionManager instance
	{
		get
		{
			return s_Instance;
		}
	}

	void Awake()
	{
		if (s_Instance == null)
			s_Instance = this;
		DontDestroyOnLoad (this);
	}

	#endregion

	float xAxis;
	float yAxis;

	bool xAxisAltLeft;
	bool xAxisAltRight;

	bool select;
	bool deselect;

	public int readyCount = 0;

	Vector3 _direction;

	public GameObject[] cursors;
	public bool[] selecting;
	public List<GameObject> charactersEnable;
	public GameObject[] characterSelected;
	public GameObject[] characters;
	public bool[] _oldTriggerHeld;
	int[] currentIdMask;
	public GameObject[] listOfMask;
	public GameObject[] chooseMask;
	public bool[] playerReady;
	public bool[] valid;

	public int[] currentTeam;

	public Texture[] bodyTextures;
	public Texture[] maskLeopardTextures;
	public Texture[] maskLlamaTextures;
	public Texture[] maskParesseuxTextures;
	public Texture[] maskToucanTextures;
	public Texture[] pagneTextures;

	public Texture textureTeam1;
	public Texture textureTeam2;

	public Texture textureBallTeam1;
	public Texture textureBallTeam2;

	public List<Texture> listMaskTextureTeam1;
	public List<Texture> listMaskTextureTeam2;

	public Texture[] currentTexture;

	GameObject _currentMask;
	bool _playMusic = false;

    public GameObject[] partSocle = new GameObject[4];
	public GameObject[] arrowSelection = new GameObject[4];

	public SpriteRenderer tagPlayer;
	public Sprite[] tagTeam1;
	public Sprite[] tagTeam2;

    // Use this for initialization
    void Start () {
		selecting = new bool[5];
		_oldTriggerHeld = new bool[5];
		characterSelected = new GameObject[5];
		currentTeam = new int[5]; 
		currentIdMask = new int[5];
		playerReady= new bool[5];
		currentTexture = new Texture[5];
		chooseMask = new GameObject[5];
		valid = new bool[5];

		for (int i = 1; i < 5; i++) {
			selecting [i] = false;
			_oldTriggerHeld [i] = false;
			playerReady [i] = false;
			valid [i] = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!_playMusic) {

			SoundManagerEvent.emit (SoundManagerType.MENU);
			_playMusic = true;
		}
		LaunchGame ();

		for (int i = 1; i < 5; i++)
        {
			xAxis = Input.GetAxis ("L_XAxis_" + i);
			yAxis = Input.GetAxis ("L_YAxis_" + i);
			select = Input.GetButtonDown ("A_Button_"+i);
			deselect = Input.GetButtonDown ("B_Button_"+i);

			if (!selecting [i])
            {
				_direction = new Vector3 (xAxis, yAxis, 0f);
				_direction.Normalize ();
				_direction = Vector3.ClampMagnitude (_direction, 1.0f);
				_direction.z = 0f;


				cursors [i].transform.Translate (_direction * 2f * Time.deltaTime);
			}

            else
            {
				xAxisAltLeft = Input.GetAxis ("L_XAxis_" + i) < 0.0f;
				xAxisAltRight = Input.GetAxis ("L_XAxis_" + i) > 0.0f;

				if ((_oldTriggerHeld[i] != xAxisAltLeft) && xAxisAltLeft) 
				{
					currentIdMask [i]--;
					if (currentIdMask [i] <= 0) 
					{
						currentIdMask [i] = 4;
					}

					Vector3 _tempPos = characterSelected [i].transform.GetChild (4).gameObject.transform.position;//
					Quaternion _tempRot = characterSelected [i].transform.GetChild (4).gameObject.transform.rotation;//
					Destroy (characterSelected [i].transform.GetChild (4).gameObject);//
					_currentMask = Instantiate(characters [currentIdMask [i]].gameObject,_tempPos , _tempRot) as GameObject;
                    int pos = _currentMask.name.IndexOf("("); // find the left parenthesis position...
                    _currentMask.name = _currentMask.name.Substring(0, pos); // and get only the substring before it
                    _currentMask.transform.IsChildOf (characterSelected [i].transform);
					_currentMask.transform.parent = characterSelected [i].transform;
					chooseMask[i] = listOfMask [currentIdMask[i]];
					ChangeTexture (currentIdMask [i], currentTeam[i], i, _currentMask.gameObject);


				}

                else if((_oldTriggerHeld[i] != xAxisAltRight) && xAxisAltRight)
				{
					currentIdMask [i]++;
					if (currentIdMask [i] >= 5) 
					{
						currentIdMask [i] = 1;
					}
					Vector3 _tempPos = characterSelected [i].transform.GetChild (4).gameObject.transform.position;//
					Quaternion _tempRot = characterSelected [i].transform.GetChild (4).gameObject.transform.rotation;//
					Destroy (characterSelected [i].transform.GetChild (4).gameObject);//
					_currentMask = Instantiate(characters [currentIdMask [i]].gameObject,_tempPos , _tempRot) as GameObject;
                    int pos = _currentMask.name.IndexOf("("); // find the left parenthesis position...
                    _currentMask.name = _currentMask.name.Substring(0, pos); // and get only the substring before it
                    _currentMask.transform.IsChildOf (characterSelected [i].transform);
					_currentMask.transform.parent = characterSelected [i].transform;
					chooseMask[i] = listOfMask [currentIdMask[i]];
                    ChangeTexture(currentIdMask [i], currentTeam[i], i, _currentMask.gameObject);
				}
				_oldTriggerHeld[i] = xAxisAltLeft || xAxisAltRight;
			}


			if (select)
            {
				Vector3 origin = Camera.main.transform.position;
				Vector3 _dir = cursors[i].transform.position - origin;
				RaycastHit hit;

				if (!selecting [i])
                {
					if (Physics.Raycast (origin,_dir, out hit))
                    {
						//Debug.DrawLine (origin, hit.point, Color.red, 5.0f);
						selecting [i] = true;
						cursors [i].SetActive (false);
						characterSelected [i] = hit.collider.gameObject;
						characterSelected [i].GetComponent<BoxCollider> ().enabled = false;
						_currentMask = characterSelected [i].transform.GetChild(4).gameObject;//
                        //_currentMask = characterSelected[i];
						hit.collider.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                        CheckTeam(characterSelected [i].transform.gameObject, i);
						AssociateTag (hit.collider.gameObject, currentTeam [i], i);
						charactersEnable.Remove (hit.collider.gameObject);
                        currentIdMask[i] = GetCurrentMaskId (hit.collider.gameObject.transform.GetChild (4).gameObject);//
						chooseMask[i] = listOfMask [currentIdMask[i]];
                        ChangeTexture(currentIdMask [i], currentTeam[i], i, characterSelected [i].transform.GetChild(4).gameObject);//
                        //transform.GetChild(1).gameObject.SetActive(true);
                        
						print(hit.collider.name);
						CheckArrow(hit.collider.gameObject);
                    }
				}

                else
                {
					


					playerReady[i] = SameMask (currentTeam [i], i);
					if(playerReady[i])
					{
						characterSelected [i].transform.GetChild(0).gameObject.SetActive(true);//particles activation
						characterSelected [i].transform.GetChild(3).gameObject.SetActive(false);
						valid [i] = true;
						readyCount++;
					}
				}
			}

			if (deselect && selecting[i])
            {
				if (valid [i])
                {
					readyCount--;
				}

				selecting [i] = false;
				playerReady [i] = false;
				cursors [i].SetActive (true);




                ChangeTexture(currentIdMask [i], 0, i, characterSelected [i].transform.GetChild(4).gameObject);//
                charactersEnable.Add (characterSelected[i]);
				characterSelected [i].GetComponent<BoxCollider> ().enabled = true;

				Vector3 origin = Camera.main.transform.position;
				Vector3 _dir = cursors[i].transform.position - origin;
				RaycastHit hit;

				if (Physics.Raycast (origin, _dir, out hit)) 
				{
					DisableArrow(hit.collider.gameObject);
					hit.collider.gameObject.transform.GetChild (2).GetComponent<SpriteRenderer> ().sprite = null;
					hit.collider.gameObject.transform.GetChild(3).gameObject.SetActive(false);

				}

                characterSelected[i].transform.GetChild(0).gameObject.SetActive(false);//particles deactivation
                characterSelected [i] = null;
                
                chooseMask[i] = null;
				CheckTeam (characterSelected [i], i);
				//ChangeTexture (currentIdMask [i], 0, i);
			}
		}
	}

	int GetCurrentMaskId(GameObject _mask)
	{

		for (int i = 1; i < 5; i++) 
		{
			if (characters [i].gameObject.name == _mask.name) {
				return i;
			}
		}

		return 0;
	}

	void CheckTeam(GameObject _slot, int _id)
	{
		if (_slot == null)
        {
			currentTeam [_id] = 0;
		}

        else
        {
			if (_slot.name.Contains ("1") || _slot.name.Contains ("2"))
            {
				currentTeam [_id] = 1;  
			}

            else if (_slot.name.Contains ("3") || _slot.name.Contains ("4"))
            {
				currentTeam [_id] = 2;
			} 
		}
	}

	bool SameMask (int _myTeamId, int _myId)
	{
		for (int i = 1; i < 5; i++) 
		{
			if (_myTeamId == currentTeam [i] && _myId != i)
            {
				if (chooseMask [i] != chooseMask [_myId])
                {
					return true;

				}

                else
                {
					return false;
				}

			} 

		}
		return true;
	}

	void LaunchGame()
	{

		if (readyCount >= 4) {
			
			for (int i = 1; i < 5; i++) 
			{

				currentTexture[i] = currentTeam[i] == 1 ? listMaskTextureTeam1[currentIdMask [i]-1] : listMaskTextureTeam2[currentIdMask [i]-1];

				//chooseMask[i] = listMaskPlayers[currentIdMask [i]];

			}
			this.enabled = false;
			SceneManager.LoadSceneAsync (2);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {

			for (int i = 1; i < 5; i++) 
			{
				currentTeam[i] = 1;
				chooseMask [i] = listOfMask [3];
				currentTexture[i] = currentTeam[i] == 1 ? listMaskTextureTeam1[3] : listMaskTextureTeam2[3];

				//chooseMask[i] = listMaskPlayers[currentIdMask [i]];

			}

			this.enabled = false;
			SceneManager.LoadSceneAsync (2);
		}


	}

	void ChangeTexture(int _idMask, int _teamId, int _idPlayer, GameObject _player)
	{

        //Debug.Log (_idMask+", /// iciiiiiiiiiiiiiiiiiiiii "+ _teamId+ "/// , "+_idPlayer);
		//Debug.Log (characterSelected [_idPlayer].transform.GetChild (0).gameObject);

        if (_teamId == 0)
        {	
			if (_idMask == 1) {
				//Jaguar
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = bodyTextures[0];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = pagneTextures [0];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskLeopardTextures [0];

			} else if (_idMask == 2) {
                //lama
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[0];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = maskLlamaTextures [0];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = bodyTextures [0];

			} else if (_idMask == 3) {
				//paresseux
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[0];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = bodyTextures[0];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskParesseuxTextures [0];

			} else if (_idMask == 4) {
				//toucan
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[0];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = bodyTextures[0];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskToucanTextures [0];

			}

		}

        else if (_teamId == 1)
        {
			if (_idMask == 1)
            {
				//Jaguar
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = bodyTextures[1];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = pagneTextures [1];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskLeopardTextures [1];


			}

            else if (_idMask == 2)
            {
				//lama
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[1];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = maskLlamaTextures [1];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = bodyTextures [1];

			}

            else if (_idMask == 3)
            {
				//paresseux
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[1];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = bodyTextures[1];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskParesseuxTextures [1];
			}

            else if (_idMask == 4)
            {
				//toucan
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[1];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = bodyTextures[1];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskToucanTextures [1];
			}
		}

        else if (_teamId == 2)
        {
			if (_idMask == 1)
            {
				//Jaguar
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = bodyTextures[2];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = pagneTextures [2];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskLeopardTextures [2];


			} else if (_idMask == 2) {
				//lama
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[2];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = maskLlamaTextures [2];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = bodyTextures [2];

			} else if (_idMask == 3) {
				//paresseux
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[2];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = bodyTextures[2];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskParesseuxTextures [2];
			} else if (_idMask == 4) {
				//toucan
				_player.gameObject.GetComponent<MeshRenderer>().materials[1].mainTexture = pagneTextures[2];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [2].mainTexture = bodyTextures[2];
				_player.gameObject.GetComponent<MeshRenderer> ().materials [3].mainTexture = maskToucanTextures [2];
			}

		}

	}

	void CheckArrow(GameObject _target)
	{
		if (_target.name.Contains ("1")) 
		{
			arrowSelection [0].SetActive (true);
		}

		else if (_target.name.Contains ("2")) 
		{
			arrowSelection [1].SetActive (true);
		}

		else if (_target.name.Contains ("3")) 
		{
			arrowSelection [2].SetActive (true);
		}

		else if (_target.name.Contains ("4")) 
		{
			arrowSelection [3].SetActive (true);
		}
	}

	void DisableArrow(GameObject _target)
	{
		if (_target.name.Contains ("1")) 
		{
			print (_target.name + "<<<disable");
			arrowSelection [0].SetActive (false);
		}

		else if (_target.name.Contains ("2")) 
		{
			arrowSelection [1].SetActive (false);
		}

		else if (_target.name.Contains ("3")) 
		{
			arrowSelection [2].SetActive (false);
		}

		else if (_target.name.Contains ("4")) 
		{
			arrowSelection [3].SetActive (false);
		}
	}

	void AssociateTag(GameObject _slot, int _teamId, int _id)
	{
		Debug.Log (_slot);
		if (_teamId == 1) 
		{
			_slot.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = tagTeam1[_id-1];
		} 

		else if (_teamId == 2) 
		{
			_slot.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = tagTeam2[_id-1];
		}
	}

}
