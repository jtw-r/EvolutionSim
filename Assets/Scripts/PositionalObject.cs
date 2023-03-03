using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PositionalObject : MonoBehaviour
{
    public PositionManager Manager;
    public Vector3 pos;
    public int TimesMoved;

    public void Start()
    {
    }

    public void Update()
    {
    }

    public void Create(float x, float y, PositionManager manager)
    {
        Manager = manager;
        var r = (float) Math.Round(Random.value, 1);
        var g = (float) Math.Round(Random.value, 1);
        var b = (float) Math.Round(Random.value, 1);

        gameObject.GetComponent<SpriteRenderer>().color =
            new Color(r, g, b);

        transform.position.Set(x, y, 0f);
        pos = new Vector3(x, y, 0);
    }

    public void Translate(Vector3 pos)
    {
        Manager.MovePiece(this, transform.position + pos);
    }

    public void MoveTo(Vector3 pos)
    {
        Manager.MovePiece(this, pos);
    }

    public void SetPosition(Vector3 pos)
    {
        TimesMoved++;
        transform.position = pos;
    }

    public void Destroy()
    {
        Manager.Positionals.Remove(this);
        Destroy(gameObject);
    }
}