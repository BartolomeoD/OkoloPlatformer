using UnityEngine;
using Random = System.Random;

public class RandomColor : MonoBehaviour
{
    private Random _random;

    // Start is called before the first frame update
    void Start()
    {
        
        _random = new Random();
        GetComponent<SpriteRenderer>().color = GetRandomColor();
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
}

