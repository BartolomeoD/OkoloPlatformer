using System;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public LiftScript liftScript;

    // Start is called before the first frame update
    void Start()
    {
        if (liftScript == null)
            throw new ApplicationException("Lift script not set");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            liftScript.PlayerEnterLift();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            liftScript.PlayerExitLift();
    }
}