using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTT : MonoBehaviour {
    [System.Serializable]
    public class Mesh_Info
    {
        public Mesh mesh;
        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;

        public Vector3 Posi1;
        public Vector3 Posi2;
        public Vector3 Posi3;
        public Vector3 Posi4;

        public Vector3[] vertices;
        public int[] triangles;

    }

    public Mesh_Info[] mesh_Info;
    public Material material;

    public int i;
    public int v;
    public bool Insta_OK;
    public bool Ver_Ready_OK;
    public bool Tri_Ready_OK;
    public bool Can_Create;

    public Transform T1;
    public Transform T2;
    void Start() {

    }

    void Update() {
        if (i < mesh_Info.Length)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Insta();
            }
            if (Insta_OK == true)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    Ver_Ready();
                }
                if (Input.GetKeyUp(KeyCode.V))
                {   
                    Ver_Ready();
                }
            }
            if (Ver_Ready_OK == true)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    Tri_Ready();
                }
            }
            if (Tri_Ready_OK == true)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Can_Create = true;
                }
            }
            if (Can_Create == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Mesh_Create();
                    i++;
                }
            }
        }
        
    }
    void Insta()
    {
        mesh_Info[i].vertices = new Vector3[4];
        mesh_Info[i].triangles = new int[6];

        mesh_Info[i].mesh = new Mesh();

        if (mesh_Info[i].meshFilter == null) gameObject.AddComponent<MeshFilter>();
        mesh_Info[i].meshFilter = gameObject.GetComponent<MeshFilter>();

        if (mesh_Info[i].meshRenderer == null) gameObject.AddComponent<MeshRenderer>();
        mesh_Info[i].meshRenderer = gameObject.GetComponent<MeshRenderer>();

        Insta_OK = true;
    }

    void Ver_Ready()
    {
        v++;
        switch (v) {
            case 1:
                mesh_Info[i].Posi1 = T1.position;
                mesh_Info[i].Posi2 = T2.position;
                break;
            case 2:
                mesh_Info[i].Posi3 = T1.position;
                mesh_Info[i].Posi4 = T2.position;

                mesh_Info[i].vertices[0] = mesh_Info[i].Posi1;
                mesh_Info[i].vertices[1] = mesh_Info[i].Posi2;
                mesh_Info[i].vertices[2] = mesh_Info[i].Posi3;
                mesh_Info[i].vertices[3] = mesh_Info[i].Posi4;

                Ver_Ready_OK = true;
                v = 0;

                break;
        }
    }
    void Tri_Ready()
    {
        mesh_Info[i].triangles[0] = 0;
        mesh_Info[i].triangles[1] = 1;
        mesh_Info[i].triangles[2] = 2;
        mesh_Info[i].triangles[3] = 3;
        mesh_Info[i].triangles[4] = 2;
        mesh_Info[i].triangles[5] = 1;

        Tri_Ready_OK = true;
    }

    void Mesh_Create()
    {
        mesh_Info[i].mesh.vertices = mesh_Info[i].vertices;
        mesh_Info[i].mesh.triangles = mesh_Info[i].triangles;

        mesh_Info[i].meshRenderer.material = material;
        mesh_Info[i].meshFilter.mesh = mesh_Info[i].mesh;

        Insta_OK = false;
        Ver_Ready_OK = false;
        Tri_Ready_OK = false;
        Can_Create = false;
    }
}
