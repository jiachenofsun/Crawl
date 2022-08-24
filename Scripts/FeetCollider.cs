using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
	// Returns whether the obj is a floor, platform, or wall
	bool isFloor(GameObject obj)
	{
		return obj.layer == LayerMask.NameToLayer("Floor");
	}

    private int m_ColCount = 0;

    private float m_DisableTimer;

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isFloor(other.gameObject))
        {
            m_ColCount++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isFloor(other.gameObject))
        {
            m_ColCount--;
        }
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }
}
