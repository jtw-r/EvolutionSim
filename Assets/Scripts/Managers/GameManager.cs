using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public InputManager InputManager;
        public SimulationManager SimulationManager;

        public void Start()
        {
            InputManager.RegisterAction(KeyCode.G,
                () =>
                {
                    Debug.Log("G was pressed!");
                    SimulationManager.WorldManager.PopulationManager.AdvanceGeneration(SimulationManager.WorldManager
                        .DesiredPopulation);
                });

            InputManager.RegisterAction(KeyCode.S,
                () =>
                {
                    SimulationManager.WorldManager.PopulationManager.Step();
                    Debug.Log("S was pressed!");
                });
        }

        public void Update()
        {
        }
    }
}