using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class State
{
    protected Enemy _enemy;
    protected bool _canSeePlayer;

    public State(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Perceive()
    {
        Watch();
    }

    public abstract void Think();

    public abstract void Act();


    private RaycastHit ThrowRaycast(Vector3 origin, Vector3 _direction)
    {
        if (Physics.Raycast(origin, _direction, out RaycastHit hit))
            return hit;

        return new RaycastHit();
    }

    private float CalculateAngle(Vector3 param1, Vector3 param2)
    {
        float angle = Vector3.Angle(param1, param2);

        return angle;
    }


    private void Watch()
    {
        Vector3 start = _enemy.Tr.position + new Vector3(0, _enemy.RaycastHeight, 0);
        Vector3 end = _enemy.Player.position + new Vector3(0, _enemy.RaycastHeight, 0);

        Vector3 direction = (end - start);

        RaycastHit hit = ThrowRaycast(start, direction);

        Vector3 localForward = new Vector3(0, 0, 1);

        float angle = CalculateAngle(_enemy.Tr.forward, direction);


        if ((angle < _enemy.AngleThreshold) && (hit.collider.name == _enemy.Player.name))
        {
            _canSeePlayer = true;
        }

        else
            _canSeePlayer = false;
    }

    protected void Move(Transform target)
    {
        _enemy.NavMeshAgent.SetDestination(target.position);
    }

    protected int NextWayPoint(List<Transform> _transform, int index)
    {
        if (HasReachedDestination())
        {
            index++;

            if (index >= _transform.Count)
            {
                index = 0;
            }
        }

        return index;
    }

    protected bool HasReachedDestination()
    {
        var navmeshAgent = _enemy.NavMeshAgent;
        if (!navmeshAgent.pathPending)
            return navmeshAgent.remainingDistance <= navmeshAgent.stoppingDistance;

        return false;
    }
}
