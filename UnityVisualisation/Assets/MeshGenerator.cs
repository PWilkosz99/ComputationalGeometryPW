using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticles;
    int[] triangles;

    void CreateShape()
    {
        verticles = new Vector3[]
        {
            new Vector3 (0,0,0),
            new Vector3 (0,0,1),
            new Vector3 (1,0,0),
        };
        triangles = new int[]
        {
            0,1,2,
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticles;
        mesh.triangles = triangles;
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();

        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
