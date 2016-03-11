using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Members
    [Header("Clip")]
	[Space(10)]
    public AudioClip Menu_Music;
	public AudioClip Game_Music;
	public AudioClip Overtime_Music;
	public AudioClip Endgame_Music;
	public AudioClip Crowd_Music;
	public AudioClip Kick_FX;
	public AudioClip Kick2_FX;
	public AudioClip Kick3_FX;
	public AudioClip Goal1_FX;
	public AudioClip Goal2_FX;
	public AudioClip Vanish_FX;
	public AudioClip Transformation_FX;
	public AudioClip Transformation2_FX;
	public AudioClip Dash_FX;
	public AudioClip Chargesmash_FX;
	public AudioClip Stun_FX;
	public AudioClip Ballbounce_FX;
	public AudioClip Areyouready_FX;
	public AudioClip Whistlestart_FX;
	public AudioClip Bassend_FX;
	public AudioClip Whistleend_FX;
	public AudioClip Crowdfever_FX;
	public AudioClip Okbutton_FX;
	public AudioClip Backbutton_FX;
	public AudioClip Movebutton_Fx;
	public AudioClip Boo_Sfx;
	public AudioClip FeverMax_Sfx;
	public AudioClip Bonus_Sfx;





    [Space(10)]
    [Header("Source")]
    public AudioSource MenuSource;
	public AudioSource OvertimeSource;
	public AudioSource CrowdSource;
	public AudioSource KickSource;
	public AudioSource GoalSource;
	public AudioSource VanishSource;
	public AudioSource TransformationSource;
	public AudioSource DashSource;
	public AudioSource ChargeSmashSource;
	public AudioSource StunSource;
	public AudioSource BallBounceSource;
	public AudioSource AreYouReadySource;
	public AudioSource WhistleStartSource;
	public AudioSource BassSource;
	public AudioSource WhistleEndSource;
	public AudioSource CrowdFeverSource;
	public AudioSource ButtonSource;
	public AudioSource BooSource;
	public AudioSource FeverMaxSource;
	public AudioSource BonusSource;


    /*[Space(10)]
    [Header("Mixer")]
    public AudioMixer masterMixer;*/
    
    #endregion

    //NE PAS TOUCHER
    /*private int LastMove1 = 0;
    private int LastMove2 = 1;
    private int LastImpact=1;
    private int LastEjection=0;
    private int LastSlow=1;*/
    private bool Mute = false;

   
    void Start()
    {
        SoundManagerEvent.onEvent += Effect;
    }

    void OnDestroy()
    {
        SoundManagerEvent.onEvent -= Effect;
    }

    void Effect(SoundManagerType emt)
    {
        //Liste des effets
        switch (emt)
        {

			case SoundManagerType.MENU:
				MenuSource.Stop();
				MenuSource.clip = Menu_Music;
				MenuSource.Play();
                break;

			case SoundManagerType.GAMEMUSIC:
				MenuSource.Stop();
				MenuSource.clip = Game_Music;
				MenuSource.Play();
				break;

			case SoundManagerType.OVERTIME:
				MenuSource.Stop();
				MenuSource.clip = Overtime_Music;
				MenuSource.Play();
				break;

			case SoundManagerType.ENDGAME:
				MenuSource.Stop();
				MenuSource.clip = Endgame_Music;
				MenuSource.Play();
				break;

			case SoundManagerType.CROWDMUSIC:
				CrowdSource.Stop();
				CrowdSource.clip = Crowd_Music;
				CrowdSource.Play();
				break;

			case SoundManagerType.KICK:
				KickSource.Stop ();
				int randKick = Random.Range (1, 4); 
				if (randKick == 1) 
				{
					KickSource.clip = Kick_FX;
				}
				else if(randKick == 2)
				{
					KickSource.clip = Kick2_FX;
				}
				else if(randKick == 3)
				{
					KickSource.clip = Kick3_FX;
				}
				KickSource.Play();
				break;

			case SoundManagerType.GOAL:
				GoalSource.Stop ();
				int randGoal = Random.Range (1, 2);
				if (randGoal == 1) 
				{
					GoalSource.clip = Goal1_FX;
				}
				else if(randGoal == 2)
				{
					GoalSource.clip = Goal2_FX;
				}
				GoalSource.Play();
				break;

			case SoundManagerType.VANISH:
				VanishSource.Stop();
				VanishSource.clip = Vanish_FX;
				VanishSource.Play();
				break;

			case SoundManagerType.TRANSFO:
				TransformationSource.Stop();
				int randTransfo = Random.Range (1, 3); 
				if (randTransfo == 1) 
				{
					TransformationSource.clip = Transformation_FX;
				}
				else if(randTransfo == 2)
				{
					TransformationSource.clip = Transformation2_FX;
				}
				TransformationSource.Play();
				break;

			case SoundManagerType.DASH:
				DashSource.Stop();
				DashSource.clip = Dash_FX;
				DashSource.Play();
				break;

			case SoundManagerType.CHARGESMASH:
				ChargeSmashSource.Stop();
				ChargeSmashSource.clip = Chargesmash_FX;
				ChargeSmashSource.Play();
				break;

			case SoundManagerType.STUN:
				StunSource.Stop();
				StunSource.clip = Stun_FX;
				StunSource.Play();
				break;

			case SoundManagerType.BOUNCE:
				BallBounceSource.Stop();
				BallBounceSource.clip = Ballbounce_FX;
				BallBounceSource.Play();
				break;

			case SoundManagerType.WHISTLSTART:
				WhistleStartSource.Stop();
				WhistleStartSource.clip = Whistlestart_FX;
				WhistleStartSource.Play();
				break;

			case SoundManagerType.AREYOUREADY:
				AreYouReadySource.Stop();
				AreYouReadySource.clip = Areyouready_FX;
				AreYouReadySource.Play();
				break;

			case SoundManagerType.WHISTLEEND:
				WhistleEndSource.Stop();
				WhistleEndSource.clip = Whistleend_FX;
				WhistleEndSource.Play();
				break;

			case SoundManagerType.BASSEND:
				BassSource.Stop();
				BassSource.clip = Bassend_FX;
				BassSource.Play();
				break;

			case SoundManagerType.BACK:
				ButtonSource.Stop();
				ButtonSource.clip = Backbutton_FX;
				ButtonSource.Play();
				break;

			case SoundManagerType.OK:
				ButtonSource.Stop();
				ButtonSource.clip = Okbutton_FX;
				ButtonSource.Play();
				break;

			case SoundManagerType.MOVE:
				ButtonSource.Stop();
				ButtonSource.clip = Movebutton_Fx;
				ButtonSource.Play();
				break;

			case SoundManagerType.BOO:
				BooSource.Stop();
				BooSource.clip = Boo_Sfx;
				BooSource.Play();
				break;

			case SoundManagerType.FEVERMAX:
				FeverMaxSource.Stop();
				FeverMaxSource.clip = FeverMax_Sfx;
				FeverMaxSource.Play();
				break;

			case SoundManagerType.BONUS:
				BonusSource.Stop();
				BonusSource.clip = Bonus_Sfx;
				BonusSource.Play();
				break;
        }

    }


    /*public void SetMasterLvl(float level)
    {
        masterMixer.SetFloat("MasterMixer", level);
    }

    public void SetMusicLvl(float level)
    {
        masterMixer.SetFloat("MusicMixer", level);
    }

    public void SetMusicPitch(float level)
    {
        masterMixer.SetFloat("MusicPitch", level);
    }

    public void SetSoundLvl(float level)
    {
        masterMixer.SetFloat("SoundMixer", level);
    }*/

}