using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Creature
{
    public int ID;
    public Brain brain;
    public PositionalObject positional;

    private List<Try> ToExecute = new();

    public Creature(int ID, Brain brain, PositionalObject positional)
    {
        this.ID = ID;
        this.brain = brain;
        this.positional = positional;
        this.brain.Compute(this);
    }

    public void Move(Vector2 delta)
    {
        ToExecute.Add(new Try(TryType.Move,
            creature => { positional.Translate(new Vector3(delta.x, delta.y, 0)); }));
    }

    public void Step()
    {
        brain.Compute(this);
        foreach (var Try in ToExecute)
        {
            Debug.Log("Running Try Action");
            Try.Action(this);
        }

        ToExecute = new List<Try>();
    }
}