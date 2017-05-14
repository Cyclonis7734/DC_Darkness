using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IEnemyActions
    {
        void IsMoving();
        void IsAttacking();
        void IsDying();
        void IsIdle();
        void IsPursuing();
    }

