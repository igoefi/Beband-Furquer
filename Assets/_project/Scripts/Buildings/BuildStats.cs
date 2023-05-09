using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStats : MonoBehaviour, IDamagable
{
    [SerializeField] bool _isEnemy;
    [SerializeField] float _HP;
    [SerializeField] float _needPointToBuild;

    public bool MakeDamage(float damage)
    {
        _HP *= damage;
        Debug.Log(_HP);
        if (_HP <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public Vector3 GetPosition() =>
        transform.position;

    public bool IsEnemy() =>
        _isEnemy;
}
