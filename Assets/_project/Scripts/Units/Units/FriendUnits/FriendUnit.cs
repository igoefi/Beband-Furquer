using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendUnit : Unit
{
    [SerializeField] private UnitLogic _logic;

    public void SetPointToGo(Vector3 point) =>
        _logic.SetTarget(point);

    public void SetPointToGo(BuildStats build) =>
        _logic.SetTarget(build);

    public void SetPointToGo(IDamagable enemy) =>
        _logic.SetTarget(enemy);
}
