using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatCategories { Preditor, Prey, Water, Land, Mammal, NotMammal }

public enum Animals
{
    Porcupine, Bear, Octopus, Crocodile, Hippo, Gorilla, Jaguar, Giraffe, Caesar, Dolphin, Bees, Duck, Snake, Shark,
    Catfish, Eagle, Ants, Spider, Jellyfish, Bunny, Krill, Dog, Skunk, Squirrel, Badger
}

public class PlayerStats : MonoBehaviour
{
    public int preditorTally;
    public int preyTally;
    public int waterTally;
    public int landTally;
    public int mammalTally;
    public int notMammalTally;

    private List<Animals> matchAttempts = new List<Animals>();

    public void AddTallies(List<StatCategories> categoryTallies)
    {
        for (int i = 0; i < categoryTallies.Count; i++)
        {
            Debug.LogError(categoryTallies[i]);
            switch (categoryTallies[i])
            {
                case StatCategories.Preditor:
                    preditorTally++;
                    break;

                case StatCategories.Prey:
                    preyTally++;
                    break;

                case StatCategories.Water:
                    waterTally++;
                    break;

                case StatCategories.Land:
                    landTally++;
                    break;

                case StatCategories.Mammal:
                    mammalTally++;
                    break;

                case StatCategories.NotMammal:
                    notMammalTally++;
                    break;
            }
        }
    }

    public void AddAnimalMatchAttempt(Animals _animal)
    {
        matchAttempts.Add(_animal);
    }

    public void DisplayStats()
    {
        Debug.LogError("Predator: " + preditorTally);
        Debug.LogError("Prey: " + preyTally);
        Debug.LogError("Water: " + waterTally);
        Debug.LogError("Land: " + landTally);
        Debug.LogError("Mammal: " + mammalTally);
        Debug.LogError("Not Mammal: " + notMammalTally);
    }

    public void DisplayAnimalsMatchAttempts()
    {
        for (int i = 0; i < matchAttempts.Count; i++)
        {
            Debug.LogError(matchAttempts[i]);
        }
    }


}
