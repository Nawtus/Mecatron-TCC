using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    private DeployObjects deployObjects;
    void Awake()
    {
        deployObjects = FindObjectOfType(typeof(DeployObjects)) as DeployObjects;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Tower")
        {
            deployObjects.RandomSpot();
        }
    }
}
