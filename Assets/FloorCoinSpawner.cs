using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    [SerializeField] private Transform endpoint;
    [SerializeField] private List<GameObject> coins;
    [SerializeField] private bool spawnCoins = true;
    [SerializeField] private List<Transform> heightSpawns;
    private Vector2 lastVec;

    private void FixedUpdate()
    {
        if (spawnCoins)
        {
            GameObject coin = GetRandomCoin();
            if (lastVec == new Vector2(0, 0))
            {
                lastVec = new Vector2(startingPoint.position.x + 75, GetRandomHeight().position.y);
                Instantiate(coin, lastVec, startingPoint.rotation);
            }
            else
            {
                lastVec = new Vector2(lastVec.x + Random.Range(45, 45), GetRandomHeight().position.y);
                Instantiate(coin, lastVec, startingPoint.rotation);
            }
        }
        if (endpoint.position.x - 100 <= lastVec.x)
        {
            spawnCoins = false;
        }
    }

    private GameObject GetRandomCoin()
    {
        return coins[Random.Range(0, coins.Count)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(collision.gameObject);
        }
    }

    private Transform GetRandomHeight()
    {
        return heightSpawns[Random.Range(0, heightSpawns.Count)];
    }
}
