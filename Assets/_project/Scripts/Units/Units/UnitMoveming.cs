using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMoveming : MonoBehaviour
{
    private NavMeshAgent _agent;
    private UnitLogic _logic;
    private UnitStats _stats;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _logic = GetComponent<UnitLogic>();
        _stats = GetComponent<UnitStats>();

        _logic.IsCameToTarget.AddListener(MoveToTarget);
    }

    private void MoveToTarget(Vector3 targetPos, bool isGoingToPoint)
    {
        _agent.SetDestination(targetPos);

        _agent.stoppingDistance = isGoingToPoint ? 0 : _stats.AttackDistance;
    }
}
