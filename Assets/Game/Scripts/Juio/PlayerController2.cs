using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]private AudioController audioController;
    public LevelLoader levelLoader;
    public Rigidbody2D body;

    [Header("Animator")]
    [SerializeField]private Animator animPlayer;
    public bool moving;

    [Header("Inventory")]
    public int pieces = 0;
    public int killCount = 0;
    public int lastRecord = 0;

    [Header("Movement")]

    public float horizontal;
    public float vertical;
    public float moveLimiter = 0.7f;
    private bool isLookLeft;
    public float runSpeed;
    public bool freeze;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        lastRecord = PlayerPrefs.GetInt("Record");
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0 && freeze == false)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void Update()
    {
        Animacao();
        if (freeze == true)
        {
            runSpeed = 0;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0 )
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (horizontal > 0 && isLookLeft == true && freeze == false)
        {
            Flip();
        }
        else if (horizontal < 0 && isLookLeft == false && freeze == false)
        {
            Flip();
        }
    }
    private void Animacao()
    {
        animPlayer.SetBool("Run", moving);
    }

    public void DiedScene()
    {
        levelLoader.LoadNextScene(1);
    }

    public void Died()
    {
        PlayerPrefs.SetInt("Kills", killCount);
        if (killCount > lastRecord)
        {
            PlayerPrefs.SetInt("Record", killCount);
            PlayerPrefs.SetInt("NewRecord", 1);
        }
        else
        {
            PlayerPrefs.SetInt("NewRecord", 0);
        }
        animPlayer.SetBool("Died", true);
        audioController.playSfx(audioController.sfxPlayerDied, 0.5f);
        pieces = 0;
        freeze = true;
    }
    public void IncreaseKillCount()
    {
        killCount += 1;
    }
    public void CollectPiece()
    {
        pieces += 1;
        audioController.playSfx(audioController.sfxPick[Random.Range(0, audioController.sfxPick.Length)], 0.8f);
    }
    void Flip()
    {
        isLookLeft = !isLookLeft;
        GetComponent<SpriteRenderer>().flipX = isLookLeft;
    }
}
