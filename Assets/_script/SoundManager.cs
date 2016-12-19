using UnityEngine;
using System.Collections;
//! pengaturan sound pada game
public class SoundManager : MonoBehaviour{

	public AudioSource bgSound;/*!<background sound*/
	public string soundName;/*!<judul dari lagu background*/
    public AudioSource WinSound;/*!<objek suara (AudioSource) ketika menang battle*/
    public AudioSource LoseSound;/*!<objek suara (AudioSource) ketika kalah battle*/
    public AudioSource otherSound;/*!<objek suara (AudioSource) lain (bila diperlukan)*/

    public AudioClip win;/*!<suara (AudioClip) menang battle*/
    public static SoundManager instance; /*!<pemanggilan script soundManager*/
	public AudioClip lose;/*!<suara (AudioClip) kalah battle*/
    public AudioSource clickSound;/*!<suara klik button*/
	public AudioClip encounter;/*!<suara ketika musuh muncul*/
    public AudioClip goBattle;/*!<suara ketika klik button lawan pada preBattle*/
    public AudioClip flee;/*!<suara ketika klik button kabur pada preBattle*/
    public AudioClip site;/*!<suara ketika pindah situs / masuk portal*/

    public bool isFadein; /*!<cek animasi fadein*/

    // Use this for initialization
    void Start()
	{
		SoundManager.instance = this;
		MusicManager.play(soundName,1,1);
		MusicManager.setVolume(0.6f,0);
	}

    /**
     * mengecilkan volume musik/sound.
     * */
	public void LowerVolume()
	{
		MusicManager.setVolume(0.2f, 1);
		Invoke("NormalizeVolume", 1f);
	}

	void NormalizeVolume()
	{
		MusicManager.setVolume(0.6f, 1);
	}

    /**
 * memainkan/play sound menang
 * */
    public void PlayWinSound()
	{	
		LowerVolume();	
		WinSound.Stop();
		WinSound.Play();
		
	}

    /**
* memainkan/play sound kalah
* */
    public void PlayLoseSound()
	{
		LowerVolume();
		LoseSound.Stop();
		LoseSound.Play();
	}

    /**
* memainkan/play sound klik button
* */
    public void PlayClickSound()
	{		
		clickSound.Stop();
		clickSound.Play();	
	}

    /**
* memainkan/play sound menjawab soal dengan benar
* */
    public void PlayCorrectSound()
	{		
		otherSound.clip = win;
		otherSound.Stop();
		otherSound.Play();
	}

    /**
* memainkan/play sound menjawab soal dengan salah
* */
    public void PlayIncorrectSound()
	{
		otherSound.clip = lose;
		otherSound.Stop();
		otherSound.Play();
	}

    /**
* memainkan/play sound preBattle/bertemu musuh
* */
    public void PlayEncounter()
	{
		LowerVolume();
		otherSound.clip = encounter;
		otherSound.Stop();
		otherSound.Play();
	}

    /**
* memainkan/play sound klik button lawan pada preBattle
* */
    public void PlayGoBattle()
	{
		LowerVolume();
		otherSound.clip = goBattle;
		otherSound.Stop();
		otherSound.Play();
	}

    /**
* memainkan/play sound klik button kabur pada preBattle
* */
    public void PlayFlee()
	{
		LowerVolume();
		otherSound.clip = flee;
		otherSound.Stop();
		otherSound.Play();
	}

    /**
* memainkan/play sound masuk situs/portal
* */
    public void PlayEnterSite()
	{
		LowerVolume();
		otherSound.clip = site;
		otherSound.Stop();
		otherSound.Play();
	}
}
