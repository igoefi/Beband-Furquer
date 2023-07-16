using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByEndParticle : MonoBehaviour
{
    private void OnEnable() =>
        StartCoroutine(Destroy());

    private IEnumerator Destroy()
    {
        yield return new WaitWhile(() => GetComponent<ParticleSystem>().isPlaying);
        Destroy(gameObject);
    }
}
