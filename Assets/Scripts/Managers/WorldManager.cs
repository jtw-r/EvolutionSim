using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private GameObject creaturesContainerObject;
    public PopulationManager PopulationManager;
    public PositionManager PositionManager;
    public GameObject template;
    private GameObject worldObject;


    // Start is called before the first frame update
    public void Start()
    {
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
    public void Update()
    {
    }
}