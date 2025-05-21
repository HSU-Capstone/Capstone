using UnityEngine;

public class AxeHit: MonoBehaviour
{
    public TreeCutter treeCutter;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe"))
        {
            treeCutter.CutTree();
            Destroy(this.gameObject);  // �� �̻� �߸��� �ʰ� Ʈ���� ����
        }
    }
}
