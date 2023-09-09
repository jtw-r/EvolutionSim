using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class NeuronGroup {
  public List<Neuron> neurons;
  public List<InputNeuron> InputNeurons = new();
  public List<OutputNeuron> OutputNeurons = new();
  public List<ThroughNeuron> ThroughNeurons = new();

  public NeuronGroup(List<Neuron> neurons) {
    this.neurons = neurons;
  }
}

public class StartingNeuronTemplate : NeuronGroup {
  public StartingNeuronTemplate(IEnumerable<InputNeuron> inputNeurons, int throughNeuronCount,
    IEnumerable<OutputNeuron> outputNeurons) : base(new List<Neuron>()) {
    neurons = new List<Neuron>();
    foreach (var neuron in inputNeurons) {
      neuron.ID = neurons.Count();
      neurons.Add(neuron);
      InputNeurons.Add(neuron);
    }

    foreach (var neuron in outputNeurons) {
      neuron.ID = neurons.Count();
      neurons.Add(neuron);
      OutputNeurons.Add(neuron);
    }

    for (var i = 0; i < throughNeuronCount; i++) {
      var neuron = new ThroughNeuron("Through Neuron #" + i, inputNeurons.Count() + throughNeuronCount,
        outputNeurons.Count() + throughNeuronCount);
      neuron.ID = neurons.Count();
      neurons.Add(neuron);
      ThroughNeurons.Add(neuron);
    }
  }
}