using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            if(!gameObject.IsDestroyed())
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
        _isReadyToAction = false;
        yield return new WaitForSeconds(_timeKD);
        _isReadyToAction = true;
    }

    public void ChangeIsEnemy() =>
        _isEnemy = !_isEnemy;

    public GameObject GetGameObject() =>
        gameObject;
}
