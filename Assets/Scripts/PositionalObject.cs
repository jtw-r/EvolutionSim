using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PositionalObject : MonoBehaviour
{
    public GameObject myGameobject;

    public Vector3 position = new(0, 0, 0);

    public void Start()
    {
    }

    private void Update()
    {
        myGameobject.transform.position = position;
    }

    public void Create(GameObject _gameObject)
    {
        myGameobject = _gameObject;
        var r = (float) Math.Round(Random.value, 1);
        var g = (float) Math.Round(Random.value, 1);
        var b = (float) Math.Round(Random.value, 1);

        myGameobject.GetComponent<SpriteRenderer>().color =
            new Color(r, g, b);

        position = myGameobject.transform.position;
    }

    public void Translate(Vector3 pos)
    {
        position += pos;
    }

    public void MoveTo(Vector3 pos)
    {
        position = pos;
    }
}