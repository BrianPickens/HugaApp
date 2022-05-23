using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsScreen : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private TextMeshProUGUI predatorStat;
    [SerializeField] private TextMeshProUGUI preyStat;
    [SerializeField] private TextMeshProUGUI landStat;
    [SerializeField] private TextMeshProUGUI waterStat;
    [SerializeField] private TextMeshProUGUI mammalStat;
    [SerializeField] private TextMeshProUGUI notMammalStat;

    [SerializeField] private TextMeshProUGUI numMatchAttempts;

    [SerializeField] private TextMeshProUGUI numMatches;
    [SerializeField] private TextMeshProUGUI numMatchAccepts;

    [SerializeField] private List<TextMeshProUGUI> compatibilityText = new List<TextMeshProUGUI>();
    public void ShowStatsScreen()
    {
        FillStats();
        gameObject.SetActive(true);
    }

    private void FillStats()
    {
        predatorStat.text = "Predator Score: " + playerStats.preditorTally.ToString();
        preyStat.text = "Prey Score: " + playerStats.preyTally.ToString();
        landStat.text = "Land Score: " + playerStats.landTally.ToString();
        waterStat.text = "Water Score: " +playerStats.waterTally.ToString();
        mammalStat.text = "Mammal Score: " + playerStats.mammalTally.ToString();
        notMammalStat.text = "Not Mammal Score: " + playerStats.notMammalTally.ToString();

        numMatchAttempts.text = "Hug Attempts: " + playerStats.GetNumMatchAttempts().ToString();
        numMatches.text = "Hug Matches: " + playerStats.GetMatches().Count.ToString() + "/25";

        playerStats.DetermineCompatibliies();

        List<PlayerStats.AnimalCompatibility> animalCompatibilities = new List<PlayerStats.AnimalCompatibility>();
        animalCompatibilities = playerStats.GetCompatiblityList();

        for (int i = 0; i < animalCompatibilities.Count; i++)
        {
            string newText = (i + 1).ToString() + ". " + NamesAndLines.Instance.GetAnimalName(animalCompatibilities[i].animalType).ToString();
            if (animalCompatibilities[i].dealBreaker)
            {
                newText += " " + "(DB)";
            }
            
            compatibilityText[i].text = newText;
        }

    }
}
