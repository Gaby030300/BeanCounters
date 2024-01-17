using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Properties")]
    [SerializeField] private List<GameObject> hazardList = new();    
    [SerializeField] private Text truckText;
    [SerializeField] private float startSpawnTime;
    [HideInInspector] public float truck;

    [Header("Other Components")]
    [SerializeField] private Stack stack;

    private float startHazardTime;
    private float startSpawnTime2;
    private float startLifeTime;
    private float hazardTime;
    private float lifeTime;
    private float spawnTime;
    private float spawnTime2;
    private int rangetop;

    void Start()
    {
        truck = 1;
        rangetop = 1;

        startSpawnTime2 = Random.Range(1, 7);
        startHazardTime = Random.Range(1, 6);
        startLifeTime = Random.Range(20, 25);

        spawnTime2 = startSpawnTime2;
        hazardTime = startHazardTime;
        lifeTime = startLifeTime;

        stack = FindObjectOfType<Stack>();
    }

    void Update()
    {
        truckText.text = "TRUCK: " + truck;
        VerifyTruck();
        SpawnObjects();
    }

    private void VerifyTruck()
    {
        if (stack.stackHeight >= 20)
        {
            truck += 1f;
            stack.stackHeight = 0;
        }
    }

    private void SpawnObjects()
    {
        SpawnBags(ref spawnTime, startSpawnTime, hazardList[3]);
        SpawnBags(ref spawnTime2, startSpawnTime2, hazardList[3]);

        InstantiateHazard();
        SpawnLifeObject(ref lifeTime, startLifeTime, hazardList[4]);
    }

    void SpawnBags(ref float timer, float startTime, GameObject objectToSpawn)
    {
        if (timer <= 0)
        {
            Instantiate(objectToSpawn, new Vector3(6f, 1f, 0f), Quaternion.identity);
            if (objectToSpawn == hazardList[3])
            {
                startSpawnTime2 = Random.Range(1, 7);
                timer = startSpawnTime2;
            }
            else
            {
                timer = startTime;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void InstantiateHazard()
    {
        if (truck >= 2)
        {
            if (hazardTime <= 0)
            {
                Instantiate(hazardList[Random.Range(0, rangetop)], new Vector3(6f, 1f, 0f), Quaternion.identity);
                startHazardTime = Random.Range(1, 6);
                hazardTime = startHazardTime;
            }
            else
            {
                hazardTime -= Time.deltaTime;
            }

            switch ((int)truck)
            {
                case 2:
                    rangetop = 1;
                    break;
                case 3:
                    rangetop = 2;
                    break;
                case 4:
                    rangetop = 3;
                    break;
            }
        }
    }

    void SpawnLifeObject(ref float timer, float startTime, GameObject objectToSpawn)
    {
        if (timer <= 0)
        {
            Instantiate(objectToSpawn, new Vector3(6f, 1f, 0f), Quaternion.identity);
            startLifeTime = Random.Range(20, 25);
            timer = startLifeTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
