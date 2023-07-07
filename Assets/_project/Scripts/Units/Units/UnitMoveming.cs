using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMoveming : MonoBehaviour
{
    private NavMeshAgent _agent;
    private UnitLogic _logic;
    private UnitStats _stats;

    private Animator _anim;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stats = GetComponent<UnitStats>();
        _anim = GetComponent<Animator>();

        GetComponent<UnitLogic>().IsCameToTarget.AddListener(MoveToTarget);
    }

    private void MoveToTarget(Vector3 targetPos, bool isGoingToPoint)
    {
        _agent.SetDestination(targetPos);

        _agent.stoppingDistance = isGoingToPoint ? 0.5f : _stats.AttackDistance;
    }
}
