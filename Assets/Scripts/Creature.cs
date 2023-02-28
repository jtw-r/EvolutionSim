using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Creature
{

    public int ID;
    public Brain brain;
    public PositionalObject positional;

    public Creature(int ID, Brain brain, PositionalObject positional)
    {
        this.ID = ID;
        this.brain = brain;
        this.positional = positional;
    }

}
