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

        _logic.IsStartAttackingEnemy.AddListener(StartAttack);
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

        try
        {
            if (_transformToLookAt != null)
                _transformToLookAt.LookAt(enemy.GetPosition());

            foreach (var place in _placesForRockets)
            {
                var obj = Instantiate(_rocketPrefab.gameObject, place.position, place.rotation, transform.parent)
                    .GetComponent<Rocket>();
                obj.SetTarget(enemy.GetPosition(), _stats.GetDamageToAttack(), _stats.IsEnemy());
            }
        }
        catch { }

        _logic.SetActiveFalse(true);
    }
}
