using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PositionManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject parent;
    public List<PositionalObject> Positionals = new();
    public int size;
    public GameObject template;
    private Dictionary<Vector2, PositionalObject> positions = new();


    public void Reset()
    {
        positions = new Dictionary<Vector2, PositionalObject>();
        Positionals = new List<PositionalObject>();
    }

    public void Start()
    {
        camera.transform.position = new Vector3(size / 2f, size / 2f, -10);
        camera.GetComponent<Camera>().orthographicSize = size / 2f + 2f;
    }

    private Vector2? genetateRandomEmptyPosition(int depth = 0)
    {
        if (depth > 10) return null;
        var position = new Vector2(Random.Range(0, size), Random.Range(0, size));
        if (positions.ContainsKey(position)) return genetateRandomEmptyPosition(depth += 1);

        return position;
    }

    [CanBeNull]
    public PositionalObject AddPositional()
    {
        var pos = genetateRandomEmptyPosition();
        if (pos.HasValue == false) return null;
        var vec3pos = new Vector3(pos.Value.x, pos.Value.y, 0);
        var obj = Instantiate(template, vec3pos, Quaternion.identity, parent.transform);
        obj.name = "Positional # " + Positionals.Count;
        obj.AddComponent<PositionalObject>();

        var positional = obj.GetComponent<PositionalObject>();
        positional.Create(pos.Value.x, pos.Value.y, this);
        Positionals.Add(positional);
        positions.Add(pos.Value, positional);
        return positions[pos.Value];
    }

    public bool MovePiece(PositionalObject obj, Vector2 req_pos)
    {
        if (positions.ContainsKey(req_pos)) return false;
        if (req_pos.x < 0 || req_pos.y < 0) return false;
        if (req_pos.x > size || req_pos.y > size) return false;
        positions.Remove(new Vector2(obj.pos.x, obj.pos.y));
        positions.Add(req_pos, obj);
        obj.SetPosition(new Vector3(req_pos.x, req_pos.y));
        return true;
    }

    public void RemovePiece(PositionalObject positional)
    {
        positions.Remove(positional.pos);
        Positionals.RemoveAt(Positionals.FindIndex(p => p.gameObject.name == positional.gameObject.name));
    }
}