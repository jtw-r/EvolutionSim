using System;
using Random = UnityEngine.Random;

[Serializable]
public class Gene
{
    public int Bias;
    public int GenomeDec;
    public string GenomeString;
    public Neuron InputNeuron;
    public Neuron OutputNeuron;

    public Gene(Neuron inputNeuron, Neuron outputNeuron, int bias)
    {
        var input_id_hex = inputNeuron.ID.ToString("x");
        var padded_input = input_id_hex;
        for (var i = 0; i < 3 - input_id_hex.Length; i++) padded_input = "0" + padded_input;

        var bias_hex = bias.ToString("X");
        var padded_bias = bias_hex;
        for (var i = 0; i < 3 - bias_hex.Length; i++) padded_bias = "0" + padded_bias;

        var output_id_hex = outputNeuron.ID.ToString("X");
        var padded_output_hex = output_id_hex;
        for (var i = 0; i < 3 - output_id_hex.Length; i++) padded_output_hex = "0" + padded_output_hex;

        GenomeString = padded_input + ">" + padded_bias + "<" + padded_output_hex;
        GenomeDec = Convert.ToInt32(padded_input + padded_bias + padded_output_hex, 16);
        InputNeuron = inputNeuron;
        OutputNeuron = outputNeuron;
        Bias = bias;
    }

    public static Gene New(Gene gene)
    {
        return new Gene(gene.InputNeuron, gene.OutputNeuron, gene.Bias);
    }

    public double calculate(double value)
    {
        return Math.Tanh(value * (Bias / 2048));
    }

    public Gene Mutate(NeuronGroup neurons)
    {
        // Creates a new copy of self
        var new_gene = new Gene(InputNeuron, OutputNeuron, Bias);
        // Randomly changes a property
        switch (Random.Range(0, 1))
        {
            case 0:
                var available_input_neurons = neurons.InputNeurons.FindAll(n =>
                    n.ID != new_gene.InputNeuron.ID && n.TotalOutputs < n.MaxOutputs);
                var available_input_through_neurons = neurons.ThroughNeurons.FindAll(n =>
                    n.ID != new_gene.InputNeuron.ID && n.TotalOutputs < n.MaxOutputs);
                var available_output_through_neurons = neurons.ThroughNeurons.FindAll(n =>
                    n.ID != new_gene.InputNeuron.ID && n.TotalInputs < n.MaxInputs);
                var available_output_neurons = neurons.OutputNeurons.FindAll(n =>
                    n.ID != new_gene.InputNeuron.ID && n.TotalInputs < n.MaxInputs);

                if (Random.Range(0, 1) == 0)
                {
                    // Determines which type the input neuron will be
                    var random = Random.Range(0, 1);
                    if (random == 0)
                        new_gene.InputNeuron =
                            available_input_neurons[Random.Range(0, available_input_neurons.Count)];
                    else
                        new_gene.InputNeuron =
                            available_input_through_neurons[
                                Random.Range(0, available_input_through_neurons.Count)];
                }
                else
                {
                    // Determines which type the output neuron will be
                    var random = Random.Range(0, 1);
                    if (random == 0)
                        new_gene.OutputNeuron =
                            available_output_through_neurons[
                                Random.Range(0, available_output_through_neurons.Count)];
                    else
                        new_gene.OutputNeuron =
                            available_output_neurons[
                                Random.Range(0, available_output_neurons.Count)];
                }


                break;
            case 1:
                // Mess with the bias
                if (new_gene.Bias == 0)
                    new_gene.Bias += 1;
                else if (new_gene.Bias == 2048)
                    new_gene.Bias -= 1;
                else
                    new_gene.Bias += (int) Math.Tanh(Random.Range(-1, 1));
                break;
        }

        return new_gene;
    }
}