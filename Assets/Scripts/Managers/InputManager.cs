using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
  private readonly List<Tuple<KeyCode, Action>> onKeyDowns = new();

  public void Start() {
    Debug.Log("Input Manager Started");
  }

  public void Update() {
    foreach (var onKeyDown in onKeyDowns)
      if (Input.GetKeyDown(onKeyDown.Item1))
        onKeyDown.Item2();
  }

  public void OnDestroy() {
    onKeyDowns.Clear();
    Debug.Log("Input Manager Stopped");
  }

  public void RegisterAction(KeyCode key, Action action) {
    onKeyDowns.Add(new Tuple<KeyCode, Action>(key, action));
  }
}