using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    public TMP_Text populationText;
    public TMP_Text deathText;
    public TMP_Text immuneText;
    public TMP_Text infectedText;
    public TMP_Text healthyText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulationUpdate()
    {
        populationText.text = "Population: " + initScript.population;

    }
    public void DeathCountUpdate()
    {
        humanScript.deathCount++;
        deathText.text = "Deaths: " + humanScript.deathCount;

    }
    public void ImmuneCountUpdate()
    {
        humanScript.immuneCount++;
        immuneText.text = "Immune: " + humanScript.immuneCount;

    }
    public void InfectedCountUpdate()
    {
        infectedText.text = "Infected: " + humanScript.infectedCount;


    }
    public void HealthyCountUpdate()
    {
        healthyText.text = "Healthy: " + humanScript.healthyCount;

    }
}
