using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int acornCount = 0;

    // ���丮 �浹 �� ����
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Acorn"))
        {
            FindObjectOfType<PlayerInventory>().acornCount++;
            Destroy(other.gameObject);
        }
    }

}
