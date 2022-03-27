using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubesController : MonoBehaviour
{
    public GameObject cubePrefab;
    
    [Range(0, 400)]
    [SerializeField]
    private int cubeCount;
    [SerializeField]
    private int columnLenght;
    private float spawnRange = 2.0f;
    private float startPos = -19f;
    

    void Start()
    {
        SpawnCube(cubeCount);
    }

    private Vector3 GenerateSpawnPosition(int cubeNumber)
    {
        Vector3 cubPos = new Vector3(startPos + (spawnRange * (cubeNumber % columnLenght)),
            0.5f, startPos + (spawnRange * (cubeNumber / columnLenght)));
        
        return cubPos;
    }

    void SpawnCube(int cubeToSpawn)
    {
        for (int i = 0; i < cubeToSpawn; i++)
        {
            GameObject cube = Instantiate(cubePrefab, GenerateSpawnPosition(i), Quaternion.identity);
            cube.transform.parent = transform;
            cube.name = "Cube " + i;
        }
    }
}
