using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Genome
{
    protected int GenomeDec;
    public string GenomeString;
    public Neuron InputNeuron;
    public Neuron OutputNeuron;
    public int Bias;

    public Genome(Neuron inputNeuron, Neuron outputNeuron, int bias)
    {
        this.GenomeString = inputNeuron.ID.ToString("X")  + ">" + bias.ToString("X") + "<" + outputNeuron.ID.ToString("X");;
        this.GenomeDec = Convert.ToInt32(this.GenomeString.Replace(">", "").Replace("<", ""), 16);
        this.InputNeuron = inputNeuron;
        this.OutputNeuron = outputNeuron;
        this.Bias = bias;
    }
}
