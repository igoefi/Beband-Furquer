using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour, IDamagable
{
    public UnityEvent IsMeAttacking { get; private set; } = new();

    [SerializeField] protected bool _isEnemy;

    [SerializeField] protected float _HP;
    [SerializeField] protected float _timeKD;
    protected bool _isReadyToAction = true;

    public bool IsEnemy() =>
        _isEnemy;

    public bool MakeDamage(float damage)
    {
        _HP -= damage;
        if (_HP <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        IsMeAttacking.Invoke();
        return false;
    }

    public Vector3 GetPosition() =>
        transform.position;

    public bool IsReadyToAction() =>
        _isReadyToAction;

    protected IEnumerator ResetReady()
    {
        Debug.Log("start");
        _isReadyToAction = false;
        yield return new WaitForSeconds(_timeKD);
        Debug.Log("end");
        _isReadyToAction = true;
    }

}
