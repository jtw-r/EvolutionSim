using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public PositionManager PositionManager;
    public PopulationManager PopulationManager;
    public GameObject template;
    public int DesiredPopulation = 50;
    private GameObject creaturesContainerObject;
    private GameObject worldObject;


    // Start is called before the first frame update
    private void Start()
    {
        worldObject = new GameObject("World Object");
        worldObject.transform.parent = gameObject.transform;
        creaturesContainerObject = new GameObject("Creatures");
        creaturesContainerObject.transform.parent = worldObject.transform;
        PositionManager = new PositionManager(creaturesContainerObject, template);
        PopulationManager = new PopulationManager(PositionManager);
        PopulationManager.CreateInitialPopulation(DesiredPopulation);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}