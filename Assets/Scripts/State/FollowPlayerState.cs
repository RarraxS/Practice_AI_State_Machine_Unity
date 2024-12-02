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
    }

    public override void Act()
    {
        Vector3 start = _enemy.tr.position + new Vector3(0, _enemy.raycastHeight, 0);
        Vector3 end = _enemy.player.position + new Vector3(0, _enemy.raycastHeight, 0);

        Vector3 direction = (end - start);

        RaycastHit hit = ThrowRaycast(start, direction);


        //Vector3 localForward = transform.InverseTransformDirection(transform.forward);
        Vector3 localForward = new Vector3(0, 0, 1);

        Debug.DrawRay(start, localForward, Color.black);


        float angle = CalculateAngle(_enemy.tr.forward, direction);

        if (angle < _enemy.angleThreshold)
        {
            Debug.Log("Angulo: " + angle + " angulo limite: " + _enemy.angleThreshold);
            Move(player);
        }
    }


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

    private void Move(Transform target)
    {
        _enemy.agent.SetDestination(target.position);
    }
}
