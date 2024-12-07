using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FollowPlayerState : State
{
    public FollowPlayerState(Enemy enemy) : base(enemy)
    {

    }

    public override void Think()
    {
        // Si pierde al jugador de vista pasa a patrulla en alerta

        if (!_canSeePlayer)
            _enemy.SetState(new SuspiciousPatrolState(_enemy));
    }

    public override void Act()
    {
        Move(_enemy.player);
    }
}
