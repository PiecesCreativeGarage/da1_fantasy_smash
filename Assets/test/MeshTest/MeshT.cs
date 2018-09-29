using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshT : MonoBehaviour {
    

        public Transform T1;
        public Transform T2;
        public Vector3 Posi1;
        public Vector3 Posi2;
        public Vector3 Posi3;
       public Vector3 Posi4;
        public Vector3[] vertices;
        public int[] triangles;

    Mesh mesh;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    public Material material;


    int v;

    bool CanCreate;
    
	void Start () {
        vertices = new Vector3[4];
        triangles = new int[6];

        meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null) gameObject.AddComponent<MeshFilter>();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null) gameObject.AddComponent<MeshRenderer>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

       
            if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("押す");
                v = 1;
                Mesh_Vr();
            }
        if (Input.GetKeyUp(KeyCode.V))
        {
            Debug.Log("離す");
            v = 2;
            Mesh_Vr();
            CanCreate = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Mesh_Tr();

            if (CanCreate == true)
            {
                Debug.Log("作る");
                Mesh_Create();
            }
            else
            {
                Debug.Log("verticesがない");
            }
            
        }
	}

    void Mesh_Vr()
    {
        switch (v)
        {
            case 1:
                Posi1 = T1.position;
                Posi2 = T2.position;

                break;
            case 2:
                Posi3 = T1.position;
                Posi4 = T2.position;

                vertices[0] = Posi1;
                vertices[1] = Posi2;
                vertices[2] = Posi3;
                vertices[3] = Posi4;
                break;
        }
    }
    void Mesh_Tr()
    {
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 3;
        triangles[4] = 2;
        triangles[5] = 1;
    }
    void Mesh_Create()
    {
        mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        meshRenderer.material = material;
        meshFilter.mesh = mesh;

    }
}
