using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPParticle : MonoBehaviour
{
    [SerializeField] private GameObject _particle;

    private Vector3 DefaultForce = new(0f, 1f, 0f);
    private const float DefaultForceScatter = 0.5f;

    private void Start()
    {
        GetComponent<Stats>().IsMeAttacking.AddListener(ChangeHP);
    }

    public void ChangeHP(float Delta)
    {
        GameObject NewHPP = Instantiate(_particle, transform.position + Vector3.up, gameObject.transform.rotation);
        NewHPP.transform.localScale = Vector3.one * 5;
        NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera");

        TextMesh TM = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();

        TM.text = Delta < 0 ? "+" : "";
        TM.text += Delta.ToString();
        TM.color = Delta < 0 ? new Color(0f, 1f, 0f, 1f) : new Color(1f, 0f, 0f, 1f);

        NewHPP.GetComponent<Rigidbody>().AddForce(new Vector3(
            DefaultForce.x 
            + Random.Range(-DefaultForceScatter, DefaultForceScatter), DefaultForce.y 
            + Random.Range(-DefaultForceScatter, DefaultForceScatter), DefaultForce.z 
            + Random.Range(-DefaultForceScatter, DefaultForceScatter)));
    }

}
