using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{

    public int health { get; set; }
   

    public override void Init()
    {
        base.Init();
        health = base.health;
    }

    public override void Movement()
    {

        base.Movement();

    }

    public void Damage()
    {
        health--;
        anim.SetTrigger("Hit");
        isHit = true;

        anim.SetBool("InCombat", true);

        if (health < 1)
        {
            Destroy(this.gameObject);
        }

    }


}
