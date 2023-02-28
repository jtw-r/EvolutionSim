using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PopulationManager
{
    public PositionManager PositionManager;
    
    public NeuronGroup BrainTemplate = new StartingNeuronTemplate(
        new List<InputNeuron>(){new InputNeuron(NeuronInputCategory.Sensory, 5), new InputNeuron(NeuronInputCategory.Sensory, 5), new InputNeuron(NeuronInputCategory.Sensory, 5), new InputNeuron(NeuronInputCategory.Sensory, 5), new InputNeuron(NeuronInputCategory.Sensory, 5), new InputNeuron(NeuronInputCategory.Sensory, 5)},
        5,
        new List<OutputNeuron>{new OutputNeuron(5), new OutputNeuron(5),new OutputNeuron(5),new OutputNeuron(5)}
    );

    public static int genome_count = 10;

    public List<Creature> Creatures = new List<Creature>();
    public int Generation = 0;

    public PopulationManager(PositionManager positionManager)
    {
        this.PositionManager = positionManager;
    }

    public void CreateInitialPopulation(int DesiredPopulationCount)
    {
        for (int i = 0; i < DesiredPopulationCount; i++)
        {
            var dna = new DNA(new List<Genome>());
            for (int g = 0; g < genome_count; g++)
            {
                // 0 = InputNeuron, 1 = ThroughNeuron
                var randomA = Random.Range(0, 1);
                NeuronFlowType a_type;
                switch (randomA)
                {
                    default:
                    case 0:
                        a_type = NeuronFlowType.Out;
                        break;
                    case 1:
                        a_type = NeuronFlowType.Through;
                        break;
                }
                
                // 0 = ThroughNeuron, 1 = OutputNeuron
                var randomB = Random.Range(0, 1);
                NeuronFlowType b_type;
                switch (randomA)
                {
                    default:
                    case 0:
                        b_type = NeuronFlowType.Through;
                        break;
                    case 1:
                        b_type = NeuronFlowType.In;
                        break;
                }

                var inputs_to_chose_from = this.BrainTemplate.neurons.FindAll((n) => n.Flow == a_type);
                var outputs_to_chose_from = this.BrainTemplate.neurons.FindAll((n) => n.Flow == b_type);

                var input = inputs_to_chose_from[(Random.Range(0, inputs_to_chose_from.Count - 1))];
                var output = outputs_to_chose_from[(Random.Range(0, outputs_to_chose_from.Count - 1))];
                
                dna.genomes.Add(new Genome(input, output, Random.Range(0, 4096 - 1)));
            }
            var new_brain = new Brain(BrainTemplate, dna);
            var creature = new Creature(i, new_brain, PositionManager.AddPositional());
            
            this.Creatures.Add(creature);
        }
    }

    public List<Creature> AllCreatures()
    {
        return this.Creatures;
    }

    public List<Creature> SelectWhere(Predicate<Creature> query)
    {
        return this.Creatures.FindAll(query);
    }

    public void Remove(IEnumerable<Creature> creatures_to_remove)
    {
        foreach (var creature in creatures_to_remove)
        {
            this.Creatures.Remove(creature);
        }
    }

    public void AdvanceGeneration(int DesiredPopulationCount)
    {
        if (this.Creatures.Count >= DesiredPopulationCount)
        {
            return;
        }

        var population_to_fill_count = DesiredPopulationCount - this.Creatures.Count;

    }
}
