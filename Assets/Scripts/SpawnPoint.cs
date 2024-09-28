// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn; // prefab to spawn
    [SerializeField] private float _repeatInterval; // repeat interval for spawning
    [SerializeField] private int spawnLimit; // spawn limit per spawn
    private int amountOfSpawns = 0; // spawn counter

    // Start is called before the first frame update
    void Start()
    {
        if (_repeatInterval > 0) { // spawn prefabs
            InvokeRepeating("SpawnObject", 0.0f, _repeatInterval);

        }
    }

    public GameObject SpawnObject() {
        if (_prefabToSpawn != null && amountOfSpawns != spawnLimit) { // if there is a prefab to spawn and spawn limit has not been hit
            amountOfSpawns++; 
            return Instantiate(_prefabToSpawn, transform.position, Quaternion.identity); // keep spawning stuff

        }

        return null;
    }
}
