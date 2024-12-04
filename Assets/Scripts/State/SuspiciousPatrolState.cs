using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuspiciousPatrolState : State
{
    private bool activated = false;

    private int indexSuspicious = 0;
    private List<Transform> suspiciousWayPoints;

    public SuspiciousPatrolState(Enemy enemy) : base(enemy) { }

    //Think DE PATRULLAR SOSPECHOSAMENTE STATE -> si ve al jugador -> debe cambiar al estado seguir
    //                                         -> si se acaban los waypoints -> debe cambiar al estado patrulla
    public override void Think()
    {
        if (_canSeePlayer)
            _enemy.SetState(new FollowPlayerState(_enemy));

        else if (suspiciousWayPoints.Count <= 0)
            _enemy.SetState(new PatrolState(_enemy));
    }

    public override void Act()
    {
        if (!activated)
            suspiciousWayPoints = GetSuspiciousPointFrom(_enemy.suspiciousWayPointsNumber);

        //for (int i = 0; i <= suspiciousWayPoints.Count; i++)
        //    Debug.Log(suspiciousWayPoints[i]);
        
        //Debug.Log("WP 1: " + suspiciousWayPoints[0].name);
        //Debug.Log("WP 2: " + suspiciousWayPoints[1].name);
        //Debug.Log("WP 3: " + suspiciousWayPoints[2].name);
        //Debug.Log("WP 4: " + suspiciousWayPoints[3].name);


        Debug.Log("Index: " + indexSuspicious);


        SuspiciousPatrol();
    }

    private void SuspiciousPatrol()
    {
        Move(suspiciousWayPoints[indexSuspicious]);

        indexSuspicious = NextWayPoint(suspiciousWayPoints, indexSuspicious);
    }

    private List<Transform> GetSuspiciousPointFrom(int index)
    {
        return _enemy.allWayPoints
            .OrderBy(waypoint => Vector3.Distance(_enemy.tr.position, waypoint.position))
            .Take(index)
            .ToList();
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

    private void Move(Transform target)
    {
        _enemy.agent.SetDestination(target.position);
    }
}
