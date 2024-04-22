using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Signal", menuName = "Signals")]
public class Signal : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        foreach (var listener in listeners)
        {
            listener.OnSignalRaised();
        }
    }

    public void RegisterListenter(SignalListener listener){
        listeners.Add(listener);   
    }

    public void UnregisterListenter(SignalListener listener){
        listeners.Remove(listener);
    }
}



