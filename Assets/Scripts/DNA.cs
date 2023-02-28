
using System;
using System.Collections.Generic;

[Serializable]
public class DNA
{
    public List<Genome> genomes;
    public string string_representation;

    public DNA(List<Genome> genomes)
    {
        this.genomes = genomes;
        this.string_representation = "";
        foreach (var genome in genomes)
        {
            this.string_representation += genome.GenomeString + " ";
        }

        this.string_representation = this.string_representation.Trim();
    }
}