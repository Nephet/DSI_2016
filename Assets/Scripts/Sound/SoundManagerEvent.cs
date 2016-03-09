using UnityEngine;
using System.Collections;

/*
 * Comment émettre un event:

        //A mettre à un endroit pour lire un son
		SoundManagerEvent.emit(SoundManagerType.DRINK);
 * 
 * Comment traiter un event (dans un start ou un initialisation)
		EventManagerScript.onEvent += (EventManagerType emt, GameObject go) => {
			if (emt == EventManagerType.ENEMY_HIT)
			{
				//SpawnFXAt(go.transform.position);
			}
		};
 * ou:
		void maCallback(EventManagerType emt, GameObject go) => {
			if (emt == EventManagerType.ENEMY_HIT)
			{
				//SpawnFXAt(go.transform.position);
			}
		};
		EventManagerScript.onEvent += maCallback;
 * 
 * qui permet de:
		EventManagerScript.onEvent -= maCallback; //Retire l'appel
 */

//Liste des effets
public enum SoundManagerType
{
    MENU,
	GAMEMUSIC,
	OVERTIME,
	ENDGAME,
	CROWDMUSIC,
	KICK,
	GOAL,
	VANISH,
	TRANSFO,
	DASH,
	CHARGEEXP,
	CHARGESMASH,
	STUN,
	BOUNCE,
	WHISTLSTART,
	AREYOUREADY,
	WHISTLEEND,
	BASSEND,
	BACK,
	OK,
	MOVE,

}

public class SoundManagerEvent : MonoBehaviour
{

    public delegate void EventAction(SoundManagerType emt);
    public static event EventAction onEvent;

    #region Singleton
    static private SoundManagerEvent s_Instance;
    static public SoundManagerEvent instance
    {
        get
        {
            return s_Instance;
        }
    }
    #endregion


    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
        SoundManagerEvent.onEvent += (SoundManagerType emt) => { };
    }

    public static void emit(SoundManagerType emt)
    {

        if (onEvent != null)
        {
            onEvent(emt);
        }
    }



}