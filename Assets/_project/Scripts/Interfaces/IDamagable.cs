using System;
using UnityEngine;

public interface IDamagable 
{
    public bool MakeDamage(float damage);
    public Vector3 GetPosition();
    public void ChangeIsEnemy();
    public GameObject GetGameObject();
}
