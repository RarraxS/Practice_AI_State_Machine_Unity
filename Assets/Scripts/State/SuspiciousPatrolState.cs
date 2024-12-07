using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuspiciousPatrolState : State
{
    private int indexSuspicious = 0;
    private List<Transform> suspiciousWayPoints;

    public SuspiciousPatrolState(Enemy enemy) : base(enemy) 
    {
        suspiciousWayPoints = GetSuspiciousPointFrom(_enemy.SuspiciousWayPointsNumber);
    }

    public override void Think()
    {
        if (_canSeePlayer)
            _enemy.SetState(new FollowPlayerState(_enemy));

        else if (indexSuspicious > (suspiciousWayPoints.Count - 1))
            _enemy.SetState(new PatrolState(_enemy));
    }

    public override void Act()
    {
        SuspiciousPatrol();
    }

    private void SuspiciousPatrol()
    {
        Move(suspiciousWayPoints[indexSuspicious]);

        indexSuspicious = NextWayPoint(suspiciousWayPoints, indexSuspicious);
    }

    private List<Transform> GetSuspiciousPointFrom(int index)
    {
        return _enemy.AllWayPoints
            .OrderBy(waypoint => Vector3.Distance(_enemy.Tr.position, waypoint.position))
            .Take(index)
            .ToList();
    }
}
