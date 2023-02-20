using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsManager : MonoBehaviour
{
    [SerializeField] float xLeftSpawnpoint;
    [SerializeField] float xRightSpawnpoint;

    [SerializeField] float yMaxSpawnHeight;
    [SerializeField] float yMinSpawnHeight;

    [SerializeField] GameObject[] cloudsPrefabs;

    private void Awake()
    {
        StartCoroutine(GenerateClouds());
        GenerateCloud();
        GenerateCloud();
    }

    IEnumerator GenerateClouds()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(7.5f, 11f));
            GenerateCloud();
        }
    }

    void GenerateCloud()
    {
        Vector2 spawnpoint = GetRandomSpawnpoint();

        byte rn = (byte)Random.Range(0,cloudsPrefabs.Length);
        GameObject cloud = Instantiate(cloudsPrefabs[rn]);
        cloud.transform.position = spawnpoint;

        if (spawnpoint.x == xRightSpawnpoint)
        {
            cloud.GetComponent<Cloud>().movDirection = Vector2.left;
        }
        else
        {
            cloud.GetComponent<Cloud>().movDirection = Vector2.right;
        }
    }

    Vector2 GetRandomSpawnpoint()
    {
        Vector2 spawnPoint;
        byte rn = (byte)Random.Range(0, 2);

        if (rn == 0)
        {
            spawnPoint.x = xLeftSpawnpoint;
        }
        else
        {
            spawnPoint.x = xRightSpawnpoint;
        }

        spawnPoint.y = Random.Range(yMinSpawnHeight, yMaxSpawnHeight);

        return spawnPoint;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(xLeftSpawnpoint, yMinSpawnHeight), new Vector3(xLeftSpawnpoint, yMaxSpawnHeight));
        Gizmos.DrawLine(new Vector3(xRightSpawnpoint, yMinSpawnHeight), new Vector3(xRightSpawnpoint, yMaxSpawnHeight));
    }
}
