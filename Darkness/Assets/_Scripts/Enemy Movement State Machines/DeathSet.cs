using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSet : IEnemyActions
{

    EnemySM ESM;

    //Constructor
    public DeathSet(EnemySM newESM)
    {
        ESM = newESM;
    }

    public void IsAttacking() { }
    public void IsDying() { }
    public void IsIdle() { }
    public void IsMoving() { }
    public void IsPursuing() { }
}
