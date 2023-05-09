using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _scrollSpeed;
    [SerializeField] float _rotateSpeed;

    [SerializeField] float _smooth;

    [SerializeField] float maxY;
    [SerializeField] float minY;

    private void LateUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var thirdMouse = -Input.GetAxis("Mouse ScrollWheel");

        var y = transform.localPosition.y > minY ?
            transform.localPosition.y < maxY ?
            _scrollSpeed * thirdMouse : -.1f
            : .1f;

        var targetPosition = transform.position 
            + _speed * vertical * transform.forward
            + new Vector3(0, y) 
            + _speed * horizontal * transform.right;

        var velocity = Vector3.zero;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition,
            targetPosition, ref velocity, _smooth);
    }

    private void Rotate()
    {
        var rotateRight = Input.GetKey(KeyCode.E);
        var rotateLeft = Input.GetKey(KeyCode.Q);

        if (rotateRight && rotateLeft) return;

        var coef = rotateRight ? 1 : 
            rotateLeft ? -1 
            : 0;

        var targetPosition = new Vector3(0, coef * _rotateSpeed, 0);

        var velocity = Vector3.zero;

        transform.Rotate(targetPosition, Space.World);
    }
}
