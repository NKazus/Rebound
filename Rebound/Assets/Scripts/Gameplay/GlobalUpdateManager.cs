using System;
using UnityEngine;

public class GlobalUpdateManager : MonoBehaviour
{
    public static event Action GlobalFixedUpdateEvent;
    public static event Action GlobalUpdateEvent;

    private void FixedUpdate()
    {
        GlobalFixedUpdateEvent?.Invoke();
    }

    private void Update()
    {
        GlobalUpdateEvent?.Invoke();
    }
}
