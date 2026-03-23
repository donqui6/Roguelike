using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    Vector3 playerLastPosition;



    [Header("Optimisation")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDistance; //doit etre plus grand que la longueur et largeur d'un chunk
    float opDistance;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    void Start()
    {
        playerLastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();

    }
    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        Vector3 moveDirection = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDirection);

        if(!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(directionName).position);
        }


    }
    string GetDirectionName(Vector3 direction)
    {
        direction.Normalize();

        bool goingRight = direction.x > 0;
        bool goingUp = direction.y > 0;
        bool isHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);

        if (isHorizontal)
        {
            if (direction.y > 0.5f) return goingRight ? "Right Up" : "Left Up";
            if (direction.y < -0.5f) return goingRight ? "Right Down" : "Left Down";
            return goingRight ? "Right" : "Left";
        }
        else
        {
            if (direction.x > 0.5f) return goingUp ? "Right Up" : "Right Down";
            if (direction.x < -0.5f) return goingUp ? "Left Up" : "Left Down";
            return goingUp ? "Up" : "Down";
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;    
        if(optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        } else 
        {             
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
            {
                opDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
                if (opDistance > maxOpDistance)
                {
                    chunk.SetActive(false);

                }
                else
                {
                    chunk.SetActive(true);
                }
            }
    }

}
