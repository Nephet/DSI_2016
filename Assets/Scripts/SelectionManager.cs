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

	/*public List<GameObject> listMaskPlayers;
	public List<GameObject> listMaskedPlayers;

    public Texture textureTeam1;
    public Texture textureTeam2;

    public Texture textureBallTeam1;
    public Texture textureBallTeam2;

    public List<Texture> listMaskTextureTeam1;
    public List<Texture> listMaskTextureTeam2;

    public GameObject[] tabPlayers;
	public GameObject[] currentMask;
	public int[] currentTeam;
    public Texture[] currentTexture;

	bool _switchTeamLeft;
	bool _switchTeamRight;
	bool _switchMaskLeft;
	bool _switchMaskRight;
	bool _ready;

	bool[] _oldTriggerHeldTeam;
	bool[] _oldTriggerHeldMask;

	void Start()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Player");
		tabPlayers = new GameObject[5];
		currentMask = new GameObject[5];
		currentTeam = new int[5]; 
		_oldTriggerHeldTeam = new bool[5];
		_oldTriggerHeldMask = new bool[5];

		foreach (GameObject player in temp) {
			int id = int.Parse(player.name[player.name.Length-1]+"");
			tabPlayers [id] = player;
			currentMask[id] = Instantiate (listMaskPlayers [id - 1]) as GameObject;
            
			currentMask [id].name = currentMask [id].name.Replace ("(Clone)", "");
			currentMask[id].transform.parent = player.transform;
			currentMask[id].transform.localPosition = Vector3.zero;
		}
	}

	void Update()
	{
		for (int i = 1; i < 5; i++) 
		{
			_switchTeamLeft = Input.GetAxis ("L_XAxis_" + i) < 0.0f;
			_switchTeamRight = Input.GetAxis ("L_XAxis_" + i) > 0.0f;

			_switchMaskLeft = Input.GetAxis ("R_XAxis_" + i) < 0.0f;
			_switchMaskRight = Input.GetAxis ("R_XAxis_" + i) > 0.0f;

			if (_oldTriggerHeldTeam [i] != _switchTeamLeft && _switchTeamLeft)
			{
				tabPlayers [i].transform.position = new Vector3 (Mathf.Clamp (tabPlayers [i].transform.position.x - 9f, -4.5f, 4.5f), tabPlayers [i].transform.position.y, tabPlayers [i].transform.position.z);
			} 
			else if (_oldTriggerHeldTeam [i] != _switchTeamRight && _switchTeamRight) 
			{
				tabPlayers [i].transform.position = new Vector3 (Mathf.Clamp (tabPlayers [i].transform.position.x + 9f, -4.5f, 4.5f), tabPlayers [i].transform.position.y, tabPlayers [i].transform.position.z);
			}

			if (_oldTriggerHeldMask [i] != _switchMaskLeft && _switchMaskLeft)
			{
				int _idMask = (CheckMask (currentMask[i]));
				Debug.Log (_idMask);
				if (_idMask <= 0) 
				{
					_idMask = 4;
				}
				Destroy (currentMask [i]);
				currentMask[i] = Instantiate (listMaskPlayers [(_idMask-1)]) as GameObject;
				currentMask [i].name = currentMask [i].name.Replace ("(Clone)", "");
				currentMask[i].transform.parent = tabPlayers [i].transform;
				currentMask[i].transform.localPosition = Vector3.zero;
			} 
			else if (_oldTriggerHeldMask [i] != _switchMaskRight && _switchMaskRight) 
			{
				int _idMask = (CheckMask (currentMask[i])+2);
				if (_idMask >= 5) 
				{
					_idMask = 1;
				}
				Destroy (currentMask [i]);
				currentMask[i] = Instantiate (listMaskPlayers [(_idMask-1)]) as GameObject;
                currentMask[i].name = currentMask[i].name.Replace("(Clone)", "");
                currentMask[i].transform.parent = tabPlayers [i].transform;
				currentMask[i].transform.localPosition = Vector3.zero;
			}

			_oldTriggerHeldTeam[i] = _switchTeamLeft || _switchTeamRight;
			_oldTriggerHeldMask [i] = _switchMaskLeft || _switchMaskRight;
				
		}

		_ready = Input.GetButtonDown ("A_Button_1");
		if (_ready && ValidTeam ()) 
		{
			PrepareGame ();
		}
	}

	int CheckMask(GameObject _mask)
	{
		for (int i = 0; i < 4; i++) 
		{
			if (listMaskPlayers [i].name == _mask.name) 
			{
				return i;
			}

		}
		return 0;
	}

	bool ValidTeam()
	{
		int nbTeam1 = 0;
		int nbTeam2 = 0;

        List<GameObject> team1 = new List<GameObject>();
        List<GameObject> team2 = new List<GameObject>();

        for (int i = 1; i < 5; i++) 
		{
			if (tabPlayers [i].transform.position.x == -4.5f) 
			{
				nbTeam1++;
                team1.Add(tabPlayers[i]);
			}
			else if(tabPlayers [i].transform.position.x == 4.5f)
			{
				nbTeam2++;
                team2.Add(tabPlayers[i]);
            }
		}

        bool doubleMask1 = nbTeam1 == 2 && team1[0].transform.GetChild(0).name == team1[1].transform.GetChild(0).name;
        bool doubleMask2 = nbTeam2 == 2 && team2[0].transform.GetChild(0).name == team2[1].transform.GetChild(0).name;

        return nbTeam1 == nbTeam2 && nbTeam1 ==2 && !doubleMask1 && !doubleMask2;

	}

	void PrepareGame()
	{
        currentTexture = new Texture[5];

        for (int i = 1; i < 5; i++) 
		{
			currentTeam [i] = tabPlayers [i].transform.position.x == -4.5f ? 1 : 2;

            currentTexture[i] = currentTeam[i] == 1 ? listMaskTextureTeam1[CheckMask(currentMask[i])] : listMaskTextureTeam2[CheckMask(currentMask[i])];

            currentMask[i] = listMaskPlayers[CheckMask(currentMask[i])];

        }
		this.enabled = false;
		SceneManager.LoadSceneAsync (2);

	}*/

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

	public int[] currentTeam;


	public Texture textureTeam1;
	public Texture textureTeam2;

	public Texture textureBallTeam1;
	public Texture textureBallTeam2;

	public List<Texture> listMaskTextureTeam1;
	public List<Texture> listMaskTextureTeam2;

	public Texture[] currentTexture;

	GameObject _currentMask;
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

		for (int i = 1; i < 5; i++) {
			selecting [i] = false;
			_oldTriggerHeld [i] = false;
			playerReady [i] = false;
		}
	}

	// Update is called once per frame
	void Update () {
		LaunchGame ();
		for (int i = 1; i < 5; i++) {

			xAxis = Input.GetAxis ("L_XAxis_" + i);
			yAxis = Input.GetAxis ("L_YAxis_" + i);
			select = Input.GetButtonDown ("A_Button_"+i);
			deselect = Input.GetButtonDown ("B_Button_"+i);

			if (!selecting [i]) {

				_direction = new Vector3 (xAxis, yAxis, 0f);
				_direction.Normalize ();
				_direction = Vector3.ClampMagnitude (_direction, 1.0f);
				_direction.z = 0f;

				Debug.Log (cursors [i]);
				cursors [i].transform.Translate (_direction * 500f * Time.deltaTime);

			} else {
				xAxisAltLeft = Input.GetAxis ("R_XAxis_" + i) < 0.0f;
				xAxisAltRight = Input.GetAxis ("R_XAxis_" + i) > 0.0f;

				if ((_oldTriggerHeld[i] != xAxisAltLeft) && xAxisAltLeft) 
				{
					currentIdMask [i]--;
					if (currentIdMask [i] <= 0) 
					{
						currentIdMask [i] = 4;
					}
					Vector3 _tempPos = characterSelected [i].transform.GetChild (0).gameObject.transform.position;
					Quaternion _tempRot = characterSelected [i].transform.GetChild (0).gameObject.transform.rotation;
					Destroy (characterSelected [i].transform.GetChild (0).gameObject);
					_currentMask = Instantiate(characters [currentIdMask [i]].gameObject,_tempPos , _tempRot) as GameObject;
					_currentMask.transform.IsChildOf (characterSelected [i].transform);
					_currentMask.transform.parent = characterSelected [i].transform;
					chooseMask[i] = listOfMask [currentIdMask[i]];


				}else if((_oldTriggerHeld[i] != xAxisAltRight) && xAxisAltRight)
				{
					currentIdMask [i]++;
					if (currentIdMask [i] >= 5) 
					{
						currentIdMask [i] = 1;
					}
					Vector3 _tempPos = characterSelected [i].transform.GetChild (0).gameObject.transform.position;
					Quaternion _tempRot = characterSelected [i].transform.GetChild (0).gameObject.transform.rotation;
					Destroy (characterSelected [i].transform.GetChild (0).gameObject);
					_currentMask = Instantiate(characters [currentIdMask [i]].gameObject,_tempPos , _tempRot) as GameObject;
					_currentMask.transform.IsChildOf (characterSelected [i].transform);
					_currentMask.transform.parent = characterSelected [i].transform;
					chooseMask[i] = listOfMask [currentIdMask[i]];

				}
				_oldTriggerHeld[i] = xAxisAltLeft || xAxisAltRight;

			}


			if (select) {
				Ray ray = Camera.main.ScreenPointToRay (cursors[i].transform.position);
				RaycastHit hit;
				if (!selecting [i]) {
					if (Physics.Raycast (ray, out hit)) {
						selecting [i] = true;
						cursors [i].SetActive (false);
						Debug.DrawLine (ray.origin, hit.point, Color.red, 5.0f);
						characterSelected [i] = hit.collider.gameObject;

						Debug.Log (characterSelected [i]);
						_currentMask = characterSelected [i].transform.GetChild (0).gameObject;
						CheckTeam (characterSelected [i].transform.gameObject, i);
						charactersEnable.Remove (hit.collider.gameObject);
						currentIdMask [i] = GetCurrentMaskId (hit.collider.gameObject.transform.GetChild (0).gameObject);

					}
				} else {
					playerReady [i] = true;
					readyCount++;
				}

			}

			if (deselect && selecting[i]) {

				selecting [i] = false;
				readyCount--;
				playerReady [i] = false;
				cursors [i].SetActive (true);
				charactersEnable.Add (characterSelected[i]);
				characterSelected [i] = null;
				CheckTeam (characterSelected [i], i);

			}

		}

	}

	int GetCurrentMaskId(GameObject _mask)
	{
		for (int i = 1; i < 5; i++) 
		{
			if (characters [i].gameObject.name == _mask.name) {
				//Debug.Log (i);
				return i;

			}

		}
		return 0;
	}

	void CheckTeam(GameObject _slot, int _id)
	{
		if (_slot == null) {
			currentTeam [_id] = 0;
		} else {
			if (_slot.name.Contains ("1") || _slot.name.Contains ("2")) {

				currentTeam [_id] = 1;
			} else if (_slot.name.Contains ("3") || _slot.name.Contains ("4")) {

				currentTeam [_id] = 2;
			} 
		}
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


	}


}
