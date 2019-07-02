using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Observer Pattern
public class SignalListener : MonoBehaviour
{
    public SignalEvent signalEvent;
    public UnityEvent unityEvent;

    public void OnSignalRaised()
    {
        unityEvent.Invoke();
    }

    private void OnEnable()
    {
        signalEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        signalEvent.DeRegisterListener(this);
    }
}
