using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class UnitLogic : MonoBehaviour
{
    public UnityEvent<BuildStats> IsCameToBuild { get; private set; } = new();
    public UnityEvent IsStopBuilding { get; private set; } = new();

    public UnityEvent<IDamagable> IsCameToEnemy { get; private set; } = new();
    public UnityEvent IsStopAttackingEnemy { get; private set; } = new();

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private UnitStats _stats;

    private BuildStats _friendBuild;
    private IDamagable _enemy;

    private bool _isActive = false;

    private void FixedUpdate()
    {
        if (_enemy == null && _friendBuild == null) return;

        var enemy = (Stats)_enemy;
        if(enemy.IsDestroyed())
        {
            SetActiveFalse(true);
            return;
        }

        if (_enemy != null)
            _agent.SetDestination(_enemy.GetPosition());

        if (_agent.remainingDistance > _agent.stoppingDistance && _isActive)
        {
            _isActive = false;
            return;
        }

        if (_agent.remainingDistance > _agent.stoppingDistance) return;

        if (_isActive)
            return;

        if (_friendBuild != null || _enemy != null)
        {
            _isActive = true;
            if (_friendBuild != null)
                IsCameToBuild.Invoke(_friendBuild);
            else
                IsCameToEnemy.Invoke(_enemy);
        }
    }

    #region Setters
    public void SetTarget(Vector3 point)
    {
        _agent.SetDestination(point);
        _friendBuild = null;
        _enemy = null;
        SetActiveFalse(false);
    }

    public void SetTarget(BuildStats build)
    {
        _agent.SetDestination(build.transform.position);
        _enemy = null;
        _friendBuild = build;
        SetActiveFalse(false);
    }

    public void SetTarget(IDamagable enemy)
    {
        _enemy = enemy;
        _friendBuild = null;
        SetActiveFalse(false);
    }

    public void SetActiveFalse(bool isResetTargets)
    {
        IsStopBuilding.Invoke();
        IsStopAttackingEnemy.Invoke();

        if (isResetTargets)
        {
            _enemy = null;
            _friendBuild = null;
        }

        _isActive = false;
        GetComponent<UnitVision>().enabled = true;
    }

    #endregion
}
