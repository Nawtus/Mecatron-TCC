using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Animator animEnemy;
    private AudioController audioController;   

    private void Start()
    {
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var play = col.gameObject.GetComponent<PlayerController2>();
            play.Died();
            Cursor.visible = true;
        }
    }
    public void Died()
    {
        animEnemy.SetBool("Died", true);
        audioController.playSfx(audioController.sfxEnemyDied[Random.Range(0,audioController.sfxEnemyDied.Length)], 0.4f);
    }
}
