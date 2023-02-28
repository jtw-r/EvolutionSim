using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Brain
{
    public NeuronGroup neurons;
    public DNA dna;

    public Brain(NeuronGroup neurons, DNA dna)
    {
        this.neurons = neurons;
        this.dna = dna;
    }
    
}
