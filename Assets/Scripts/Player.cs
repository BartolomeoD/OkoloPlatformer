using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalPower;
    public float jumpPower;
    public bool allowedDoubleJump;
    public float inAirPowerMultiplier;
    public LayerMask constructs;


    private Rigidbody2D _rigidBody;
    private int _jumpCount;
    private float _distanceFromCenterToBottomCollider;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        var col = GetComponent<CapsuleCollider2D>();
        _distanceFromCenterToBottomCollider = col.size.y / 2 + col.offset.y;
    }

    void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var result = new RaycastHit2D[1];
        var count = Physics2D.RaycastNonAlloc(transform.position, Vector2.down, result,
            _distanceFromCenterToBottomCollider + 0.01f, constructs);

        var inAir = count == 0;

        if (count > 0)
            _jumpCount = 0;
        
        Vector2 position = transform.position;
        Debug.DrawLine(position, position + Vector2.down * (_distanceFromCenterToBottomCollider + 0.1f),
            inAir ? Color.blue : Color.red);


        switch (horizontalInput)
        {
            case > 0:
                _rigidBody.AddForce(Vector2.right * (horizontalPower * inAirPowerMultiplier), ForceMode2D.Force);
                break;
            case < 0:
                _rigidBody.AddForce(Vector2.left * (horizontalPower * inAirPowerMultiplier), ForceMode2D.Force);
                break;
        }


        if (verticalInput > 0 && (_jumpCount == 0 || _jumpCount == 1 && allowedDoubleJump))
        {
            Debug.Log(verticalInput);
            Debug.Log("jump");
            _rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            _jumpCount++;
        }
    }
}