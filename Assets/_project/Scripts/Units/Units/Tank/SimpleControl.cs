using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleControl : MonoBehaviour {
	
	//all left wheels
    public GameObject[] LeftWheels;
	//all right wheels
    public GameObject[] RightWheels;

    public GameObject LeftTrack;

    public GameObject RightTrack;

    public float wheelsSpeed = 2f;
    public float tracksSpeed = 2f;
    public float forwardSpeed = 1f;
    public float rotateSpeed = 1f;

    private delegate void _animDelegate();
    private Coroutine _animation;

    public enum Direction
    {
        Forward,
        Right,
        Left
    }


    public void SetDirection(Direction direction)
    {
        _animDelegate dir = null;
        switch (direction) 
        {
            case Direction.Forward:
                dir = new(Forward);
                break;
            case Direction.Right:
                dir = new(Right);
                break;
            case Direction.Left:
                dir = new(Left);
                break;
        }
        _animation = StartCoroutine(PlayAnimation(dir));

    }

    public void StopAnimation() =>
        StopCoroutine(_animation);

    private void Forward()
    {
        foreach (GameObject wheelL in LeftWheels)
        {
            wheelL.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
        }
        //Right wheels rotate
        foreach (GameObject wheelR in RightWheels)
        {
            wheelR.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
        }
        //left track texture offset
        LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);
        //right track texture offset
        RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);

        //Move Tank

        transform.Translate(new Vector3(0f, 0f, forwardSpeed));
    }

    private void Right()
    {
        foreach (GameObject wheelL in LeftWheels)
            wheelL.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));

        foreach (GameObject wheelR in RightWheels)
            wheelR.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));

        LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);
        RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);

        transform.Rotate(new Vector3(0f, -rotateSpeed, 0f));
    }

    private void Left()
    {
        foreach (GameObject wheelL in LeftWheels)
            wheelL.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
        foreach (GameObject wheelR in RightWheels)
            wheelR.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));

        LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
        RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);

        transform.Rotate(new Vector3(0f, rotateSpeed, 0f));
    }

    private IEnumerator PlayAnimation(_animDelegate dir)
    {
        if (dir == null) throw new("Hey, Direction is wrong");

        while (true)
        {
            dir();
            yield return new WaitForFixedUpdate();
        }
    }
}
