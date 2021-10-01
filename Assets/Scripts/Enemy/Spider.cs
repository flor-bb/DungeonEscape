using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{

    public GameObject acidEffectPrefab;
    public int health { get; set; }


    public override void Init()
    {
        base.Init();
        health = base.health;

    }

    public override void Update()
    {
      
    }

    public void Damage()
    {
        health--;
        if(health < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public override void Movement()
    {
        //Dont move
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);

    }




}
