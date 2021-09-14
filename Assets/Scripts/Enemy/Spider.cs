using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int health { get; set; }
    public void Damage() { }

    public override void Init()
    {
        base.Init();
        health = base.health;

    }

}
