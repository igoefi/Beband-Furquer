using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamagable
{
    [SerializeField] protected bool _isEnemy;

    [SerializeField] protected float _HP;
    [SerializeField] protected float _timeKD;

    public bool IsEnemy() =>
        _isEnemy;

    public bool MakeDamage(float damage)
    {
        _HP -= damage;
        Debug.Log("HP " + _HP);
        if (_HP <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public Vector3 GetPosition() =>
        transform.position;

}
