using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCrutch : MonoBehaviour
{
    [SerializeField] Animator _anim;

    private const string _animRunNameBool = "IsRun";
    private const string _animAttackNameTrigger = "IsAttack";

    public void SetIsRunTrue() =>
        _anim.SetBool(_animRunNameBool, true);
    public void SetIsRunFalse() =>
        _anim.SetBool(_animRunNameBool, false);
    public void SetIsAttack() =>
        _anim.SetTrigger(_animAttackNameTrigger);
}
