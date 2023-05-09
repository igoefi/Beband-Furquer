using UnityEngine;

public class SpawnPrefabOnKeyDown : MonoBehaviour
{
    public GameObject m_Prefab;
    public KeyCode m_KeyCode;

    private void Update()
    {
        if (Input.GetKeyDown(m_KeyCode) && m_Prefab != null)
            Instantiate(m_Prefab, transform.position, transform.rotation);
    }
}
