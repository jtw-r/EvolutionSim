using System;

[Serializable]
public class Brain {
  public NeuronGroup neurons;
  public DNA dna;

  public Brain(NeuronGroup neurons, DNA dna) {
    this.neurons = neurons;
    this.dna = dna;
  }

  public void Compute(Creature creature) {
    var through_genomes = dna.genomes.FindAll(g => g.InputNeuron.Flow == NeuronFlowType.Through);
    var output_genomes = dna.genomes.FindAll(g => g.OutputNeuron.Flow == NeuronFlowType.In);
    for (var i = 0; i < neurons.InputNeurons.Count; i++) {
      var neuron = neurons.InputNeurons[i];
      neuron.Step(creature);
    }

    for (var i = 0; i < neurons.ThroughNeurons.Count; i++) {
      var neuron = neurons.ThroughNeurons[i];
      neuron.Step(output_genomes);
    }

    for (var i = 0; i < neurons.OutputNeurons.Count; i++) {
      var neuron = neurons.OutputNeurons[i];
      neuron.Step(output_genomes, creature);
    }
  }
}