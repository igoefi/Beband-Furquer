using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUnitAttack : MonoBehaviour
{
    private UnitStats _stats;
    private UnitLogic _logic;

    private Coroutine _attackCorutine;

    private void Start()
    {
        _stats = GetComponent<UnitStats>();
        _logic = GetComponent<UnitLogic>();

        _logic.IsCameToEnemy.AddListener(StartAttack);
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
        while (true)
        {
            if (enemy == null) break;

            //transform.LookAt(enemy.GetPosition());
            if (_stats.IsReadyToAction() && enemy.MakeDamage(_stats.GetDamageToAttack())) break;

            yield return new WaitWhile(() => !_stats.IsReadyToAction());
        }
        _logic.SetActiveFalse(true);
    }
}
