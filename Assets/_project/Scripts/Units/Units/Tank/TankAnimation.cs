using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] LeftWheels;
    [SerializeField] private GameObject[] RightWheels;

    [SerializeField] private GameObject LeftTrack;

    [SerializeField] private GameObject RightTrack;

    [SerializeField] private float wheelsSpeed = 2f;
    [SerializeField] private float tracksSpeed = 2f;
    [SerializeField] private float forwardSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;

    private Coroutine _animation;

    public void SetDirection()
    {
        StopAnimation();
        _animation = StartCoroutine(PlayAnimation());
    }

    public void StopAnimation()
    {
        if (_animation != null)
            StopCoroutine(_animation);
    }

    private void Forward()
    {
        foreach (GameObject wheelL in LeftWheels)
            wheelL.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));

        foreach (GameObject wheelR in RightWheels)
            wheelR.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));

        LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);
        RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);
    }

    private IEnumerator PlayAnimation()
    {
        while (true)
        {
            Forward();
            yield return new WaitForFixedUpdate();
        }
    }
}
