using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatCategories { Preditor, Prey, Water, Land, Mammal, NotMammal }

public enum Animals
{
    Porcupine, Bear, Octopus, Crocodile, Hippo, Gorilla, Jaguar, Giraffe, Caesar, Dolphin, Bees, Duck, Snake, Shark,
    Catfish, Eagle, Ants, Spider, Jellyfish, Bunny, Krill, Dog, Skunk, Squirrel, Badger
}

public class PlayerStats : MonoBehaviour
{
    [Serializable]
    public struct AnimalStats
    {
        public Animals animalType;
        public int predatorNumber;
        public int preyNumber;
        public int landNumber;
        public int waterNumber;
        public int mammalNumber;
        public int notMammalNumber;
    }

    public struct AnimalCompatibility
    {
        public Animals animalType;
        public int compatibilityScore;
        public bool dealBreaker;

        public AnimalCompatibility(Animals _animal, int _score, bool _dealBreaker)
        {
            animalType = _animal;
            compatibilityScore = _score;
            dealBreaker = _dealBreaker;
        }
    }

    public int preditorTally;
    public int preyTally;
    public int waterTally;
    public int landTally;
    public int mammalTally;
    public int notMammalTally;

    [SerializeField] private List<AnimalStats> allAnimalStats = new List<AnimalStats>();

    private List<Animals> matchAttempts = new List<Animals>();

    private List<Animals> hugAccepts = new List<Animals>();

    private List<Animals> hugRejects = new List<Animals>();

    private List<Animals> animalMatches = new List<Animals>();

    private List<Animals> dealBreakerAnimals = new List<Animals>();

    private List<AnimalCompatibility> compatibilityList = new List<AnimalCompatibility>();

    public void AddTallies(List<StatCategories> categoryTallies)
    {
        for (int i = 0; i < categoryTallies.Count; i++)
        {
            //Debug.LogError("Stat: " + categoryTallies[i]);

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

    public void AddHugAccept(Animals _animal)
    {
        hugAccepts.Add(_animal);
    }

    public void AddHugRejects(Animals _animal)
    {
        hugRejects.Add(_animal);
    }

    public int GetNumHugAccepts()
    {
        return hugAccepts.Count;
    }

    public int GetNumMatchAttempts()
    {
        return matchAttempts.Count;
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

    public void DetermineCompatibliies()
    {
        for (int i = 0; i < allAnimalStats.Count; i++)
        {
            int compatibilityScore = 0;
            AnimalStats animalStat = allAnimalStats[i];

            compatibilityScore += Mathf.Abs(preditorTally - animalStat.predatorNumber);
            compatibilityScore += Mathf.Abs(preyTally - animalStat.preyNumber);
            compatibilityScore += Mathf.Abs(landTally - animalStat.landNumber);
            compatibilityScore += Mathf.Abs(waterTally - animalStat.waterNumber);
            compatibilityScore += Mathf.Abs(mammalTally - animalStat.mammalNumber);
            compatibilityScore += Mathf.Abs(notMammalTally - animalStat.notMammalNumber);

            bool dealbreaker = false;
            for (int j = 0; j < dealBreakerAnimals.Count; j++)
            {
                if (animalStat.animalType == dealBreakerAnimals[j])
                {
                    dealbreaker = true;
                }
            }

            bool acceptedHug = false;
            for (int j = 0; j < hugAccepts.Count; j++)
            {
                if (animalStat.animalType == hugAccepts[j])
                {
                    acceptedHug = true;
                }
            }

            if (!dealbreaker && !acceptedHug)
            {
                AddToCompatibiltyList(animalStat.animalType, compatibilityScore, dealbreaker);
            }

            
        }

    }

    public void DetermineMatches()
    {
        for (int i = 0; i < matchAttempts.Count; i++)
        {
            bool isMatch = true;
            AnimalStats animalStat = GetAnimalStats(matchAttempts[i]);

            if (preditorTally < animalStat.predatorNumber)
            {
                isMatch = false;
            }

            if (preditorTally < animalStat.preyNumber)
            {
                isMatch = false;
            }

            if (landTally < animalStat.landNumber)
            {
                isMatch = false;
            }

            if (waterTally < animalStat.waterNumber)
            {
                isMatch = false;
            }

            if (mammalTally < animalStat.mammalNumber)
            {
                isMatch = false;
            }

            if (notMammalTally < animalStat.notMammalNumber)
            {
                isMatch = false;
            }

            if (isMatch)
            {
                animalMatches.Add(matchAttempts[i]);
            }

        }
    }

    public void AddAnimalMatch(Animals _type)
    {
        animalMatches.Add(_type);
    }

    public void AddToCompatibiltyList(Animals _animal, int _score, bool _dealbreaker)
    {
        AnimalCompatibility newCompatibility = new AnimalCompatibility(_animal, _score, _dealbreaker);
        compatibilityList.Add(newCompatibility);
    }

    public List<AnimalCompatibility> GetCompatiblityList()
    {
        //sort list
        compatibilityList.Sort((animal1, animal2) => animal2.compatibilityScore.CompareTo(animal1.compatibilityScore));
        //add dealBreakers
        for (int i = 0; i < dealBreakerAnimals.Count; i++)
        {
            AnimalCompatibility newCompatibility = new AnimalCompatibility(dealBreakerAnimals[i], 1000, true);
            compatibilityList.Add(newCompatibility);
        }

        //add hug accepts to front of list
        for (int i = 0; i < hugAccepts.Count; i++)
        {
            AnimalCompatibility newCompatibility = new AnimalCompatibility(hugAccepts[i], 0, false);
            compatibilityList.Insert(0, newCompatibility);
        }

        return compatibilityList;
    }

    public List<Animals> GetMatches()
    {
        return animalMatches;
    }

    public List<Animals> GetMatchAttemptsList()
    {
        return matchAttempts;
    }

    public AnimalStats GetAnimalStats(Animals _animalType)
    {
        for (int i = 0; i < allAnimalStats.Count; i++)
        {
            if (allAnimalStats[i].animalType == _animalType)
            {
                return allAnimalStats[i];
            }
        }

        return allAnimalStats[0];
    }

    public void AddDealBreakerAnimal(Animals _animalType)
    {
        dealBreakerAnimals.Add(_animalType);
    }


}
