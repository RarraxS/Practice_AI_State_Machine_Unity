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




        //float angle = Vector3.Angle(tr.forward, direction);

        //Debug.Log(angle);


        //if (hit.collider.name == player.name && angle < angleThreshold)
        //{
        //    Move(player);
        //}

        if (angle < _enemy.angleThreshold)
        {
            Debug.Log("Angulo: " + angle + " angulo limite: " + _enemy.angleThreshold);
            _canSeePlayer = true;
        }

        else
        {
            _canSeePlayer = false;


            //agent.Stop();
        }
    }
}
