using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _maxDistanceToTarget;

    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        if(_target != null)
        {
            if(Vector3.Distance(transform.position, _target.position) <= _maxDistanceToTarget)
            {

            }
            transform.LookAt(_target);
            transform.position += transform.forward * _speed;
        }
    }
}
