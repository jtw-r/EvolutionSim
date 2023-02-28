using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NeuronInputCategory
{
    Sensory,
    Constant,
    Oscillation,
}

public enum NeuronFlowType
{
    Out, // Data only passes out of the Neuron
    Through, // Data passes in and out of the neuron
    In, // Data only flows into the neuron
}

[Serializable]
public class Neuron
{
    public int ID;
    public NeuronFlowType Flow;
    public int TotalInputs;
    public int TotalOutputs;
    
    public Neuron( NeuronFlowType flowType, int totalInputs, int totalOutputs)
    {
        this.Flow = flowType;
        this.TotalInputs = totalInputs;
        this.TotalOutputs = totalOutputs;
    }
}

public class InputNeuron : Neuron
{
    private NeuronInputCategory Category;
    public InputNeuron( NeuronInputCategory category, int totalOutputs) : base(NeuronFlowType.Out, 0, totalOutputs)
    {
        this.Category = category;
        this.Flow = NeuronFlowType.Out;
        this.TotalInputs = 0;
        this.TotalOutputs = totalOutputs;
    }
}

public class OutputNeuron : Neuron
{
    
    public OutputNeuron(int totalInputs) : base(NeuronFlowType.In, totalInputs, 0)
    {
        this.Flow = NeuronFlowType.In;
        this.TotalInputs = totalInputs;
        this.TotalOutputs = 0;
    }
}

public class ThroughNeuron : Neuron
{
    
    public ThroughNeuron( int totalInputs, int totalOutputs) : base( NeuronFlowType.Through, totalInputs, totalOutputs)
    {
        this.Flow = NeuronFlowType.Through;
        this.TotalInputs = totalInputs;
        this.TotalOutputs = totalOutputs;
    }
}

