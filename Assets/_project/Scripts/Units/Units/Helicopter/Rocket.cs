using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _maxDistanceToTarget;
    [SerializeField] float _explosionRadius;    
    [SerializeField] float _timeFirstForwardMoveming;    
    [SerializeField] ParticleSystem _explosionParticle;

    private Vector3 _target;
    private float _damage;
    private bool _isEnemy;
    private bool _isMovemingToTarget = false;

    public void SetTarget(Vector3 target, float damage, bool isEnemy)
    {
        _target = target;
        _damage = damage;
        _isEnemy = isEnemy;
        StartCoroutine(FirstMovement());
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * _speed;
        if (_isMovemingToTarget)
        {
            transform.LookAt(_target);

            if (Vector3.Distance(transform.position, _target) <= _maxDistanceToTarget)
                Attack();
        }
    }

    private void Attack()
    {
        _explosionParticle.gameObject.SetActive(true);
        _explosionParticle.transform.parent = transform.parent;
        _explosionParticle.Play();

        var raycasts = Physics.SphereCastAll(transform.position, _explosionRadius, transform.position);

        foreach (var cast in raycasts)
            if (cast.collider.TryGetComponent(out Stats unit))
                if (unit.IsEnemy() ^ _isEnemy) 
                    unit.MakeDamage(_damage);

        Destroy(gameObject);
    }

    private IEnumerator FirstMovement()
    {
        yield return new WaitForSeconds(_timeFirstForwardMoveming);

        _isMovemingToTarget = true;
    }
}
