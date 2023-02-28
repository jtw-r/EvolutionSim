using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private PositionManager PositionManager;
    public PopulationManager PopulationManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.PositionManager = new PositionManager();
        this.PopulationManager = new PopulationManager(this.PositionManager);
        this.PopulationManager.CreateInitialPopulation(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
