using System;
using System.Collections;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public GameObject destination;

    public GameObject platform;

    public float speed = 1;

    public float timeToWaitInDestination;


    private LiftStates _currentState = LiftStates.WaitPassenger;

    private Vector2 _initialPosition;
    private Vector2 _destination;
    private int _itemsInLiftCount;
    

    // Start is called before the first frame update
    void Start()
    {
        if (platform == null)
            throw new ApplicationException("Platform not set");
        
        if (platform == null)
            throw new ApplicationException("Destination not set");
        
        _initialPosition = platform.transform.position;
        _destination = destination.transform.position;
    }

    private void OnDrawGizmos()
    {
        if (platform == null)
            return;

        var pos1 = (Vector2)platform.transform.position;
        var pos2 = (Vector2)destination.transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos1, pos2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_currentState is LiftStates.WaitPassenger or LiftStates.WaitingInDestination)
            return;

        var currentDestination = _currentState switch
        {
            LiftStates.OnWayToDestination => _destination,
            LiftStates.OnWayBack => _initialPosition,
            _ => throw new ApplicationException("Unknown lift state")
        };

        var transformPosition =
            Vector2.MoveTowards(platform.transform.position, currentDestination, speed * Time.deltaTime);
        platform.transform.position = transformPosition;

        if ((Vector2)platform.transform.position != currentDestination)
            return;

        if (_currentState == LiftStates.OnWayToDestination)
        {
            _currentState = LiftStates.WaitingInDestination;

            StartCoroutine(TimerOnWayBackStatusChange(Time.time + timeToWaitInDestination));
        }
        else if (_currentState == LiftStates.OnWayBack)
        {
            _currentState = LiftStates.WaitPassenger;
        }
    }

    private IEnumerator TimerOnWayBackStatusChange(float changeAfter)
    {
        while (Time.time < changeAfter)
            yield return null;

        if (_currentState == LiftStates.WaitingInDestination)
            _currentState = LiftStates.OnWayBack;
    }

    public void PlayerEnterLift()
    {
        _itemsInLiftCount++;
        if (_itemsInLiftCount > 0 && _currentState != LiftStates.WaitingInDestination)
            _currentState = LiftStates.OnWayToDestination;
    }

    public void PlayerExitLift()
    {
        _itemsInLiftCount--;
        if (_itemsInLiftCount < 1 && _currentState != LiftStates.WaitingInDestination)
            _currentState = LiftStates.OnWayBack;
    }
}

public enum LiftStates
{
    WaitPassenger,
    OnWayToDestination,
    WaitingInDestination,
    OnWayBack,
}