using System.Collections.Generic;
using UnityEngine;

public class PositionManager
{
    public GameObject template;
    private List<PositionalObject> Positionals = new List<PositionalObject>();

    public PositionalObject AddPositional()
    {
        PositionalObject obj = new PositionalObject(template);
        this.Positionals.Add(obj);
        return obj;
    }

    
}