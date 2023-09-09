using UnityEngine;

namespace Managers {
  /*
   * The World Manager controls:
   *  - The Creatures Container
   *  - The Position Manager
   *  - The Population Manager
   *  Later:
   *  - The Environment Manager
   */
  public class WorldManager : MonoBehaviour {
    public PopulationManager PopulationManager;
    public PositionManager PositionManager;
    public GameObject template;
    private GameObject creaturesContainerObject;
    private GameObject worldObject;

    public void Start() {
      worldObject = new GameObject("World Object");
      worldObject.transform.parent = gameObject.transform;
      creaturesContainerObject = new GameObject("Creatures");
      creaturesContainerObject.transform.parent = worldObject.transform;
      PositionManager.parent = creaturesContainerObject;
      PositionManager.template = template;
      PopulationManager = new PopulationManager(PositionManager);
      PopulationManager.CreateInitialPopulation();
    }

    // Update is called once per frame
    public void Update() { }
  }
}