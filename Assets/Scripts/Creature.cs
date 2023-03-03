using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Creature
{
    public Brain brain;
    public int ID;
    public PositionalObject positional;

    private List<Try> ToExecute = new();

    public Creature(int ID, Brain brain, PositionalObject positional)
    {
        this.ID = ID;
        this.brain = brain;
        this.positional = positional;
        this.brain.Compute(this);
    }

    public void Move(Vector2 delta)
    {
        ToExecute.Add(new Try(TryType.Move,
            creature => { positional.Translate(new Vector3(delta.x, delta.y, 0)); }));
    }

    public void Step()
    {
        brain.Compute(this);
        var Moves = ToExecute.FindAll(t => t.Type == TryType.Move);
        if (Moves.Count > 0) Moves[Random.Range(0, Moves.Count)].Action(this);


        ToExecute = new List<Try>();
    }

    public Creature Mate(Creature partner)
    {
        var creatureA = this;
        var creatureB = partner;

        var new_genes = new List<Gene>();
        var avg_genes =
            (int) Math.Ceiling((creatureA.brain.dna.genomes.Count + creatureB.brain.dna.genomes.Count) / 2f);
        for (var i = 0; i < avg_genes; i++)
        {
            Gene gene;
            if (Random.Range(0, 1) == 0)
            {
                // Copy from creatureA

                if (creatureA.brain.dna.genomes.Count < i)
                    gene = creatureA.brain.dna.genomes[i];
                else
                    gene = creatureB.brain.dna.genomes[i];
            }
            else
            {
                // Copy from creatureB
                if (creatureB.brain.dna.genomes.Count < i)
                    gene = creatureB.brain.dna.genomes[i];
                else
                    gene = creatureA.brain.dna.genomes[i];
            }

            if (Random.Range(0, 20) == 0)
            {
                Debug.Log("Mutating gene");
                new_genes.Add(gene.Mutate(creatureA.brain.neurons));
            }
            else
            {
                new_genes.Add(gene);
            }
        }

        var new_dna = new DNA(new_genes);
        var new_brain = new Brain(creatureA.brain.neurons, new_dna);
        return new Creature(0, new_brain, positional.Manager.AddPositional());
    }

    public void Destroy()
    {
        positional.Destroy();
    }
}