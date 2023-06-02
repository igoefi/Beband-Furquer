using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _maxDistanceToTarget;
    [SerializeField] float _explosiomRadius;
    [SerializeField] private ParticleSystem _explosion;

    private Vector3 _target;
    private float _damage;
    private bool _isEnemy;

    public void SetTarget(Vector3 target, float damage, bool isEnemy)
    {
        _target = target;
        _damage = damage;
        _isEnemy = isEnemy;
    }

    private void FixedUpdate()
    {
        if(_target != null)
        {
            transform.LookAt(_target);
            transform.position += transform.forward * _speed;

            if (Vector3.Distance(transform.position, _target) <= _maxDistanceToTarget)
                Attack();
        }
    }

    private void Attack()
    {
        _explosion.gameObject.SetActive(true);
        _explosion.transform.parent = transform.parent;
        _explosion.Play();

        var raycasts = Physics.SphereCastAll(transform.position, _explosiomRadius, transform.position);

        foreach (var cast in raycasts)
            if (cast.collider.TryGetComponent(out Stats unit))
                if (unit.IsEnemy() ^ _isEnemy)
                    unit.MakeDamage(_damage);

        Destroy(gameObject);
    }
}
