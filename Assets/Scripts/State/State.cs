using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



        //_enemy.SetState(new PatrolState(this._enemy));
        //this._canSeePlayer = 
    }
    //Perceive DE PATRULLAR STATE -> HA VISTO AL JUGADOR? (LO GUARDA EN UN BOOL)
    //Perceive DE SEGUIR STATE => SIGUE VIENDO AL JUGADOR O LO HA PERDIDO (LO GUARDA EN UN BOOL)
    //Perceive DE SUSPICIOUS STATE => HA VISTO AL JUGADOR

    public abstract void Think();
    //Think DE PATRULLAR STATE -> si ve al jugador -> debe cambiar al estado seguir 
    //Think DE SEGUIR STATE => si no ve al jugador => debe cambiar al estado suspicious
    //Think DE SUSPICIOUS STATE => ya he terminado de recorrer los elementos de la lista? => si es asi, cambiar al estado patrullar
    //                                  o si veo al jugador vuelvo al estado seguir 

    public abstract void Act();
    //Act DE PATRULLAR STATE -> enviar al navmeshagent a la posicion que le toca
    //Act DE SEGUIR STATE => enviar al navmeshagent a la posicion del jugador
    //Act DE SUSPICIOUS STATE => enviar a la poisicion que le toque


    private RaycastHit ThrowRaycast(Vector3 origin, Vector3 _direction)
    {
        Debug.DrawRay(origin, _direction, Color.black);

        if (Physics.Raycast(origin, _direction, out RaycastHit hit))
            return hit;

        return new RaycastHit();
    }

    private float CalculateAngle(Vector3 param1, Vector3 param2)
    {
        float angle = Vector3.Angle(param1, param2);

        //Debug.Log(angle);

        return angle;
    }


    private void Watch()
    {
        Vector3 start = _enemy.tr.position + new Vector3(0, _enemy.raycastHeight, 0);
        Vector3 end = _enemy.player.position + new Vector3(0, _enemy.raycastHeight, 0);

        Vector3 direction = (end - start);

        RaycastHit hit = ThrowRaycast(start, direction);

        Vector3 localForward = new Vector3(0, 0, 1);

        Debug.DrawRay(start, localForward, Color.black);


        float angle = CalculateAngle(_enemy.tr.forward, direction);


        //Debug.Log("Collided obj: " + hit.collider.name + ",player name: " + _enemy.player.name);

        if ((angle < _enemy.angleThreshold) && (hit.collider.name == _enemy.player.name))
        {
            //Debug.Log("Angulo: " + angle + " angulo limite: " + _enemy.angleThreshold);
            _canSeePlayer = true;
        }

        else
        {
            _canSeePlayer = false;
        }

        //Debug.Log(_canSeePlayer);
    }

    // Meter el Move protected
    protected void Move(Transform target)
    {
        _enemy.NavMeshAgent.SetDestination(target.position);
    }

    protected int NextWayPoint(List<Transform> _transform, int index)
    {
        Debug.Log(HasReachedDestination());
        if (//AreFloatsApproximatelyEqual(_enemy.tr.position.x, _transform[index].position.x) &&
            //AreFloatsApproximatelyEqual(_enemy.tr.position.z, _transform[index].position.z) &&
            HasReachedDestination())
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
