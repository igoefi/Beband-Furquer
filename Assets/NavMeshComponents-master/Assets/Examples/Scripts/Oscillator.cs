using UnityEngine;

// Makes a transform oscillate relative to its start position
public class Oscillator : MonoBehaviour
{
    public float m_Amplitude = 1.0f;
    public float m_Period = 1.0f;
    public Vector3 m_Direction = Vector3.up;
    private Vector3 m_StartPosition;

    private void Start()
    {
        m_StartPosition = transform.position;
    }

    private void Update()
    {
        var pos = m_StartPosition + (m_Direction * m_Amplitude * Mathf.Sin(2.0f * Mathf.PI * Time.time / m_Period));
        transform.position = pos;
    }
}
