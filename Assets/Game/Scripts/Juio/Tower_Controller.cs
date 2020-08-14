using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower_Controller : MonoBehaviour
{
    [Header("Tower")]
    private AudioController audioController;
    private PlayerController2 player;
    private Animator towerAnim;

    [Header("Shoot")]
    public GameObject spot;
    public GameObject bullet;
    public float bulletTime; // Variavel

    [Header("Broke")]
    public bool breaking;
    public float brokeTime; // Variavel
    public int random;
    [Header("Pieces")]
    public int pieces = 0;
    public int maxPieces = 6;  // Variavel
    public int need = 6;

    private void Awake()
    {
        player = FindObjectOfType(typeof(PlayerController2)) as PlayerController2; // new
    }

    void Start()
    {
        towerAnim = GetComponent<Animator>();
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        random = random = Random.Range(20, 25);
    }
    void Update()
    {
        Animation();

        need = maxPieces - pieces;

        if (pieces == maxPieces)
        {
            bulletTime += Time.deltaTime;
            breaking = true;
            if (bulletTime >= 3f)
            {
                var Enemies = GameObject.FindGameObjectsWithTag("EnemyCollider");
                if (Enemies == null)
                {
                    bulletTime = 0;
                    return;
                }
                Instantiate(bullet, new Vector3(spot.transform.position.x, spot.transform.position.y, spot.transform.position.z), spot.transform.rotation).GetComponent<Bullet_Controller>().Enemies = Enemies;
                audioController.playSfx(audioController.sfxTowerShoot[Random.Range(0,audioController.sfxTowerShoot.Length)], 0.5f);
                bulletTime = 0;
            }
        }
        if (breaking == true)
        {
            brokeTime += Time.deltaTime;
            if (brokeTime >= random)
            {
                pieces -= 1;
                brokeTime = 0;
                random = Random.Range(20, 25);
            }
        }
    }   
    void Animation()
    {
        switch (pieces)
        {
            case 0:
                towerAnim.SetInteger("Pieces", pieces);
                break;
            case 1:
                towerAnim.SetInteger("Pieces", pieces);
                break;
            case 2:
                towerAnim.SetInteger("Pieces", pieces);
                break;
            case 3:
                towerAnim.SetInteger("Pieces", pieces);
                break;
            case 4:
                towerAnim.SetInteger("Pieces", pieces);
                break;
            case 5:
                towerAnim.SetInteger("Pieces", pieces);
                break;
            case 6:
                towerAnim.SetInteger("Pieces", pieces);
                break;
        }
        if (pieces == 0)
        {
            towerAnim.SetInteger("Pieces", 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.pieces >= need)
            {
                player.pieces = (player.pieces - need);
                pieces = maxPieces;
                if (need != 0)
                {
                    audioController.playSfx(audioController.sfxFixTower[Random.Range(0, audioController.sfxFixTower.Length)], 0.5f);
                }
            }
            else
            {
                pieces += player.pieces;
                if (need != 0 && player.pieces >= 1)
                {
                    audioController.playSfx(audioController.sfxFixTower[Random.Range(0, audioController.sfxFixTower.Length)], 0.5f);
                }
                player.pieces = 0;
            }
        }
    }
}