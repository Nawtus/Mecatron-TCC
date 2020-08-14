using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployObjects : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject respawn;
    public float respawnTime = 5f; // Variavel
    private float time;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 60, Screen.height - 60, Camera.main.transform.position.z));
        RandomSpot();
    }
    private void Update()
    {
        time += Time.deltaTime;

        if (time >= respawnTime)
        {
            SpawnPiece();
            RandomSpot();
            time = 0;
        }
    }
    public void RandomSpot()
    {
        respawn.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
    }
    public void SpawnPiece()
    {
        GameObject piece = Instantiate(objectPrefab) as GameObject;
        piece.transform.position = new Vector2(respawn.transform.position.x,respawn.transform.position.y);
    }
}
