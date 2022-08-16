using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColor : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return Random.Range(0, 3) switch
        {
            0 => Color.green,
            1 => Color.magenta,
            2 => Color.red,
            3 => Color.yellow,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}