using UnityEngine;

public class initScript : MonoBehaviour
{

    public GameObject human;
    public int startNumber = 50;
    public static int population = 0;
    public UIScript uiScript;

    void Start()
    {
        for (var i = 0; i < startNumber; i++)
        {
            Instantiate(human, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
            population++;
            Debug.Log(population);
        }
        uiScript.PopulationUpdate();
        uiScript.DeathCountUpdate();
        uiScript.ImmuneCountUpdate();
        uiScript.InfectedCountUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
