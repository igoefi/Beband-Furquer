using UnityEngine;
using UnityEngine.AI;

public class UnitStats : Stats
{
    [SerializeField] private float _eyeShot;
    [SerializeField] private float _speed;

    [SerializeField] private float _minAttackDamage;
    [SerializeField] private float _maxAttackDamage;
    [SerializeField] private float _attackDistance;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _HP = _maxHP;
    }

    public float GetDamageToAttack()
    {
        StartCoroutine(ResetReady());
        return (float)System.Math.Round(Random.Range(_minAttackDamage, _maxAttackDamage), 1);
    }
    public float EyeShot =>
        _eyeShot;
    public float AttackDistance =>
        _attackDistance;
}
