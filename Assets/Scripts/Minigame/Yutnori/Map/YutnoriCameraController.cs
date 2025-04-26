using Photon.Pun.Demo.PunBasics;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class YutnoriCameraController : MonoBehaviour
{
    [SerializeField]
    private YutnoriGameManager gameManager;
    private Camera _mainCam;
    private Vector3 _dragStartPos;
    private bool _isDraggingYut;
    [SerializeField] private float minY = 5f;
    [SerializeField] private float maxY = 15f;
    [SerializeField] private LayerMask yutLayer; // �ν����Ϳ��� "Yut" ���̾� 


    private float dragSpeed = 1.0f;
    void Awake()
    {
        _mainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.stage != GameStage.Interact) return;

        // ���콺 Ŭ�� ���� �� ������ �Ǻ�
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, Mathf.Infinity, yutLayer))
            {
                _isDraggingYut = true;
                return;
            }
            _dragStartPos = _mainCam.ScreenToViewportPoint(Input.mousePosition);
        }

        // ���� �巡�� ���� �ƴϰ�, ���콺�� �������� �� ī�޶� �̵�
        if (!_isDraggingYut && Input.GetMouseButton(0))
        {
            Vector3 currentPos = _mainCam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 delta = _dragStartPos - currentPos;
            float newY = transform.position.y + delta.y * dragSpeed * 100; // ���� ����
            newY = Mathf.Clamp(newY, minY, maxY);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            _dragStartPos = currentPos;
        }

        // ���콺 ��ư���� ���� �� �� �ʱ�ȭ
        if (Input.GetMouseButtonUp(0))
        {
            _isDraggingYut = false;
        }
    }
}
