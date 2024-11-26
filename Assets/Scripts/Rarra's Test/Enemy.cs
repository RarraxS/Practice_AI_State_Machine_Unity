using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    //--------------------------------------------------------------------
    [SerializeField] private float raycastHeight;

    [SerializeField] private float angleThreshold;
    //--------------------------------------------------------------------



    //--------------------------------------------------------------------
    [SerializeField] private List<Transform> patrolWayPoints;
    private int indexPatrol = 0;
    //--------------------------------------------------------------------


    //--------------------------------------------------------------------
    [SerializeField] private List<Transform> allWayPoints;
    [SerializeField] private int suspiciousWayPointsNumber;
    private List<Transform> suspiciousWayPoints;
    private int indexSuspicious = 0;
    //--------------------------------------------------------------------

    private Transform tr;


    private void Start()
    {
        tr = GetComponent<Transform>();

        suspiciousWayPoints = SetList(suspiciousWayPoints, suspiciousWayPointsNumber);
    }

    private void Update()
    {
        Watch();

        //Move(player);

        //Patrol();

        //SuspiciousPatrol();
    }

    private void Move(Transform target)
    {
        agent.SetDestination(target.position);
    }


    private RaycastHit ThrowRaycast(Vector3 origin, Vector3 _direction)
    {
        Debug.DrawRay(origin, _direction, Color.black);

        if (Physics.Raycast(origin, _direction, out RaycastHit hit))
        {
            Debug.Log(hit.collider.name);
            return hit;
        }

        return new RaycastHit();
    }

    private float CalculateAngle(Vector3 param1, Vector3 param2)
    {
        float angle = Vector3.Angle(param1, param2);

        Debug.Log(angle);

        return angle;
    }


    private void Watch()
    {
        Vector3 start = tr.position + new Vector3(0, raycastHeight, 0);
        Vector3 end = player.position + new Vector3(0, raycastHeight, 0);

        Vector3 direction = (end - start);

        RaycastHit hit = ThrowRaycast(start, direction);


        //Vector3 localForward = transform.InverseTransformDirection(transform.forward);
        Vector3 localForward = new Vector3(0, 0, 1);

        Debug.DrawRay(start, localForward, Color.black);


        float angle = CalculateAngle(tr.forward, direction);




        //float angle = Vector3.Angle(tr.forward, direction);

        //Debug.Log(angle);


        //if (hit.collider.name == player.name && angle < angleThreshold)
        //{
        //    Move(player);
        //}

        if (angle < angleThreshold)
        {
            Move(player);
        }




        ////Comprueba el angulo del raycast
        //float angle = Vector3.Angle(rayDirection, hit.point - transform.position);

        //// Comprobamos si el ángulo está dentro del rango de +-45º
        //if (angle <= angleThreshold)
        //{
        //    // Si está dentro del ángulo permitido, hacemos algo
        //    Debug.Log("El Raycast está dentro del ángulo permitido.");
        //    // Aquí puedes agregar lo que quieres hacer cuando esté dentro del ángulo
        //}
    }

    private void SuspiciousPatrol()
    {
        Move(suspiciousWayPoints[0]);

        indexSuspicious = NextWayPoint(suspiciousWayPoints, indexSuspicious);
        if (indexSuspicious >= 1)
        {
            suspiciousWayPoints = RemoveWayPoint(suspiciousWayPoints, 0);
            indexSuspicious = 0;
        }
    }

    private void Patrol()
    {
        Move(patrolWayPoints[indexPatrol]);

        indexPatrol = NextWayPoint(patrolWayPoints, indexPatrol);
    }

    private List<Transform> SetList(List<Transform> _list, int index)
    {
        _list = allWayPoints
            .OrderBy(waypoint => Vector3.Distance(tr.position, waypoint.position))
            .Take(index)
            .ToList();

        return _list;
    }

    private int NextWayPoint(List<Transform> _transform, int index)
    {
        if (tr.position.x == _transform[indexPatrol].position.x && tr.position.z == _transform[indexPatrol].position.z)
        {
            index++;

            if (index >= _transform.Count)
            {
                index = 0;
            }
        }

        return index;
    }

    private List<Transform> RemoveWayPoint(List<Transform> _list, int index)
    {
        _list.RemoveAt(index);

        return _list;
    }
}
