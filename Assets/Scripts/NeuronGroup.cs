using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class NeuronGroup
{
    public List<Neuron> neurons;
    public List<InputNeuron> InputNeurons = new List<InputNeuron>();
    public List<ThroughNeuron> ThroughNeurons = new List<ThroughNeuron>();
    public List<OutputNeuron> OutputNeurons = new List<OutputNeuron>();

    public NeuronGroup(List<Neuron> neurons)
    {
        this.neurons = neurons;
    }
}

public class StartingNeuronTemplate : NeuronGroup
{
    public StartingNeuronTemplate(IEnumerable<InputNeuron> inputNeurons, int throughNeuronCount, IEnumerable<OutputNeuron> outputNeurons) : base(new List<Neuron>())
    {
        this.neurons = new List<Neuron>();
        foreach (var neuron in inputNeurons)
        {
            neuron.ID = this.neurons.Count();
            this.neurons.Add(neuron);
            this.InputNeurons.Add(neuron);
        }
        
        foreach (var neuron in outputNeurons)
        {
            neuron.ID = this.neurons.Count();
            this.neurons.Add(neuron);
            this.OutputNeurons.Add(neuron);
        }
        for (int i = 0; i < throughNeuronCount; i++)
        {
            var neuron = new ThroughNeuron("Through Neuron #" + i.ToString(), inputNeurons.Count() + throughNeuronCount,
                outputNeurons.Count() + throughNeuronCount);
            neuron.ID = this.neurons.Count();
            this.neurons.Add(neuron);
            this.ThroughNeurons.Add(neuron);
        }
    }
}