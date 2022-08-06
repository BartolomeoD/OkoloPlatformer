using System;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class Bomb : MonoBehaviour
{
    public float timerInSec;
    public float power;
    public float explosionRadius;

    private DateTime _exposesAt;

    private Boolean _exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        _exposesAt = DateTime.Now.AddSeconds(timerInSec);
    }

    private void OnDrawGizmos()
    {
        GizmosUtils.DrawCircle(transform.position, explosionRadius, Color.red);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_exploded)
            return;

        var now = DateTime.Now;
        if (_exposesAt > now)
            return;

        var colliders = new Collider2D[10];

        var bombPosition = transform.position;
        var size = Physics2D.OverlapCircleNonAlloc(bombPosition, explosionRadius, colliders);
        for (var i = 0; i < size; i++)
        {
            var o = colliders[i].GameObject();
            var rigid = o.GetComponent<Rigidbody2D>();
            if (rigid != null)
            {
                var objPosition = o.transform.position;
                var direction = Vector2.ClampMagnitude(objPosition - bombPosition, 1);
                var distance = (objPosition - bombPosition).magnitude;
                var muliplier = (explosionRadius - Mathf.Min(distance, explosionRadius)) / explosionRadius; 
                rigid.AddForce(direction * (power * muliplier), ForceMode2D.Impulse);
            }
        }

        _exploded = true;
        Destroy(gameObject);
        

        Debug.Log("Expoloded");
    }
}