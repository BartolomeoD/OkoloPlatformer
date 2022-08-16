using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPoint;
    public int maxCountOfObjects;
    public GameObject[] objects;
    public int delayBetweenSpawns;

    private Transform _spawnLocation;

    private float? _nextSpawn;
    private TimeSpan _delay;
    private int _indexOfLastItem;
    private int _objectsInSpawner;

    void Start()
    {
        _spawnLocation = spawnPoint.transform;
        _indexOfLastItem = objects.Length - 1;
    }

    void FixedUpdate()
    {
        var unixTimeSeconds = Time.time;
        if (_nextSpawn > unixTimeSeconds || _objectsInSpawner >= maxCountOfObjects)
            return;

        _nextSpawn = unixTimeSeconds + delayBetweenSpawns;


        var index = Random.Range(0, _indexOfLastItem);
        var randomIndex = objects[index];

        Instantiate(randomIndex, _spawnLocation.position, _spawnLocation.rotation, transform);
        _objectsInSpawner++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
        _objectsInSpawner--;
    }
}