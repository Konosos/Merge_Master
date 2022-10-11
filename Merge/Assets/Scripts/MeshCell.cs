using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCell : MonoBehaviour
{
    Vector3[] newVertices;
    int[] newTriangles;
    Mesh mesh;
    [SerializeField] private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        CreateShape();
        UpdateMesh();
    }
    void CreateShape()
    {
        newVertices = new Vector3[]
        {
            new Vector3(-2,0,-2),
            new Vector3(-2,0,2),
            new Vector3(2,0,2),
            new Vector3(2,0,-2)
        };
        newTriangles = new int[]
        {
            0,1,2,
            0,2,3
        };
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
    }
}
