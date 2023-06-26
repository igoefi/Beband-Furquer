using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUnitAttack : MonoBehaviour
{
    private UnitStats _stats;
    private UnitLogic _logic;

    [SerializeField] Transform _transformToLookAt;

    private Coroutine _attackCorutine;

    private void Start()
    {
        _stats = GetComponent<UnitStats>();
        _logic = GetComponent<UnitLogic>();

        _logic.IsStartAttackingEnemy.AddListener(StartAttack);
        _logic.IsStopAttackingEnemy.AddListener(EndAttack);
    }

    private void StartAttack(IDamagable enemy) =>
        _attackCorutine = StartCoroutine(Attack(enemy));

    private void EndAttack() 
    { 
        if(_attackCorutine != null)
            StopCoroutine(_attackCorutine);
    }

    private IEnumerator Attack(IDamagable enemy)
    {
        yield return new WaitWhile(() => !_stats.IsReadyToAction());

        try
        {
            if (_transformToLookAt != null)
                _transformToLookAt.LookAt(enemy.GetPosition());

            enemy.MakeDamage(_stats.GetDamageToAttack());
        }
        catch { }

        _logic.SetActiveFalse(true);
    }
}
