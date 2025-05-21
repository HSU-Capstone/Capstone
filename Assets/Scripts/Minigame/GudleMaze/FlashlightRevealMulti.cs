using UnityEngine;

public class FlashlightRevealMulti : MonoBehaviour
{
    [System.Serializable]
    public class RevealTarget
    {
        public GameObject targetObject; // ���� Canvas
        public Transform targetTransform; // ��ġ ���� (���� targetObject.transform)
    }

    public Transform flashlight; // Spot Light (������)
    public float maxDistance = 10f; // �ִ� �Ÿ�
    public float angleThreshold = 20f; // ������ ���� ��� ����
    public RevealTarget[] targets; // ���� �� ���

    void Update()
    {
        foreach (var target in targets)
        {
            Vector3 dirToTarget = target.targetTransform.position - flashlight.position;
            float angle = Vector3.Angle(flashlight.forward, dirToTarget);
            float distance = dirToTarget.magnitude;

            bool isVisible = angle < angleThreshold && distance < maxDistance;
            target.targetObject.SetActive(isVisible);
        }
    }
}
