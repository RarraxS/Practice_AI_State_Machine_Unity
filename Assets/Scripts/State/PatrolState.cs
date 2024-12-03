using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private int indexPatrol = 0;

    public PatrolState(Enemy enemy) : base(enemy) { }

    //Think DE PATRULLAR STATE -> si ve al jugador -> debe cambiar al estado seguir 
    public override void Think()
    {
        if(this._canSeePlayer)
            this._enemy.SetState(new FollowPlayerState(this._enemy));
    }

    public override void Act()
    {
        Patrol();
    }

    private void Patrol()
    {
        Move(_enemy.patrolWayPoints[indexPatrol]);

        indexPatrol = NextWayPoint(_enemy.patrolWayPoints, indexPatrol);
    }

    private void Move(Transform target)
    {
        _enemy.agent.SetDestination(target.position);
    }

    private int NextWayPoint(List<Transform> _transform, int index)
    {
        if (_enemy.tr.position.x == _transform[index].position.x && _enemy.tr.position.z == _transform[index].position.z)
        {
            index++;

            if (index >= _transform.Count)
            {
                index = 0;
            }
        }

        return index;
    }
}
