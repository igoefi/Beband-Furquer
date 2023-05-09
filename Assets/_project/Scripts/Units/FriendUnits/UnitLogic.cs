using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class UnitLogic : MonoBehaviour
{
    public UnityEvent<Build> IsCameToBuild { get; private set; } = new();
    public UnityEvent IsStopBuilding { get; private set; } = new();

    public UnityEvent<EnemyUnit> IsCameToEnemy { get; private set; } = new();
    public UnityEvent IsStopAttackingEnemy { get; private set; } = new();


    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private UnitStats _stats;

    private Build _build;
    private EnemyUnit _enemy;

    private bool _isActive = false;

    private void Update()
    {
        if (_agent.remainingDistance > _agent.stoppingDistance 
            || _agent.remainingDistance == 0 
            || _isActive) return;

        if (_build != null || _enemy != null)
        {
            _isActive = true;  
            if(_build != null)
                IsCameToBuild.Invoke(_build);
            else
                IsCameToEnemy.Invoke(_enemy);
        }
    }

    #region SetTarget
    public void SetTarget(Vector3 enemy)
    {
        _agent.SetDestination(enemy);
        _build = null;
        _enemy = null;
        SetActiveFalse();
    }

    public void SetTarget(Build build)
    {
        _agent.SetDestination(build.transform.position);
        _enemy = null;
        _build = build;
        _isActive = false;
        SetActiveFalse();
    }

    public void SetTarget(EnemyUnit enemy)
    {
        _agent.SetDestination(enemy.transform.position);
        _enemy = enemy;
        _build = null;
        _isActive = false;
        SetActiveFalse();
    }

    private void SetActiveFalse()
    {
        if (_isActive)
        {
            IsStopBuilding.Invoke();
            IsStopAttackingEnemy.Invoke();
            _isActive = false;
        }
    }
    #endregion
}
