using System;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent SignalEvent;

    public void OnSignalRaised()
    {
        SignalEvent.Invoke();
    }

    public void OnEnable()
    {
       signal.RegisterListenter(this);
    }

    public void OnDisable()
    {
       signal.UnregisterListenter(this);
        
    }

}