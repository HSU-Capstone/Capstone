using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeshDeformation : MonoBehaviour
{
    public MeshFilter meshFilter;   // ������ MeshFilter
    public Vector3 holeCenter;      // ������ ���� ��ġ
    public float holeRadius = 1f;   // ������ ũ��
    public float holeDepth = 0.5f;  // ������ ����

    void Start()
    {
        DeformMesh();
    }

    void DeformMesh()
    {
        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            float distance = Vector3.Distance(vertex, holeCenter);

            if (distance < holeRadius)
            {
                float deformation = Mathf.Lerp(0, holeDepth, 1 - (distance / holeRadius)); // ���̸� �ο�
                vertices[i] -= new Vector3(0, deformation, 0); // Y������ ���̸�ŭ ����
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals(); // ���� ���
        mesh.RecalculateBounds();  // �ٿ�� ���
    }
}

