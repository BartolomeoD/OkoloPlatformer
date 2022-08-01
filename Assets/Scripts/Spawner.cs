using System;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPoint;
    public int maxCountOfObjects;
    public GameObject[] objects;
    public int delayBetweenSpawns;

    private Transform _spawnLocation;

    // seconds from UNIX epoch
    private long? _nextSpawn;
    private TimeSpan _delay;
    private int _objectsCount;
    private Random _random;
    private int _objectsInSpawner = 0;

    void Start()
    {
        _spawnLocation = spawnPoint.transform;
        _objectsCount = objects.Length;
        _random = new Random();
    }

    void FixedUpdate()
    {
        var unixTimeSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
        if (_nextSpawn > unixTimeSeconds || _objectsInSpawner >= maxCountOfObjects)
            return;

        _nextSpawn = unixTimeSeconds + delayBetweenSpawns;


        var index = _random.Next(_objectsCount);
        var randomIndex = objects[index];

        var obj = Instantiate(randomIndex, _spawnLocation.position, _spawnLocation.rotation, transform);
        obj.GetComponent<SpriteRenderer>().color = GetRandomColor();
        _objectsInSpawner++;
    }

    private Color GetRandomColor()
    {
        return _random.Next(4) switch
        {
            0 => Color.green,
            1 => Color.magenta,
            2 => Color.red,
            3 => Color.yellow,
        };
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
        _objectsInSpawner--;
    }
}