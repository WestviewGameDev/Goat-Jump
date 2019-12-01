using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    //spawn rate is a float between that represents the percent change of an o
    [SerializeField]
    float spawnRate = 100f;
    //chance that the new object spawned is a power up
    [SerializeField]
    float powerupChance = 0.5f;
    [SerializeField]
    GameObject upperBarrier;

    GameObject[] powerups;

    GameObject[] obstacles;

    Queue<GameObject> objQueue;
    Queue<float> objSpawnTimeQueue;

    public float maxSpawnRate = 3;
    public float initialSpawnRate = 0.75f;
    public float growthRate = 0.20f;
    public float p0 = 0.01f;

    private float elapsedTime;
    void Start()
    {
        spawnRate = calculateSpawnRate();
        objQueue = new Queue<GameObject>();
        objSpawnTimeQueue = new Queue<float>();
        elapsedTime = Time.time;

        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obj in powerups)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in obstacles)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnRate = calculateSpawnRate();
        while (objSpawnTimeQueue.Count > 0 && Time.time > objSpawnTimeQueue.Peek())
        {
            objSpawnTimeQueue.Dequeue();
            placeObject(objQueue.Dequeue());
        }
        if (Time.time - elapsedTime >= 1)
        {
            for (int i = 1; i <= spawnRate; i++)
            {
                spawnFallingObject();
            }

            float lastObjectChance = spawnRate - (int)spawnRate;
            if (Random.value <= lastObjectChance)
            {
                spawnFallingObject();
            }
            elapsedTime = Time.time;
            int objectsSpawned = objQueue.Count - objSpawnTimeQueue.Count;
            float spawnInterval = 1f / objectsSpawned;
            for (int i = 1; i <= objectsSpawned; i++)
            {
                objSpawnTimeQueue.Enqueue(i * spawnInterval + Time.time);
            }
        }

        //Debug.Log(calculateSpawnRate());
    }

    private float calculateSpawnRate()
    {
        float m = maxSpawnRate - initialSpawnRate;
        float a = (m / p0) - 1;
        float x = GameManager.instance.score;
        float newSpawnRate = (m / (1 + a * Mathf.Pow((float)System.Math.E, -1f * growthRate * x))) + initialSpawnRate;
        return newSpawnRate;
    }
    private void spawnFallingObject()
    {
        float randNum = Random.value;
        if (randNum <= powerupChance)
        {
            GameObject powerup = powerups[(int)Random.Range(0, powerups.Length)];
            objQueue.Enqueue(powerup);
        }
        else
        {
            GameObject obstacle = obstacles[(int)Random.Range(0, obstacles.Length)];
            objQueue.Enqueue(obstacle);
        }
    }
    private void placeObject(GameObject obj)
    {
        Bounds bounds = upperBarrier.GetComponent<BoxCollider2D>().bounds;
        float left = bounds.center.x - bounds.extents.x;
        float right = bounds.center.x + bounds.extents.x;
        float top = bounds.center.y + bounds.extents.y;
        float bottom = bounds.center.y - bounds.extents.y;
        float xPos = Random.Range(left, right);
        float yPos = bottom;
        GameObject newObj = Instantiate(obj);
        newObj.SetActive(true);
        newObj.transform.position = new Vector3(xPos, yPos, 0);
        Rigidbody2D rigidbody = newObj.GetComponent<Rigidbody2D>();
        rigidbody.angularVelocity = Random.Range(-50, 50f);
    }


}   
