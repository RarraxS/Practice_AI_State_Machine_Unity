using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //--------------------------------------------------------------------
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    //--------------------------------------------------------------------

    //--------------------------------------------------------------------
    [SerializeField] private float raycastHeight;

    [SerializeField] private float angleThreshold;
    //--------------------------------------------------------------------

    //--------------------------------------------------------------------
    [SerializeField] private List<Transform> patrolWayPoints;
    //--------------------------------------------------------------------

    //--------------------------------------------------------------------
    [SerializeField] private List<Transform> allWayPoints;
    [SerializeField] private int suspiciousWayPointsNumber;
    //--------------------------------------------------------------------

    private Transform tr;
    private State _state;


    public NavMeshAgent NavMeshAgent => agent;
    public Transform Player => player;
    public Transform Tr => tr;
    public float RaycastHeight => raycastHeight;
    public float AngleThreshold => angleThreshold;
    public List<Transform> PatrolWayPoints => patrolWayPoints;
    public List<Transform> AllWayPoints => allWayPoints;
    public int SuspiciousWayPointsNumber => suspiciousWayPointsNumber;


    private void Start()
    {
        tr = GetComponent<Transform>();

        //suspiciousWayPoints = GetSuspiciousPointFrom(suspiciousWayPointsNumber);
        InvokeRepeating(nameof(PerformAction), 0.0f, 0.2f);
        SetState(new PatrolState(this));
    }

    private void PerformAction()
    {
        this._state.Perceive();
        this._state.Think();
        this._state.Act();

        Debug.Log(_state);
    }

    public void SetState(State state)
    {
        this._state = state;
    }

    private void Move(Transform target)
    {
        agent.SetDestination(target.position);
    }


    //private RaycastHit ThrowRaycast(Vector3 origin, Vector3 _direction)
    //{
    //    Debug.DrawRay(origin, _direction, Color.black);

    //    if (Physics.Raycast(origin, _direction, out RaycastHit hit))         
    //        return hit;

    //    return new RaycastHit();
    //}

    //private float CalculateAngle(Vector3 param1, Vector3 param2)
    //{
    //    float angle = Vector3.Angle(param1, param2);

    //    //Debug.Log(angle);

    //    return angle;
    //}


    //private void Watch()
    //{
    //    Vector3 start = tr.position + new Vector3(0, raycastHeight, 0);
    //    Vector3 end = player.position + new Vector3(0, raycastHeight, 0);

    //    Vector3 direction = (end - start);

    //    RaycastHit hit = ThrowRaycast(start, direction);


    //    //Vector3 localForward = transform.InverseTransformDirection(transform.forward);
    //    Vector3 localForward = new Vector3(0, 0, 1);

    //    Debug.DrawRay(start, localForward, Color.black);


    //    float angle = CalculateAngle(tr.forward, direction);




    //    //float angle = Vector3.Angle(tr.forward, direction);

    //    //Debug.Log(angle);


    //    //if (hit.collider.name == player.name && angle < angleThreshold)
    //    //{
    //    //    Move(player);
    //    //}

    //    if (angle < angleThreshold)
    //    {
    //        Debug.Log("Angulo: " + angle + " angulo limite: " + angleThreshold);
    //        Move(player);
    //    }

    //    else
    //    {
    //        //agent.Stop();
    //    }




    //    ////Comprueba el angulo del raycast
    //    //float angle = Vector3.Angle(rayDirection, hit.point - transform.position);

    //    //// Comprobamos si el ángulo está dentro del rango de +-45º
    //    //if (angle <= angleThreshold)
    //    //{
    //    //    // Si está dentro del ángulo permitido, hacemos algo
    //    //    Debug.Log("El Raycast está dentro del ángulo permitido.");
    //    //    // Aquí puedes agregar lo que quieres hacer cuando esté dentro del ángulo
    //    //}
    //}

    //private void SuspiciousPatrol()
    //{
    //    Move(suspiciousWayPoints[0]);

    //    indexSuspicious = NextWayPoint(suspiciousWayPoints, indexSuspicious);
    //    if (indexSuspicious >= 1)
    //    {
    //        suspiciousWayPoints.RemoveAt(0);
    //        indexSuspicious = 0;
    //    }
    //}

    //private void Patrol()
    //{
    //    Move(patrolWayPoints[indexPatrol]);

    //    indexPatrol = NextWayPoint(patrolWayPoints, indexPatrol);
    //}

    private List<Transform> GetSuspiciousPointFrom(int index)
    {
        return allWayPoints
            .OrderBy(waypoint => Vector3.Distance(tr.position, waypoint.position))
            .Take(index)
            .ToList();
    }

    private int NextWayPoint(List<Transform> _transform, int index)
    {
        if (tr.position.x == _transform[index].position.x && tr.position.z == _transform[index].position.z)
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
