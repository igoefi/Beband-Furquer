using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour, IDamagable
{
    public UnityEvent<float> IsMeAttacking { get; private set; } = new();

    [SerializeField] protected bool _isEnemy;

    [SerializeField] protected float _maxHP;
    protected float _HP;

    [SerializeField] protected float _minTimeKD;
    [SerializeField] protected float _maxTimeKD;
    protected bool _isReadyToAction = true;

    public bool IsEnemy() =>
        _isEnemy;

    public bool MakeDamage(float damage)
    {
        _HP -= damage;
        if (_HP <= 0)
        {
            if(!gameObject.IsDestroyed())
                Destroy(gameObject);
            return true;
        }
        IsMeAttacking.Invoke(damage);
        return false;
    }

    public Vector3 GetPosition() =>
        transform.position;

    public bool IsReadyToAction() =>
        _isReadyToAction;

    protected IEnumerator ResetReady()
    {
        _isReadyToAction = false;
        yield return new WaitForSeconds(Random.Range(_minTimeKD, _maxTimeKD));
        _isReadyToAction = true;
    }

    public void ChangeIsEnemy() =>
        _isEnemy = !_isEnemy;

    public GameObject GetGameObject() =>
        gameObject;
}
