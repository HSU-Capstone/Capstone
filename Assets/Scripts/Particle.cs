using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private float lifetime = 30f; // Particle�� 30�ʱ��� ������

    void Start()
    {
        Destroy(gameObject, lifetime); // 30�� �Ŀ� ����
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Smoke"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Smoke"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}

