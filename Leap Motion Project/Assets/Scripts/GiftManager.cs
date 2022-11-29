using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour
{
    public GameObject[] gifts;

    public float xRange;
    public float zRange;

    public float yHeight;

    public float xRotationRange;
    public float zRotationRange;

    private float spawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        StartCoroutine(SpawnGift());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnGift()
    {
        int giftIndex = Random.Range(0, gifts.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), yHeight, Random.Range(0.1f, zRange));
        Quaternion spawnRotation = Quaternion.Euler(Random.Range(-xRotationRange, xRotationRange), 0, Random.Range(-zRotationRange, zRotationRange));
        Debug.Log("Position: " + spawnPosition + " Rotation: " + spawnRotation);
        Instantiate(gifts[giftIndex], spawnPosition, spawnRotation);

        yield return new WaitForSeconds(spawnTime);

        if (spawnTime > 0.2f)
            spawnTime -= 0.1f;
        
        StartCoroutine(SpawnGift());
    }
}
