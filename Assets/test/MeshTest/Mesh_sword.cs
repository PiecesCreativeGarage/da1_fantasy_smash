using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_sword : MonoBehaviour {

    public GameObject posi1;
    public GameObject posi2;

    Vector3 posi1_1fbefore = Vector3.zero;
    Vector3 posi2_1fbefore = Vector3.zero;

    Vector3[] vertices;
    int[] triangles;

    
    
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    
    void Start () {
        vertices = new Vector3[4];
        triangles = new int[6];
        
    }

    // Update is called once per frame
    void Update() {
    
        Mesh mesh = new Mesh();
        mesh.Clear();

        vertices[0] = posi1.transform.position;
        vertices[1] = posi2.transform.position;
        vertices[2] = posi1_1fbefore;
        vertices[3] = posi2_1fbefore;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 3;
        triangles[4] = 2;
        triangles[5] = 1;

        

        mesh.vertices = vertices;
        mesh.triangles = triangles;


        mesh.RecalculateNormals();

        meshRenderer = GetComponent<MeshRenderer>();
       

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;




        posi1_1fbefore = posi1.transform.position;
        posi2_1fbefore = posi2.transform.position;
       

	}
}
