using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmokeControl : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.MainModule mainModule;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        mainModule = ps.main;

        // ���� ������ ȸ������ ����
        mainModule.startColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    public void ReduceSmoke()
    {
        mainModule.startSize = 1f;  // ���� ũ�� ���̱�
    }
}

