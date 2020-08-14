using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class DeployEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public DeployObjects objects;

    public GameObject[] enemySpots;
    public float velTime;
    [SerializeField]private float respawnTime; // Variavel
    public float timer; //TimeCount
    public float totalTimer; //TimeCount

    private void Start()
    {
        respawnTime = 3.5f;
        objects = GetComponent<DeployObjects>();
    }
    private void Update()
    {
        Balance();
        velTime += Time.deltaTime;

        if (velTime >= respawnTime)
        {
            SpawnEnemy();
            velTime = 0;
        }
    }
    private void Balance()
    {
        //Balanceamento por TimerCount
        timer += Time.deltaTime;
        totalTimer += Time.deltaTime;

        if (timer >= 30f && respawnTime >= 2f)
        {
            respawnTime -= 0.5f;
            timer = 0;
        }
        else if (timer >= 30f && respawnTime < 2f && respawnTime >= 1.1f)
        {
            respawnTime -= 0.2f;
            timer = 0;
            objects.respawnTime = 4.5f;
        }
    }
    private void SpawnEnemy()
    {
        int aux = Random.Range(0, 4);
        GameObject enemy = Instantiate(enemyPrefab) as GameObject;
        enemy.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;

        switch (aux)
        {
            case 0:
                enemy.transform.position = new Vector3(enemySpots[aux].transform.position.x,enemySpots[aux].transform.position.y);
                break;
            case 1:
                enemy.transform.position = new Vector3(enemySpots[aux].transform.position.x, enemySpots[aux].transform.position.y);
                break;
            case 2:
                enemy.transform.position = new Vector3(enemySpots[aux].transform.position.x, enemySpots[aux].transform.position.y);
                break;
            case 3:
                enemy.transform.position = new Vector3(enemySpots[aux].transform.position.x, enemySpots[aux].transform.position.y);
                break;
        }
    }
}
