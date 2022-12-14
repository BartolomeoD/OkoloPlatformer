using UnityEngine;

public class RotatingScript : MonoBehaviour
{
    public float rotateDegInSecs = 360;

    private Rigidbody2D _spinner;
    
    // Start is called before the first frame update
    void Start()
    {
        _spinner = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _spinner.angularVelocity = -rotateDegInSecs;
    }
}
