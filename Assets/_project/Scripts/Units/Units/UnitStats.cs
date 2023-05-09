using UnityEngine;
using UnityEngine.AI;

public class UnitStats : Stats
{
    [SerializeField] private float _eyeShot;
    [SerializeField] private float _speed;

    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackDistance;

    [SerializeField] private NavMeshAgent _agent;

    private void Start() =>
        _agent.speed = _speed;

    public float GetDamage() =>
        _attackDamage;
    public float GetKD() =>
        _timeKD;
    public float GetEyeShot() =>
        _eyeShot;
    public float GetAttackDistance() =>
        _attackDistance;
}
