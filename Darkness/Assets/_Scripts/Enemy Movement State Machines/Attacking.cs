using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : IEnemyActions
{

    EnemySM ESM;

    //Constructor
    public Attacking(EnemySM newESM)
    {
        ESM = newESM;
    }

    public void IsAttacking()
    {
        
    }

    public void IsDying()
    {
        ESM.SetEnemyState(ESM.getIsDeathSet());
        ESM.KillThisEnemy();
    }

    public void IsIdle()
    {
        ESM.SetEnemyState(ESM.getIsIdle());
        ESM.StartCoroutine(ESM.IHandleLostPlayer());
    }

    public void IsMoving()
    {

    }

    public void IsPursuing()
    {
        ESM.SetEnemyState(ESM.getIsPursuing());
        ESM.PursuePlayer();
    }
}
