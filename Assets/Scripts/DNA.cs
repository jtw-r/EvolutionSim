using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class DNA
{
    public List<Genome> genomes;
    public string string_representation;

    public DNA(List<Genome> genomes = null)
    {
        this.genomes = genomes;
        string_representation = "";
        if (genomes != null)
            foreach (var genome in genomes)
                string_representation += genome.GenomeString + " ";
        else
            genomes = new List<Genome>();
    }

    public void AddGenome(Genome genome)
    {
        genomes.Add(genome);
        if (genomes.Count == 1)
            string_representation = genome.GenomeString;
        else
            string_representation += " " + genome.GenomeString;
    }

    /**
     * Randomly initializes the genomes
     */
    public void FullyRandomize(int genome_count, NeuronGroup brain_template)
    {
        genomes = new List<Genome>();
        for (var g = 0; g < genome_count; g++)
        {
            Neuron input = null;
            Neuron output = null;
            // 0 = InputNeuron, 1 = ThroughNeuron
            var randomA = Random.Range(0, 2);
            switch (randomA)
            {
                case 0:
                    var inputNeurons = brain_template.InputNeurons.FindAll(n => n.TotalOutputs < n.MaxOutputs);
                    if (inputNeurons.Count == 0) break;
                    var _input = inputNeurons[Random.Range(0, inputNeurons.Count)];
                    _input.AddConnection();
                    input = _input;
                    break;
                case 1:
                    var throughNeurons = brain_template.ThroughNeurons.FindAll(n => n.TotalOutputs < n.MaxOutputs);
                    if (throughNeurons.Count == 0) break;
                    var _through = throughNeurons[Random.Range(0, throughNeurons.Count)];
                    _through.AddConnection();
                    input = _through;
                    break;
            }

            // 0 = ThroughNeuron, 1 = OutputNeuron
            var randomB = Random.Range(0, 2);
            switch (randomB)
            {
                case 0:
                    var throughNeurons = brain_template.ThroughNeurons.FindAll(n => n.TotalInputs < n.MaxInputs);
                    if (throughNeurons.Count == 0) break;
                    var _through = throughNeurons[Random.Range(0, throughNeurons.Count)];
                    _through.AddConnection();
                    output = _through;
                    break;
                case 1:
                    var outputNeurons = brain_template.OutputNeurons.FindAll(n => n.TotalInputs < n.MaxInputs);
                    if (outputNeurons.Count == 0) break;
                    var _output = outputNeurons[Random.Range(0, outputNeurons.Count)];
                    _output.AddConnection();
                    output = _output;
                    break;
            }

            if (input != null && output != null) AddGenome(new Genome(input, output, Random.Range(0, 4096 - 1)));
        }
    }

    public void Mutate()
    {
    }
}