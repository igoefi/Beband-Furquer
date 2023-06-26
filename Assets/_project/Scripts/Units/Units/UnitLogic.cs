using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class UnitLogic : MonoBehaviour
{
    public UnityEvent<Vector3, bool> IsCameToTarget { get; private set; } = new();

    public UnityEvent<BuildStats> IsStartBuilding { get; private set; } = new();
    public UnityEvent IsStopBuilding { get; private set; } = new();

    public UnityEvent<IDamagable> IsStartAttackingEnemy { get; private set; } = new();
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
        if (!_isActive && _enemy.IsUnityNull() && _friendBuild.IsUnityNull())
        {
            SetVisionEnabled(true);
            return;
        }

        //var enemy = (Stats)_enemy;
        //if (enemy.IsDestroyed())
        //{
        //    SetActiveFalse(true);
        //    IsStopAttackingEnemy.Invoke();
        //    _anim.SetBool(_animRunNameBool, false);
        //    _anim.SetTrigger(_animEndAttackNameTrigger);
        //    return;
        //}

        if (_agent.remainingDistance > _agent.stoppingDistance)
        {
            if(_isActive)
                SetActiveFalse(false);

            if (_friendBuild != null)
                IsCameToTarget.Invoke(_friendBuild.GetPosition(), false);
            else
                IsCameToTarget.Invoke(_enemy.GetPosition(), false);
        }
        else
        {
            if (_isActive) return;
            _isActive = true;

            _anim.SetBool(_animRunNameBool, false);
            _anim.SetTrigger(_animAttackNameTrigger);

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
        GetComponent<UnitVision>().enabled = false;
        _anim.SetBool(_animRunNameBool, true);
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
        IsStopBuilding.Invoke();
        IsStopAttackingEnemy.Invoke();
        _anim.SetTrigger(_animEndAttackNameTrigger);
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
