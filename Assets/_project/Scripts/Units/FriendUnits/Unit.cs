using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;

    [SerializeField] private UnitLogic _logic;

    public void SetPointToGo(Vector3 point) =>
        _logic.SetTarget(point);

    public void SetPointToGo(Build build) =>
        _logic.SetTarget(build);
    
    public void SetPointToGo(EnemyUnit enemy) =>
        _logic.SetTarget(enemy);

    public void Click()
    {
        _source.Stop();
        _source.clip = _clip;
        _source.Play();
    }
}
