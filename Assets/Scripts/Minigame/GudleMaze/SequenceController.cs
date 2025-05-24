using System.Collections;
using UnityEngine;

public class SequenceController : MonoBehaviour
{
    public Animator rotationAnimator;        // 360�� ȸ�� �ִϸ�����
    public Animator mainAnimator;            // ���� ���� �ִϸ��̼�
    public GameObject[] deactivateObjects;   // ��Ȱ��ȭ�� ������Ʈ��
    public GameObject crossSection;          // �ܸ鵵 ������Ʈ
    public GameObject[] uiSequence;          // ������� ���� UI ������Ʈ��

    void Start()
    {
        // 1. 360�� ȸ�� �ִϸ��̼� ���� ����
        rotationAnimator.Play("Rotate360");

        // 2. 5�� �� �ܸ鵵 ���� �� ������Ʈ ��Ȱ��ȭ
        Invoke("ShowCrossSection", 5f);
    }

    void ShowCrossSection()
    {
        foreach (var obj in deactivateObjects)
            obj.SetActive(false);

        crossSection.SetActive(true);

        // 3. ���� �ִϸ��̼� ����
        mainAnimator.SetTrigger("StartMain"); // Animator�� trigger �Ű����� �ʿ�

        // 4. UI ���ʷ� ��Ÿ����
        StartCoroutine(ActivateUISequence());
    }

    IEnumerator ActivateUISequence()
    {
        for (int i = 0; i < uiSequence.Length; i++)
        {
            yield return new WaitForSeconds(1f); // 1�� ���� (���ϸ� ���� ����)
            uiSequence[i].SetActive(true);
        }
    }
}
