using System;
using System.Threading.Tasks;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public Vector2 Destination;

    public GameObject Platform;

    public float Speed;

    public float TimeToWaitInDestination = 2;


    private LiftStates CurrentState = LiftStates.WaitPassenger;

    private Vector2 InitialPosition;


    // Start is called before the first frame update
    void Start()
    {
        if (Platform == null)
            throw new ApplicationException("Platform not set");
        InitialPosition = Platform.transform.position;
    }

    private void OnDrawGizmos()
    {
        if (Platform == null)
            return;

        var position = (Vector2)Platform.transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position, Destination);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentState is LiftStates.WaitPassenger or LiftStates.WaitingInDestination)
            return;

        var currentDestination = CurrentState switch
        {
            LiftStates.OnWayToDestination => Destination,
            LiftStates.OnWayBack => InitialPosition,
            _ => throw new ApplicationException("Unknown lift state")
        };

        var transformPosition =
            Vector2.MoveTowards(Platform.transform.position, currentDestination, Speed * Time.deltaTime);
        Platform.transform.position = transformPosition;


        if ((Vector2)Platform.transform.position != currentDestination)
            return;

        if (CurrentState == LiftStates.OnWayToDestination)
        {
            CurrentState = LiftStates.WaitingInDestination;
            Task.Run(async () =>
            {
                await Task.Delay((int)(TimeToWaitInDestination * 1000));
                CurrentState = LiftStates.OnWayBack;
            });
        }
        else if (CurrentState == LiftStates.OnWayBack)
        {
            CurrentState = LiftStates.WaitPassenger;
        }
    }

    public void PlayerEnterLift()
    {
        CurrentState = LiftStates.OnWayToDestination;
    }

    public void PlayerExitLift()
    {
        if (CurrentState != LiftStates.WaitingInDestination)
            CurrentState = LiftStates.OnWayBack;
    }
}

public enum LiftStates
{
    WaitPassenger,
    OnWayToDestination,
    WaitingInDestination,
    OnWayBack,
}