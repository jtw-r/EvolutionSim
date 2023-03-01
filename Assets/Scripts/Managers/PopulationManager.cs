using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PopulationManager
{
    public static int genome_count = 5;

    public List<Creature> Creatures = new();
    public int Generation;
    public int Year;
    private PositionManager PositionManager;

    public PopulationManager(PositionManager positionManager)
    {
        PositionManager = positionManager;
    }

    public void CreateInitialPopulation(int DesiredPopulationCount)
    {
        for (var i = 0; i < DesiredPopulationCount; i++)
        {
            var brain_template = new StartingNeuronTemplate(
                new List<InputNeuron>
                {
                    new("Constant #1", NeuronInputCategory.Sensory, 5, creature => 1),
                    new("Age", NeuronInputCategory.Sensory, 5, creature => Year / 100.0),
                    new("Constant #2", NeuronInputCategory.Sensory, 5, creature => 0.5)
                },
                2,
                new List<OutputNeuron>
                {
                    new("Output #01", 5, (creature, value) =>
                    {
                        if (value >= 0.5)
                        {
                            creature.Move(new Vector2(-1, 0));
                            Debug.Log("Output #01");
                        }
                        //Do some action depending on the values
                    }),
                    new("Output #02", 5, (creature, value) =>
                    {
                        if (value >= 0.5)
                        {
                            creature.Move(new Vector2(1, 0));
                            Debug.Log("Output #02");
                        }
                        //Do some action depending on the values
                    }),
                    new("Output #03", 5, (creature, value) =>
                    {
                        if (value >= 0.5)
                        {
                            creature.Move(new Vector2(0, -1));
                            Debug.Log("Output #03");
                        }
                        //Do some action depending on the values
                    }),
                    new("Output #04", 5, (creature, value) =>
                    {
                        if (value >= 0.5)
                        {
                            creature.Move(new Vector2(0, 1));
                            Debug.Log("Output #04");
                        }
                        //Do some action depending on the values
                    })
                }
            );

            var dna = new DNA();
            dna.FullyRandomize(genome_count, brain_template);

            var new_brain = new Brain(brain_template, dna);
            var creature = new Creature(i, new_brain, PositionManager.AddPositional());

            Creatures.Add(creature);
        }
    }

    public List<Creature> AllCreatures()
    {
        return Creatures;
    }

    public List<Creature> SelectWhere(Predicate<Creature> query)
    {
        return Creatures.FindAll(query);
    }

    public void Remove(IEnumerable<Creature> creatures_to_remove)
    {
        foreach (var creature in creatures_to_remove) Creatures.Remove(creature);
    }

    public void Step()
    {
        foreach (var creature in Creatures)
        {
            creature.Step();
            Year++;
        }
    }

    public void AdvanceGeneration(int DesiredPopulationCount)
    {
        if (Creatures.Count >= DesiredPopulationCount) return;

        var population_to_fill_count = DesiredPopulationCount - Creatures.Count;
        Year = 0;
    }
}