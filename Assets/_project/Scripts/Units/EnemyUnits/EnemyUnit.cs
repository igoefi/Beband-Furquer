using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] protected AudioClip _clip;
    [SerializeField] protected AudioSource _source;
    public void Click()
    {
        _source.Stop();
        _source.clip = _clip;
        _source.Play();
    }
}
