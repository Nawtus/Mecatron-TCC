using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    private PlayerController2 player; // new

    void Awake()
    {
        player = FindObjectOfType(typeof(PlayerController2)) as PlayerController2; //new
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.CollectPiece();
            Destroy(gameObject);
        }
    }
}
