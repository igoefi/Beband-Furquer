using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuroAttack : MonoBehaviour
{
    [SerializeField] Transform _transformFromLookAt;

    private Coroutine _attackCorutine;
    private UnitStats _stats;
    private UnitLogic _logic;

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
        if (_attackCorutine != null)
            StopCoroutine(_attackCorutine);
    }

    private IEnumerator Attack(IDamagable enemy)
    {
        yield return new WaitWhile(() => !_stats.IsReadyToAction());

        _stats.GetDamageToAttack();

        if (_transformFromLookAt != null)
            _transformFromLookAt.LookAt(enemy.GetPosition());

        enemy?.ChangeIsEnemy();

        _logic.SetActiveFalse(true);
    }
}
