using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class PositionManager
{
    public List<PositionalObject> Positionals = new();
    public int size = 10;
    private GameObject parent;
    private Dictionary<Vector2, PositionalObject> positions;
    private GameObject template;

    public PositionManager(GameObject parent, GameObject template)
    {
        this.parent = parent;
        this.template = template;
        positions = new Dictionary<Vector2, PositionalObject>();
    }

    private Vector2 genetateRandomEmptyPosition()
    {
        var position = new Vector2(Random.Range(0, size - 1), Random.Range(0, size - 1));
        if (positions.ContainsKey(position)) return genetateRandomEmptyPosition();

        return position;
    }

    public PositionalObject AddPositional()
    {
        Debug.Log("Adding Positional");
        var obj = Object.Instantiate(template, parent.transform).gameObject;
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        var positional = parent.AddComponent<PositionalObject>();
        positional.Create(obj);
        var pos = genetateRandomEmptyPosition();
        var vec3pos = new Vector3(pos.x, pos.y, 0);
        positional.MoveTo(vec3pos);
        Positionals.Add(positional);
        positions.Add(pos, positional);
        return positions[pos];
    }
}