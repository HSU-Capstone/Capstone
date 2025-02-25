using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{


    public GameObject objectA;  // ��ü A
    public GameObject objectB;  // ��ü B (Particle System)

    void Start()
    {
        // ���� ���� �� objectB�� ��Ȱ��ȭ
        objectB.SetActive(false);
    }

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ��
        if (Input.GetMouseButtonDown(0))
        {
            // ��ü A�� Ŭ������ ��
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == objectA)
                {
                    // ��ü A�� Ŭ���Ǹ� ��ü A�� ������� �ϰ�
                    objectA.SetActive(false);

                    // ��ü B�� Ȱ��ȭ
                    objectB.SetActive(true);
                }
            }
        }
    }
}
