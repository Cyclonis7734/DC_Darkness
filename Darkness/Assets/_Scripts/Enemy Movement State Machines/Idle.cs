using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IEnemyActions
{

    EnemySM ESM;

    //Constructor
    public Idle(EnemySM newESM)
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

    }

    public void IsMoving()
    {
        ESM.SetEnemyState(ESM.getIsMoving());
        ESM.HandleWandering();
    }

    public void IsPursuing()
    {
        ESM.SetEnemyState(ESM.getIsPursuing());
        ESM.PursuePlayer();
    }

}
