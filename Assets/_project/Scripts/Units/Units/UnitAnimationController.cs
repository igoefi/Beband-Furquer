using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private Animator _anim;

    private const string _animRunNameBool = "IsRun";
    private const string _animBuildBool = "IsBuild";
    private const string _animAttackNameTrigger = "IsAttack";

    private void Start()
    {
        _anim = GetComponent<Animator>();
        var logic = GetComponent<UnitLogic>();

        logic.IsCameToTarget.AddListener((_, _) => _anim.SetBool(_animRunNameBool, true));
        logic.IsReachedToTarget.AddListener(() => _anim.SetBool(_animRunNameBool, false));
        logic.IsAttackEnemy.AddListener(() => _anim.SetTrigger(_animAttackNameTrigger));
        logic.IsStartBuilding.AddListener((_) => _anim.SetBool(_animBuildBool, true));
        logic.IsStopBuilding.AddListener(() => _anim.SetBool(_animBuildBool, false));
    }
}
