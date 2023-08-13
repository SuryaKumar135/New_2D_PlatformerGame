using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public bool canRaciveInput;
    public bool inputRecived;
    public static PlayerAttackScript instance;
    private PlayerMoment playerMomentInstance;
    public BoxCollider2D boxCollider;
    private void Awake()
    {
        instance = this;
        playerMomentInstance=GetComponentInParent<PlayerMoment>();
      
       
    }
    private void Update()
    {
       Attack();
       
    }
    public void Attack()
    {
        if((Input.GetMouseButtonDown(0)))
        {

           
            if (canRaciveInput)
            {
               
                inputRecived = true;
                canRaciveInput = false;
            }
            else
            {
              
                return;
            }
        }
    }
    public void InputTrigger()
    {
        if(!canRaciveInput)
        {
            canRaciveInput=true;
        }
        else
        {
            canRaciveInput = false;
        }
    }

    public void turnOnAttackPoint()
    {
       boxCollider.enabled = true;
    }
    public void turnOffAttackPoint()
    {
        boxCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
    }
}
