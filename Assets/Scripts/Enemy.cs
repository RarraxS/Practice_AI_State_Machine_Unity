using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    [SerializeField] private float raycastHeight;
    [SerializeField] private float angleThreshold;

    [SerializeField] private List<Transform> allWayPoints;
    [SerializeField] private List<Transform> patrolWayPoints;

    [SerializeField] private int suspiciousWayPointsNumber;

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

        InvokeRepeating(nameof(PerformAction), 0.0f, 0.2f);
        SetState(new PatrolState(this));
    }

    private void PerformAction()
    {
        this._state.Perceive();
        this._state.Think();
        this._state.Act();
    }

    public void SetState(State state)
    {
        this._state = state;
    }
}
