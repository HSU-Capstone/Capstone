using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Scene ��ȯ�� ���� ���ӽ����̽�

 // Scene ��ȯ�� ���� ���ӽ����̽�

public class ClickEffect : MonoBehaviour
{
    public GameObject clickEffectPrefab;  // ���¿��� ������ ��ƼŬ ȿ�� ������
    public float effectDuration = 5f;     // ����Ʈ�� ���ӵ� �ð� (��)
    private GameObject effect;            // ������ ��ƼŬ ȿ���� ������ ����

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��Ŭ�� (���� ���콺 ��ư)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // ���콺 ��ġ���� ���� �߻�
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // ��ü�� �浹�� ���
            {
                // Ŭ���� ��ġ�� ��ƼŬ �ý��� ����
                effect = Instantiate(clickEffectPrefab, hit.point, Quaternion.identity);

                // 5�� �Ŀ� ����Ʈ�� ����
                Destroy(effect, effectDuration); // effectDuration �ð� �Ŀ� ����
            }
        }
        if (effect != null && !effect.activeInHierarchy)
        {
            SceneManager.LoadScene("Gudle_End");  // "End"��� ���� �ε�
        }
    }

    // 5�� �� �ڵ����� End ������ ��ȯ
    private void Start()
    {
        // Invoke�� Start()���� �����ϰ�, ��ƼŬ �ý����� ����� �� �� ��ȯ�� ó���� ����
    }

    // ��ƼŬ�� ������� End ������ ��ȯ
 
}
