using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] Transform levelPart;
    private Transform startingPoint;
    private Transform endpoint;
    [SerializeField] private List<GameObject> coins;
    [SerializeField] private bool spawnCoins = true;
    [SerializeField] private List<Transform> heightSpawns;
    private Vector2 lastVec;

    public float startingRangeCoin;
    public float endRangeBetweenCoin;
    public float distanceBetweenCoin;

    private void Awake()
    {
        startingPoint = levelPart.Find("Distance_Left").transform;
        endpoint = levelPart.Find("Distance_Right").transform;
    }

    private void FixedUpdate()
    {
        if (spawnCoins)
        {
            GameObject coin = GetRandomCoin();
            if (lastVec == new Vector2(0, 0))
            {
                lastVec = new Vector2(startingPoint.position.x + startingRangeCoin, GetRandomHeight().position.y);
                Instantiate(coin, lastVec, startingPoint.rotation);
            }
            else
            {
                lastVec = new Vector2(lastVec.x + distanceBetweenCoin, GetRandomHeight().position.y);
                Instantiate(coin, lastVec, startingPoint.rotation);
            }
        }
        if (endpoint.position.x - endRangeBetweenCoin <= lastVec.x)
        {
            spawnCoins = false;
        }
    }

    private GameObject GetRandomCoin()
    {
        return coins[Random.Range(0, coins.Count)];
    }


    private Transform GetRandomHeight()
    {
        return heightSpawns[Random.Range(0, heightSpawns.Count)];
    }
}
