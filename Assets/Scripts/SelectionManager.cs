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

	public List<GameObject> listMaskPlayers;

    public GameObject PlayerBody;

    public GameObject[] tabPlayers;
	public GameObject[] currentMask;
	public int[] currentTeam;

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
				currentMask [i].name = currentMask [i].name.Replace ("(Clone)", "");
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
		for (int i = 1; i < 5; i++) 
		{
			currentTeam [i] = tabPlayers [i].transform.position.x == -4.5f ? 1 : 2; 
			currentMask [i] = listMaskedPlayers [CheckMask (currentMask [i])];
        }
		this.enabled = false;
		SceneManager.LoadSceneAsync (2);

	}

}
