using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStats : Stats
{
    [SerializeField] float _needPointsToBuild;

    private void Start()
    {
        _HP = _maxHP;
    }
}
