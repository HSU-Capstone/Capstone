using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtExplosion : MonoBehaviour
{
    public ParticleSystem dirtParticles; // �� Ƣ�� ���� �ý���

    void Start()
    {
        // �������� ������ ���� Ƣ�� ȿ�� �߻�
        PlayDirtParticles(transform.position);
    }

    void PlayDirtParticles(Vector3 position)
    {
        dirtParticles.transform.position = position;
        dirtParticles.Play();
    }
}
