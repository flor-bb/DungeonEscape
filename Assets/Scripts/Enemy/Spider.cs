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

        if (isDead)
        {
            return;
        }
        health--;
        if(health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
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
