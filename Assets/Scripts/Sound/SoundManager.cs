using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Members
    [Header("Clip")]
	[Space(10)]
    public AudioClip Drink;


    [Space(10)]
    [Header("Source")]
    public AudioSource DrinkSource;

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

            case SoundManagerType.DRINK:
                DrinkSource.Stop();
                DrinkSource.clip = Drink;
                DrinkSource.Play();
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