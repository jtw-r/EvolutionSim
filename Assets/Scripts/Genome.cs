using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Genome
{
    public int GenomeDec;
    public string GenomeString;
    public Neuron InputNeuron;
    public Neuron OutputNeuron;
    public int Bias;

    public double calculate(double value)
    {
        return Math.Tanh(value * (this.Bias / 2048));
    }

    public Genome(Neuron inputNeuron, Neuron outputNeuron, int bias)
    {
        var input_id_hex = inputNeuron.ID.ToString("x");
        var padded_input = input_id_hex;
        for (int i = 0; i < 3 - input_id_hex.Length; i++)
        {
            padded_input = "0" + padded_input;
        }

        var bias_hex = bias.ToString("X");
        var padded_bias = bias_hex;
        for (int i = 0; i < 3 - bias_hex.Length; i++)
        {
            padded_bias = "0" + padded_bias;
        }
        
        var output_id_hex = outputNeuron.ID.ToString("X");
        var padded_output_hex = output_id_hex;
        for (int i = 0; i < 3 - output_id_hex.Length; i++)
        {
            padded_output_hex = "0" + padded_output_hex;
        }
        
        this.GenomeString = padded_input  + ">" + padded_bias + "<" + padded_output_hex;
        this.GenomeDec = Convert.ToInt32(padded_input+padded_bias+padded_output_hex, 16);
        this.InputNeuron = inputNeuron;
        this.OutputNeuron = outputNeuron;
        this.Bias = bias;
    }
}
