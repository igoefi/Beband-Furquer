using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] protected AudioSource _source;
    [SerializeField] protected AudioClip _clip;

    public void Click()
    {
        _source.Stop();
        _source.clip = _clip;
        _source.Play();
    }
}
