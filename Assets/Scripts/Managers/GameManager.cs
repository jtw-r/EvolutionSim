using System.Collections;
using UnityEngine;

namespace Managers {
  public class GameManager : MonoBehaviour {
    public InputManager InputManager;
    public SimulationManager SimulationManager;

    public void Start() {
      InputManager.RegisterAction(KeyCode.G,
        () => {
          Debug.Log("G was pressed!");
          SimulationManager.WorldManager.PopulationManager.AdvanceGeneration();
        });

      InputManager.RegisterAction(KeyCode.S,
        () => {
          Debug.Log("S was pressed!");

          IEnumerator Advance() {
            for (var i = 0; i < 100; i++) {
              yield return new WaitForSeconds(0.05f);
              SimulationManager.WorldManager.PopulationManager.Step();
            }
          }

          StartCoroutine(Advance());
        });
    }

    public void Update() { }
  }
}