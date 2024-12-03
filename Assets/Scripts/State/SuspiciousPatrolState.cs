using System.Collections.Generic;
using UnityEngine;

public class SuspiciousPatrolState : State
{
    private int indexPatrol = 0;

    public SuspiciousPatrolState(Enemy enemy) : base(enemy) { }

    //Think DE PATRULLAR SOSPECHOSAMENTE STATE -> si ve al jugador -> debe cambiar al estado seguir
    //                                         -> si se acaban los waypoints -> debe cambiar al estado patrulla
    public override void Think()
    {
        
    }

    public override void Act()
    {
        
    }
}
