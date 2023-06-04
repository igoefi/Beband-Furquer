using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketAttack : MonoBehaviour
{
    [SerializeField] List<Transform> _placesForRockets;
    [SerializeField] Rocket _rocketPrefab;

    [SerializeField] Transform _transformToLookAt;

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

        if (_transformToLookAt != null)
        {
            _transformToLookAt.LookAt(enemy.GetPosition());
        }
        if (enemy != null)
            foreach(var place in _placesForRockets)
            {
                var obj = Instantiate(_rocketPrefab.gameObject, place.position, transform.rotation, transform.parent)
                    .GetComponent<Rocket>();
                obj.SetTarget(enemy.GetPosition(), _stats.GetDamageToAttack(), _stats.IsEnemy());
            }

        _logic.SetActiveFalse(true);
    }
}
