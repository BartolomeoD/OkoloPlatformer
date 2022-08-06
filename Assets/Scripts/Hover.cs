using System;
using Unity.VisualScripting;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public GameObject directionFrom;
    public GameObject directionTo;
    public float power = 1;

    private Collider2D _collider;
    private Vector2 _direction;
    private float _directionValue;
    private Vector2 _initialPoint;

    private void OnDrawGizmos()
    {
        if (directionTo == null || directionFrom == null)
            return;

        var pos1 = (Vector2)directionFrom.transform.position;
        var pos2 = (Vector2)directionTo.transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos1, pos2);
        var perpendicular = Vector2.Perpendicular(pos2 - pos1);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pos1 - (perpendicular / 2), (perpendicular) / 2 + pos1);
    }


    void Start()
    {
        _collider = GetComponent<Collider2D>();
        if (_collider == null)
            throw new ApplicationException("No collider on GameObject");

        if (_collider == null)
            throw new ApplicationException("No collider on GameObject");

        _initialPoint = directionFrom.transform.position;
        var destination = (Vector2)directionTo.transform.position;
        var directionVector = destination - _initialPoint;
        _direction = Vector2.ClampMagnitude(directionVector, 1);
        _directionValue = directionVector.magnitude;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var o = other.GameObject();
        var rigidbodyComponent = o.GetComponent<Rigidbody2D>();
        var objectPosition = o.transform.position;
        var vector = (Vector2)objectPosition - _initialPoint;
        var angle = Vector2.Angle(_direction, vector);
        var value = vector.magnitude * Mathf.Cos(Mathf.Deg2Rad * angle);
        var calculatedForce = _direction * power * (_directionValue - Mathf.Min(value, _directionValue));
        rigidbodyComponent.AddForce(calculatedForce, ForceMode2D.Force);
        Debug.DrawLine(_initialPoint, vector + _initialPoint, Color.cyan);
        Debug.DrawLine(objectPosition, calculatedForce + (Vector2)objectPosition, Color.blue);
    }
}