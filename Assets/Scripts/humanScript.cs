using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class humanScript : MonoBehaviour
{

    public string state = "Healthy";
    public bool sick = false;
    public bool moving = true;

  
    public int humanSpeed = 3;
    public int sickTime = 10 * 60;

    public Vector2 target;
    SpriteRenderer spriteRenderer;


    void Start()
    {
        transform.position = new Vector3 (Random.Range(-8,8), Random.Range(-4,4));
        StartCoroutine(MoveSwitch());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator MoveSwitch()
    {

        while (true) 
        {
            float timer = Random.Range(3, 8);
            moving = true;
            target = Direction()*Random.Range(1,10);
            yield return new WaitForSeconds(timer);
        }

       // yield return null;
    }

    IEnumerator InfectionRoll()
    {
         //Debug.Log("Infection Roll");
         float timer = Random.Range(5, 10);
         yield return new WaitForSeconds(timer);
         RollInfDice();
    }

    IEnumerator SurvivalRoll()
    {
        float timer = Random.Range(5, 15);
        yield return new WaitForSeconds(timer);
        RollSurDice();
    }

    private Vector2 Direction() 
    {
        Vector2 returnValue; 
        returnValue = new Vector2(Random.Range(-1,2), Random.Range(-1,2));
        return returnValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            target = new Vector2(-target.x, -target.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == "Sick")
        {
            StartCoroutine(InfectionRoll());
        }
        
    }

    void RollInfDice()
    {
        int dice = Random.Range(1,100);
        Debug.Log("Rolled dice " + dice);
        
        if (dice > 1 && dice <= 10 && state != "Sick" && state != "Immune")
        {
            state = "Sick";
            ChangeColor();
            

        }
       /* else if (dice >= 10 && dice < 100 && state != "Healthy")
        {
            state = "Healthy";
            ChangeColor();
        } */

    }

    void RollSurDice()
    {


        int dice = Random.Range(1, 100);
        Debug.Log("Rolled survival dice " + dice);

        if (dice > 1 && dice < 10 && state != "Dead")
        {
            state = "Dead";
            GameObject.Destroy(gameObject);

        }

        else if (dice >= 10 && dice < 20 && state != "Immune")
        {
            state = "Immune";
            sick = false;
            ChangeColor();
        }

        else if (dice >= 20 && dice <= 100 && state != "Healthy")
        {
            state = "Healthy";
            sick = false;
            ChangeColor();
        }
    } 


    void ChangeColor()
    {
        if (state == "Healthy")
        {
            spriteRenderer.color = Color.white;
        }

        else if (state == "Sick")
        {
            spriteRenderer.color = Color.red;
        }
        else if (state == "Immune")
        {
            spriteRenderer.color = Color.green;
        }

        else
        {
            spriteRenderer.color = Color.white;
        } 

    }

    void Update()
    {
       if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, humanSpeed * Time.deltaTime);
        }

       if (transform.position.x > 9 || transform.position.x < -9)
        {
            target = new Vector2(-target.x, target.y);
        }

        if (transform.position.y > 5 || transform.position.y < -5)
        {
            target = new Vector2(target.x, -target.y);
        }

        //ChangeColor();
        if (state == "Sick" && !sick && state != "Immune")
        {
            sick = true;
            StartCoroutine(SurvivalRoll());
        }



    }


}
