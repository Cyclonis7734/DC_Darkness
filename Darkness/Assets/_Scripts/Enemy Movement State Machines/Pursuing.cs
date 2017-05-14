using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuing : IEnemyActions
{

    EnemySM ESM;

    //Constructor
    public Pursuing(EnemySM newESM)
    {
        ESM = newESM;
    }

    public void IsAttacking()
    {
        ESM.SetEnemyState(ESM.getIsAttacking());
        ESM.HandleAttacking();
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
        ESM.PursuePlayer();
    }
}
