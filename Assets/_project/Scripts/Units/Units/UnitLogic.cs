using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class UnitLogic : MonoBehaviour
{
    public UnityEvent<Vector3, bool> IsCameToTarget { get; private set; } = new();
    public UnityEvent IsReachedToTarget { get; private set; } = new();

    public UnityEvent<BuildStats> IsStartBuilding { get; private set; } = new();
    public UnityEvent IsStopBuilding { get; private set; } = new();

    public UnityEvent<IDamagable> IsStartAttackingEnemy { get; private set; } = new();
    public UnityEvent IsAttackEnemy { get; private set; } = new();
    public UnityEvent IsStopAttackingEnemy { get; private set; } = new();

    private NavMeshAgent _agent;
    private UnitStats _stats;

    private BuildStats _friendBuild;
    private IDamagable _enemy;

    private bool _isActive = false;
    private bool _isCameToPoint = false;

    private void Start()
    {
        _stats = GetComponent<UnitStats>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (!_isActive && !_isCameToPoint && _enemy.IsUnityNull() && _friendBuild.IsUnityNull())
        {
            SetVisionEnabled(true);
            return;
        }

        if (_agent.remainingDistance > _agent.stoppingDistance)
        {
            if (_isActive)
                SetActiveFalse(false);

            if (_friendBuild != null)
                IsCameToTarget.Invoke(_friendBuild.GetPosition(), false);
            else if (_enemy != null)
                IsCameToTarget.Invoke(_enemy.GetPosition(), false);
        }
        else
        {
            if (_isActive) return;
            _isActive = true;

            IsReachedToTarget.Invoke();
            if (_friendBuild != null)
                IsStartBuilding.Invoke(_friendBuild);
            else
                IsStartAttackingEnemy.Invoke(_enemy);
        }
    }

    #region Setters
    public void SetTarget(Vector3 point)
    {
        IsCameToTarget.Invoke(point, true);
        SetActiveFalse(true);
        _isCameToPoint = true;
        GetComponent<UnitVision>().enabled = false;
    }

    public void SetTarget(BuildStats build)
    {
        _agent.SetDestination(build.transform.position);
        _agent.stoppingDistance = _stats.AttackDistance;
        _enemy = null;
        _friendBuild = build;
        SetActiveFalse(false);
    }

    public void SetTarget(IDamagable enemy)
    {
        _agent.SetDestination(enemy.GetPosition());
        _agent.stoppingDistance = _stats.AttackDistance;
        _enemy = enemy;
        _friendBuild = null;
        SetActiveFalse(false);
    }

    public void SetActiveFalse(bool isResetTargets)
    {
        _isActive = false;
        _isCameToPoint = false;
        IsStopBuilding.Invoke();
        IsStopAttackingEnemy.Invoke();
        if (isResetTargets)
        {
            _enemy = null;
            _friendBuild = null;
        }
    }

    public void SetVisionEnabled(bool isEnabled) =>
        GetComponent<UnitVision>().enabled = isEnabled;

    #endregion
}
