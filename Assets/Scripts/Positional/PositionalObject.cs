using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Something that moves!
/// </summary>
[Serializable]
public class PositionalObject : MonoBehaviour {
  public PositionManager Manager;
  public Vector3 pos;
  public int TimesMoved;
  private bool setupComplete;
  private bool frozen;

  public void Start() {
    setupComplete = false;
    frozen = false;
    TimesMoved = 0;
  }

  public void Update() { }

  private bool isReady() {

    if (Manager == null) {
      return false;
    }

    if (setupComplete == false) {
      return false;
    }
    
    return true;
  }

  public bool CanBeginMove() {
    if (isReady() == false) {
      return false;
    }

    if (IsFrozen()) {
      return false;
    }

    return true;
  }

  public void Create(float x, float y, PositionManager manager) {
    Manager = manager;
    var r = (float) Math.Round(Random.value, 1);
    var g = (float) Math.Round(Random.value, 1);
    var b = (float) Math.Round(Random.value, 1);

    gameObject.GetComponent<SpriteRenderer>().color =
      new Color(r, g, b);

    transform.position.Set(x, y, 0f);
    pos = new Vector3(x, y, 0);
    setupComplete = true;
  }

  public void Freeze() {
    frozen = true;
  }
  
  public void Unfreeze() {
    frozen = false;
  }
  
  /// <summary>
  /// Toggle the positionals frozen status
  /// </summary>
  /// <returns>The new boolean value for if the positional is Frozen</returns>
  public bool ToggleFreeze() {
    frozen = !frozen;
    return frozen;
  }

  public bool IsFrozen() {
    return frozen;
  }

  /// <summary>
  /// Move the positional object relative to it's current position
  /// </summary>
  /// <param name="pos"></param>
  public void Translate(Vector3 pos) {
    Manager.MovePiece(this, transform.position + pos);
  }

  public void MoveTo(Vector3 pos) {
    Manager.MovePiece(this, pos);
  }

  public void SetPosition(Vector3 pos) {
    TimesMoved++;
    transform.position = pos;
  }

  public void Destroy() {
    Manager.Positionals.Remove(this);
    Destroy(gameObject);
  }
}