using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SmokePool : MonoBehaviour
{
    public ParticleSystem smokePrefab;
    private Queue<ParticleSystem> smokePool = new Queue<ParticleSystem>();
    public int poolSize = 5;  // �ִ� 5���� ����

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            ParticleSystem smoke = Instantiate(smokePrefab, transform.position, Quaternion.identity);
            smoke.gameObject.SetActive(false);
            smokePool.Enqueue(smoke);
        }
    }

    public void SpawnSmoke()
    {
        if (smokePool.Count > 0)
        {
            ParticleSystem smoke = smokePool.Dequeue();
            smoke.transform.position = transform.position;
            smoke.gameObject.SetActive(true);
            smoke.Play();

            // ���� �ð��� ������ �ٽ� ��Ȱ��ȭ
            StartCoroutine(ReturnToPool(smoke, 3f));
        }
    }

    private System.Collections.IEnumerator ReturnToPool(ParticleSystem smoke, float delay)
    {
        yield return new WaitForSeconds(delay);
        smoke.Stop();
        smoke.gameObject.SetActive(false);
        smokePool.Enqueue(smoke);
    }
}
