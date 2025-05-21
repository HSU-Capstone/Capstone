using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour
{
    [Tooltip("Ŭ���� ������ 3D ������Ʈ�� �±�")]
    public string targetObjectTag = "GudleJang";

    [Tooltip("Ȱ��ȭ�� Canvas�� �±�")]
    public string canvasTag = "GudleJang";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(targetObjectTag))
                {
                    Debug.Log("Pressed T while pointing at: " + hit.collider.name);

                    GameObject[] canvases = GameObject.FindGameObjectsWithTag(canvasTag);
                    foreach (GameObject canvasObj in canvases)
                    {
                        canvasObj.SetActive(true); // Canvas ��ü GameObject Ȱ��ȭ
                    }
                }
            }
        }
    }
}
