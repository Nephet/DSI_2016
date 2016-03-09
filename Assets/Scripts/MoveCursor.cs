using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MoveCursor : MonoBehaviour {

	#region Singleton
	static private MoveCursor s_Instance;
	static public MoveCursor instance
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

	GameObject _currentMask;
	// Use this for initialization
	void Start () {
		selecting = new bool[5];
		_oldTriggerHeld = new bool[5];
		characterSelected = new GameObject[5];
		currentTeam = new int[5]; 
		currentIdMask = new int[5];
		playerReady= new bool[5];
		chooseMask = new GameObject[5];

		for (int i = 1; i < 5; i++) {
			selecting [i] = false;
			_oldTriggerHeld [i] = false;
			playerReady [i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

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
				}

			}

			if (deselect && selecting[i]) {
				
				selecting [i] = false;
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
		this.enabled = false;
		SceneManager.LoadSceneAsync (2);

	}

}
