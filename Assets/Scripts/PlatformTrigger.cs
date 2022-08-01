using System;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public LiftScript LiftScript;
    
    // Start is called before the first frame update
    void Start()
    {
        if (LiftScript == null)
            throw new ApplicationException("Lift script not set");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            LiftScript.PlayerEnterLift();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            LiftScript.PlayerExitLift();
    }
}
