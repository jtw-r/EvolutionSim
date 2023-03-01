using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private readonly List<Tuple<KeyCode, Action>> onKeyDowns = new();

    public void Start()
    {
    }

    public void Update()
    {
        foreach (var onKeyDown in onKeyDowns)
            if (Input.GetKeyDown(onKeyDown.Item1))
                onKeyDown.Item2();
    }

    public void RegisterAction(KeyCode key, Action action)
    {
        onKeyDowns.Add(new Tuple<KeyCode, Action>(key, action));
    }
}