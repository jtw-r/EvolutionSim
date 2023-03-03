using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum NeuronInputCategory
{
    Sensory,
    Constant,
    Oscillation
}

public enum NeuronFlowType
{
    Out, // Data only passes out of the Neuron
    Through, // Data passes in and out of the neuron
    In // Data only flows into the neuron
}

[Serializable]
public class Neuron
{
    public NeuronFlowType Flow;
    public int ID;
    public int MaxInputs;
    public int MaxOutputs;
    public string Name;
    public int TotalInputs;
    public int TotalOutputs;
    public double value;

    public Neuron(string Name, NeuronFlowType flowType, int maxInputs, int maxOutputs)
    {
        this.Name = Name;
        Flow = flowType;
        TotalInputs = 0;
        TotalOutputs = 0;
        MaxInputs = maxInputs;
        MaxOutputs = maxOutputs;
    }

    public void AddConnection()
    {
        Debug.Log("Nope");
    }
}

public class InputNeuron : Neuron
{
    private readonly Func<Creature, double> valueFn;
    private NeuronInputCategory Category;

    public InputNeuron(string Name, NeuronInputCategory category, int maxOutputs,
        Func<Creature, double> valueFn) : base(
        Name, NeuronFlowType.Out, 0, maxOutputs)
    {
        Category = category;
        Flow = NeuronFlowType.Out;
        MaxInputs = 0;
        MaxOutputs = maxOutputs;
        this.valueFn = valueFn;
    }

    public void Step(Creature creature)
    {
        value = valueFn(creature);
    }

    public new void AddConnection()
    {
        if (TotalOutputs < MaxOutputs) TotalOutputs++;
    }
}

public class OutputNeuron : Neuron
{
    public Action<Creature, double> updateFunc;

    public OutputNeuron(string Name, int maxInputs, Action<Creature, double> updateFunc) : base(Name,
        NeuronFlowType.In, maxInputs, 0)
    {
        this.updateFunc = updateFunc;
        Flow = NeuronFlowType.In;
        MaxInputs = maxInputs;
        MaxOutputs = 0;
    }

    public void Step(List<Gene> outputGenomes, Creature creature)
    {
        if (outputGenomes.Count == 0) return;
        value = Math.Tanh(outputGenomes.Average(v => v.InputNeuron.value * v.Bias));
        updateFunc(creature, value);
    }

    public new void AddConnection()
    {
        if (TotalInputs < MaxInputs) TotalInputs++;
    }
}

public class ThroughNeuron : Neuron
{
    public ThroughNeuron(string Name, int maxInputs, int maxOutputs) : base(Name, NeuronFlowType.Through, maxInputs,
        maxOutputs)
    {
        Flow = NeuronFlowType.Through;
        MaxInputs = maxInputs;
        MaxOutputs = maxOutputs;
    }

    public void Step(List<Gene> throughGenomes)
    {
        if (throughGenomes.Count == 0) return;
        value = Math.Tanh(throughGenomes.Average(v => v.InputNeuron.value * v.Bias));
    }


    public new void AddConnection()
    {
        if (TotalInputs < MaxInputs) TotalInputs++;

        if (TotalOutputs < MaxOutputs) TotalOutputs++;
    }
}