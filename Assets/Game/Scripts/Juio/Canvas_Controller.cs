using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_Controller : MonoBehaviour
{
    private LevelLoader levelLoader;
    private AudioController audioController;

    [Header("Menu")]
    public Button buttonPlay;
    public Button buttonCredits;
    public Animator animCredits;
    public bool CreditsMove;
    [Header("Menu - Music")]
    private AudioSource music;
    public Animator musicButton;
    private AudioSource sfx;
    public Animator sfxButton;

    [Header("Game")]
    public Text keysNumber;
    public Text killCountText;
    private PlayerController2 playerController2;

    [Header("Lose")]
    public Text killsText;
    public int kills;
    public Text recordText;
    public Text newRecordText;
    public int lastRecord = 0;
    public int newRecord = 0;
    private void Start()
    {
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        playerController2 = FindObjectOfType(typeof(PlayerController2)) as PlayerController2;
        levelLoader = FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();

        lastRecord = PlayerPrefs.GetInt("Record");
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            int musicAnim = PlayerPrefs.GetInt("MusicAnim");
            if (musicAnim == 1)
            {
                music.mute = true;
                musicButton.SetBool("Muted", true);
            }
            else
            {
                music.mute = false;
                musicButton.SetBool("Muted", false);
            }

            int sfxAnim = PlayerPrefs.GetInt("SfxAnim");
            if (sfxAnim == 1)
            {
                sfx.mute = true;
                sfxButton.SetBool("Muted", true);
            }
            else
            {
                sfx.mute = false;
                sfxButton.SetBool("Muted", false);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            audioController.musicSource.Pause();
            audioController.playSfx(audioController.sfxLose, 0.8f);
            Cursor.visible = true;
            kills = PlayerPrefs.GetInt("Kills");
            newRecord = PlayerPrefs.GetInt("NewRecord");
            if (newRecord == 1)
            {
                killsText.text = "KILLS:" + kills;
                newRecordText.gameObject.SetActive(true);
                recordText.text = "RECORD:" + kills;
            }
            else
            {
                recordText.text = "RECORD:" + lastRecord;
                killsText.text = "KILLS:" + kills;
            }
        }

    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            keysNumber.text = playerController2.pieces.ToString(); // new
            killCountText.text = playerController2.killCount.ToString(); // new          
        }
    }
    public void Play()
    {
        audioController.playSfx(audioController.sfxButton, 0.2f);
        levelLoader.LoadNextScene(1);
        Cursor.visible = false;
    }
    public void Credits()
    {
        CreditsMove = !CreditsMove;
        audioController.playSfx(audioController.sfxButton, 0.2f);
        if (CreditsMove)
        {
            animCredits.SetBool("Active", true);
        }

        buttonPlay.interactable = false;
        buttonCredits.interactable = false;
    }
    public void ReturnCredits()
    {
        CreditsMove = !CreditsMove;
        audioController.playSfx(audioController.sfxButton, 0.2f);
        if (!CreditsMove)
        {
            animCredits.SetBool("Active", false);
        }

        buttonPlay.interactable = true;
        buttonCredits.interactable = true;
    }
    public void Exit()
    {
        audioController.playSfx(audioController.sfxButton, 0.2f);
        Debug.LogError("Exit");
        Application.Quit();
        //Browser
        //Application.OpenURL("about:blank");
    }
    public void BackMenuLose()
    {
        audioController.playSfx(audioController.sfxButton, 0.2f);
        levelLoader.LoadNextScene(-2);
        audioController.musicSource.Play();
    }
    public void PlayAgain()
    {
        audioController.playSfx(audioController.sfxButton, 0.2f);
        levelLoader.LoadNextScene(-1);
        audioController.musicSource.Play();
        Cursor.visible = false;
    }
    public void MusicButton()
    {
        if (music.mute == false)
        {
            music.mute = true;
            musicButton.SetBool("Muted", true);
            PlayerPrefs.SetInt("MusicAnim", 1);
        }
        else
        {
            music.mute = false;
            musicButton.SetBool("Muted", false);
            PlayerPrefs.SetInt("MusicAnim", 0);
        }        
    }
    public void SfxButton()
    {
        if (sfx.mute == false)
        {
            sfx.mute = true;
            sfxButton.SetBool("Muted", true);
            PlayerPrefs.SetInt("SfxAnim", 1);
        }
        else
        {
            sfx.mute = false;
            sfxButton.SetBool("Muted", false);
            PlayerPrefs.SetInt("SfxAnim", 0);
        }
    }
}
