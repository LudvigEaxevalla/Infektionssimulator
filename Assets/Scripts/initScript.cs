using UnityEngine;

public class initScript : MonoBehaviour
{

    public GameObject human;
    public int startNumber = 50;
    void Start()
    {
        for (var i = 0; i < startNumber; i++)
        {
            Instantiate(human, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
