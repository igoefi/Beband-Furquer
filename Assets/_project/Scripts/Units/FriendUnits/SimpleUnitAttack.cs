using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUnitAttack : MonoBehaviour
{
    [SerializeField] UnitStats _stats;
    [SerializeField] UnitLogic _logic;

    private Coroutine _attackCorutine;

    private void Start()
    {
        _logic.IsCameToEnemy.AddListener(StartAttack);
        _logic.IsStopAttackingEnemy.AddListener(EndAttack);
    }

    private void StartAttack(EnemyUnit enemy) =>
        _attackCorutine = StartCoroutine(Attack(enemy.GetComponent<UnitStats>()));

    private void EndAttack() =>
        StopCoroutine(_attackCorutine);

    private IEnumerator Attack(IDamagable enemy)
    {
        while (true)
        {
            if (enemy == null) break;
                
            if(enemy.MakeDamage(_stats.GetDamage())) break;
            yield return new WaitForSeconds(_stats.GetKD());
        }
    }
}
