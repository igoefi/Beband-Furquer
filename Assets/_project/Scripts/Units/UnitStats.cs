using UnityEngine;
using UnityEngine.AI;

public class UnitStats : MonoBehaviour, IDamagable
{
    [SerializeField] bool _isEnemy;

    [SerializeField] private float _eyeShot;
    [SerializeField] private float _speed;

    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackDistance;

    [SerializeField] private float _HP;
    [SerializeField] private float _timeKD;

    [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        _agent.speed= _speed;
        _agent.stoppingDistance = _attackDistance;
    }

    public bool MakeDamage(float damage)
    {
        _HP -= damage;
        if (_HP <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public Vector3 GetPosition() =>
        transform.position;

    public float GetDamage() =>
        _attackDamage;
    public float GetKD() =>
        _timeKD;
    public float GetEyeShot() =>
        _eyeShot;
    public bool IsEnemy() =>
        _isEnemy;
}
