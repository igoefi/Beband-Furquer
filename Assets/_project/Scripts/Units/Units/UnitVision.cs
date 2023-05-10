using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    [SerializeField] int _rays = 8;
    private float _distance;
    [SerializeField] float _angle = 90;

    [SerializeField] Vector3 _offset;

    private bool _isEnemy;

    private void Start()
    {
        var stats = GetComponent<UnitStats>();
        _distance = stats.GetEyeShot();
        _isEnemy = stats.IsEnemy();
        GetComponent<UnitStats>().IsMeAttacking.AddListener(SeeAttacker);
    }

    void FixedUpdate()
    {
        var enemy = GetNearEnemy(RayToScan(false));
        if (enemy == null) return;

        GetComponent<UnitLogic>().SetTarget(enemy);
        enabled = false;
    }

    #region Raycasting
    private List<IDamagable> RayToScan(bool isAttacking)
    {
        float j = 0;

        var list = new List<IDamagable>();

        for (int rayNum = 0; rayNum < _rays; rayNum++)
        {
            var sin = Mathf.Sin(j);
            var cos = Mathf.Cos(j);

            j += isAttacking ? 180 : _angle * Mathf.Deg2Rad / _rays;

            Vector3 direction = transform.TransformDirection(new Vector3(sin, 0, cos));

            var enemy = GetRaycast(direction);
            if (enemy != null) list.Add(enemy);

            if (sin != 0)
            {
                direction = transform.TransformDirection(new Vector3(-sin, 0, cos));
                enemy = GetRaycast(direction);
                if (enemy != null) list.Add(enemy);
            }
        }
        return list;
    }

    private IDamagable GetRaycast(Vector3 dir)
    {
        Vector3 pos = transform.position + _offset;
        if (Physics.Raycast(pos, dir, out RaycastHit hit, _distance))
        {
            Debug.DrawLine(pos, hit.point, Color.blue);

            var build = hit.collider.gameObject.GetComponent<BuildStats>();
            var unit = hit.collider.gameObject.GetComponent<UnitStats>();

            if (build != null && (build.IsEnemy() ^ _isEnemy))
                return build;
            else if (unit != null && (unit.IsEnemy() ^ _isEnemy))
                return unit;
        }
        else
        {
            Debug.DrawRay(pos, dir * _distance, Color.red);
        }
        return null;
    }
    #endregion

    private IDamagable GetNearEnemy(List<IDamagable> list)
    {
        if (list.Count == 0) return null;

        IDamagable needEnemy = null;

        foreach (var enemy in list)
        {
            if (needEnemy == null)
            {
                needEnemy = enemy;
                continue;
            }

            var firstDistance = Vector3.Distance(transform.position, needEnemy.GetPosition());
            var secondDistance = Vector3.Distance(transform.position, enemy.GetPosition());
            if (firstDistance > secondDistance)
                needEnemy = enemy;
        }
        return needEnemy;
    }

    private void SeeAttacker()
    {
        if (!enabled) return;
        var enemy = GetNearEnemy(RayToScan(true));
        if (enemy == null) return;

        GetComponent<UnitLogic>().SetTarget(enemy);
        enabled = false;
    }
}
