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

    private NavMeshAgent _agent;
    private UnitStats _stats;
    private Animator _anim;

    private BuildStats _friendBuild;
    private IDamagable _enemy;

    private bool _isActive = false;

    private const string _animRunNameBool = "IsRun";
    private const string _animAttackNameTrigger = "IsAttack";
    private const string _animEndAttackNameTrigger = "IsEndAttack";

    private void Start()
    {
        _stats = GetComponent<UnitStats>();
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_enemy == null && _friendBuild == null && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            GetComponent<UnitVision>().enabled = true;
            _anim.SetBool(_animRunNameBool, false);
            return;
        }

        var enemy = (Stats)_enemy;
        if (enemy.IsDestroyed())
        {
            SetActiveFalse(true);
            _anim.SetBool(_animRunNameBool, false);
            _anim.SetTrigger(_animEndAttackNameTrigger);
            return;
        }

        if (_enemy != null)
        {
            _agent.SetDestination(_enemy.GetPosition());
        }

        if (_agent.remainingDistance > _agent.stoppingDistance && _isActive)
        {
            _isActive = false;
            _anim.SetBool(_animRunNameBool, true);
            return;
        }

        if (_agent.remainingDistance > _agent.stoppingDistance) return;

        if (_isActive)
            return;

        if (_friendBuild != null || _enemy != null)
        {
            _isActive = true;

            _anim.SetTrigger(_animAttackNameTrigger);
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
        _agent.stoppingDistance = 0;
        _friendBuild = null;
        _enemy = null;
        SetActiveFalse(true);
        GetComponent<UnitVision>().enabled = false;
        _anim.SetBool(_animRunNameBool, true);
    }

    public void SetTarget(BuildStats build)
    {
        _agent.SetDestination(build.transform.position);
        _agent.stoppingDistance = _stats.GetAttackDistance();
        _enemy = null;
        _friendBuild = build;
        SetActiveFalse(false);
    }

    public void SetTarget(IDamagable enemy)
    {
        _agent.SetDestination(enemy.GetPosition());
        _agent.stoppingDistance = _stats.GetAttackDistance();
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
            _anim.SetTrigger(_animEndAttackNameTrigger);
        }
        else
        {
            GetComponent<UnitVision>().enabled = false;
            _anim.SetBool(_animRunNameBool, true);
        }
        _isActive = false;
    }

    #endregion
}
