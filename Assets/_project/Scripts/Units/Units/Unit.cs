using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] protected AudioSource _source;
    [SerializeField] protected AudioClip _clip;

    private UnitLogic _logic;
    private void Start()
    {
        _logic = GetComponent<UnitLogic>();
    }

    public void Click()
    {
        _source.Stop();
        _source.clip = _clip;
        _source.Play();
    }

    public void SetPointToGo(Vector3 point) =>
        _logic.SetTarget(point);

    public void SetPointToGo(BuildStats build) =>
        _logic.SetTarget(build);

    public void SetPointToGo(IDamagable enemy) =>
        _logic.SetTarget(enemy);

    
}
