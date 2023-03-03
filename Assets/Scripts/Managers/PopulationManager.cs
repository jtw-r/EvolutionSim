using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PopulationManager
{
    public static int genome_count = 5;

    public List<Creature> Creatures = new();
    public int DesiredPopulationCount = 50;
    public int Generation = 1;
    private PositionManager PositionManager;
    public int Year;

    public PopulationManager(PositionManager positionManager)
    {
        PositionManager = positionManager;
    }

    public void CreateInitialPopulation()
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
                        Debug.Log("Output #01");
                        Debug.Log(value);
                        if (value >= 0.5)
                        {
                            var pos = new Vector2(-1, 0);

                            creature.Move(pos);
                        }
                        //Do some action depending on the values
                    }),
                    new("Output #02", 5, (creature, value) =>
                    {
                        Debug.Log("Output #02");
                        Debug.Log(value);
                        if (value >= 0.5) creature.Move(new Vector2(1, 0));
                        //Do some action depending on the values
                    }),
                    new("Output #03", 5, (creature, value) =>
                    {
                        Debug.Log("Output #03");
                        Debug.Log(value);
                        if (value >= 0.5) creature.Move(new Vector2(0, -1));
                        //Do some action depending on the values
                    }),
                    new("Output #04", 5, (creature, value) =>
                    {
                        Debug.Log("Output #04");
                        Debug.Log(value);
                        if (value >= 0.5) creature.Move(new Vector2(0, 1));
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

    public void Remove(Creature creature)
    {
        Debug.Log(Creatures.Count);
        creature.Destroy();

        Debug.Log(Creatures.Count);
    }

    public void Step()
    {
        foreach (var creature in Creatures) creature.Step();
        Year++;
    }

    public void AdvanceGeneration()
    {
        var to_reproduce = new List<Creature>();
        var avg_move = (int) Creatures.Average(C => C.positional.TimesMoved);
        Debug.Log("VALUE AVG_MOVE = " + avg_move);
        foreach (var creature in Creatures)
            if (creature.positional.TimesMoved < avg_move)
            {
                if (Random.Range(1, avg_move - creature.positional.TimesMoved) == 1)
                    to_reproduce.Add(creature);
            }
            else
            {
                to_reproduce.Add(creature);
            }


        Debug.Log("VALUE to_reproduce = " + to_reproduce.Count);
        if (to_reproduce.Count < 2) return;

        PositionManager.Reset();

        var new_creatures = new List<Creature>();
        while (new_creatures.Count <= DesiredPopulationCount)
            for (var i = 0; i < to_reproduce.Count - 2; i += 2)
            {
                var creature = to_reproduce[i].Mate(to_reproduce[i + 1]);
                if (creature.positional == null)
                {
                    creature.Destroy();
                    continue;
                }

                new_creatures.Add(creature);
            }


        foreach (var creature in Creatures) Remove(creature);

        Creatures = new List<Creature>();
        var ready = new_creatures.FindAll(creature => creature.positional != null);
        if (ready.Count > DesiredPopulationCount)
        {
            var to_remove = ready.GetRange(DesiredPopulationCount, ready.Count - DesiredPopulationCount);
            foreach (var creature in to_remove) creature.Destroy();
            Creatures = ready.GetRange(0, DesiredPopulationCount);
        }
        else
        {
            Creatures = ready;
        }

        if (PositionManager.Positionals.Count > DesiredPopulationCount)
            PositionManager.Positionals = PositionManager.Positionals.GetRange(0, DesiredPopulationCount);

        Year = 0;
        Generation++;
    }
}