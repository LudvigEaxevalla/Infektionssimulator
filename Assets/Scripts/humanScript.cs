using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

public class humanScript : MonoBehaviour
{

    //public string state = "Healthy";
    public bool sick = false;
    public bool moving = false;
    public bool infected = false;
    bool infectionRolling = false;
  
    public int humanSpeed = 2;

    public Vector2 target;
    SpriteRenderer spriteRenderer;

    public enum HumanState
    {
        Healthy,
        Sick,
        Immune,
        Dead
    }

    public HumanState state; 

    void Start()
    {

        if (Random.Range(0,10) < 1)
        {
            Debug.Log("Started sick");
            state = HumanState.Sick;
            StartCoroutine(InfectionRoll());
        }

        transform.position = new Vector3 (Random.Range(-8,8), Random.Range(-4,4));
        StartCoroutine(MoveSwitch());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator MoveSwitch()
    {

        while (true) 
        {
            float timer = Random.Range(3, 6);
            yield return new WaitForSeconds(timer);
            moving = true;
            //target = Direction()*Random.Range(-30,30);
            target = (Vector2)transform.position + Direction() * Random.Range(2f, 6f);

        }

        // yield return null;
    }

    IEnumerator InfectionRoll()
    {
         infectionRolling = true;
         float timer = Random.Range(5, 10);
         yield return new WaitForSeconds(timer);
         RollInfDice();
         infectionRolling = false;
    }

    IEnumerator SurvivalRoll()
    {
        float timer = Random.Range(5, 8);
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
            moving = false;
            target = new Vector2(-target.x, -target.y);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        humanScript otherHuman = collision.GetComponent<humanScript>();

        if (otherHuman == null) return;

        if (state == HumanState.Healthy && otherHuman.state == HumanState.Sick && !infectionRolling)
        {
            StartCoroutine(InfectionRoll());
        }

    }

    void RollInfDice()
    {
        int dice = Random.Range(0,100);
        Debug.Log("Rolled INFECTION dice " + dice);
        
        if (dice > 0 && dice <= 50 && state != HumanState.Sick && state != HumanState.Immune)
        {
            state = HumanState.Sick;
            UpdateColor();
            

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
        Debug.Log("Rolled SURVIVAL dice " + dice);

        if (dice > 1 && dice < 10 && state != HumanState.Dead)
        {
            state = HumanState.Dead;
            GameObject.Destroy(gameObject);

        }

        else if (dice >= 10 && dice < 20 && state != HumanState.Immune)
        {
            state = HumanState.Immune;
            sick = false;
            UpdateColor();
        }

        else if (dice >= 20 && dice <= 100 && state != HumanState.Healthy)
        {
            state = HumanState.Healthy;
            sick = false;
            UpdateColor();
        }
    }


    void UpdateColor()
    {
        switch (state)
        {
            case HumanState.Healthy:
                spriteRenderer.color = Color.white;
                break;
            case HumanState.Sick:
                spriteRenderer.color = Color.red;
                break;
            case HumanState.Immune:
                spriteRenderer.color = Color.green;
                break;
        }
    }

    void Update()
    {

        UpdateColor();

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

        if (state == HumanState.Sick && !sick && state != HumanState.Immune)
        {
            sick = true;
            StartCoroutine(SurvivalRoll());
        }


    }


}
