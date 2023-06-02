using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBySeconds : MonoBehaviour
{
    [SerializeField] float _timeToDestroy;

    private void OnEnable() =>
        StartCoroutine(Destroy());

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }
}
