using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    [SerializeField] int _rays = 8;
    [SerializeField] float _angle = 90;
    private float _distance;

    [SerializeField] Vector3 _offset;

    private UnitStats _stats;
    private void Start()
    {
        _stats = GetComponent<UnitStats>();
        _distance = _stats.EyeShot;
        GetComponent<UnitStats>().IsMeAttacking.AddListener((_) => ReactionToAttack());
    }

    void FixedUpdate()
    {
        var enemy = GetNearEnemy(RayToScan(false, false));
        if (enemy == null) return;

        GetComponent<UnitLogic>().SetTarget(enemy);
        enabled = false;
    }

    #region Raycasting
    private List<IDamagable> RayToScan(bool isAttacking, bool isSearchFriend)
    {
        float j = 0;

        var list = new List<IDamagable>();

        for (int rayNum = 0; rayNum < _rays; rayNum++)
        {
            var sin = Mathf.Sin(j);
            var cos = Mathf.Cos(j);

            j += isAttacking ? 180 : _angle * Mathf.Deg2Rad / _rays;

            Vector3 direction = transform.TransformDirection(new Vector3(sin, 0, cos));

            var enemy = !isSearchFriend ? GetRaycast(direction) : GetFriendRaycast(direction);
            if (enemy != null) list.Add(enemy);

            if (sin != 0)
            {
                direction = transform.TransformDirection(new Vector3(-sin, 0, cos));
                enemy = !isSearchFriend ? GetRaycast(direction) : GetFriendRaycast(direction);
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

            if (build != null && (build.IsEnemy() ^ _stats.IsEnemy()))
                return build;
            else if (unit != null && (unit.IsEnemy() ^ _stats.IsEnemy()))
                return unit;
        }
        else
        {
            Debug.DrawRay(pos, dir * _distance, Color.red);
        }
        return null;
    }

    private IDamagable GetFriendRaycast(Vector3 dir)
    {
        Vector3 pos = transform.position + _offset;
        if (Physics.Raycast(pos, dir, out RaycastHit hit, _distance))
        {
            Debug.DrawLine(pos, hit.point, Color.blue);

            var unit = hit.collider.gameObject.GetComponent<UnitStats>();

            if (unit != null && (!unit.IsEnemy() ^ _stats.IsEnemy()))
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

    public void ReactionToAttack()
    {
        if (!enabled) return;

        SayFriendsAboutEnemy(SeeAttacker());
        enabled = false;
    }

    private IDamagable SeeAttacker()
    {
        var enemy = GetNearEnemy(RayToScan(true, false));
        if (enemy == null) return null;

        GetComponent<UnitLogic>().SetTarget(enemy);
        return enemy;
    }

    private void SayFriendsAboutEnemy(IDamagable enemy)
    {
        if (enemy == null) return;
        var friends = RayToScan(true, true);

        foreach (var frind in friends)
            frind.GetGameObject().GetComponent<UnitVision>().ReactionToAttack();
    }
}
