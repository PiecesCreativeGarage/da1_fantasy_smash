using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTTT : MonoBehaviour {
    public Transform T1;
    public Transform T2;

    public Vector3[] posi;

    public List<Vector3> v_list;
    public Vector3[] vector3s;
    public int v3s_count = 0;
    public int Vadd_count;

    public List<int> i_list;
    public int[] ints;
    public int Iadd_count;
    public int ints_count;


    Mesh mesh;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    public Material material;
    private void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        posi = new Vector3[4];

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
        {
            posi[2] = posi[0];
            posi[3] = posi[1];
            posi[0] = T1.position;
            posi[1] = T2.position;





            vertices_ready();





            triangles_ready();

            mesh_Create();
        }

    }

    void vertices_ready()
    {
        for (int i = 0; i < posi.Length; i++)
        {
            v_list.Add(posi[i]);
        }
        vector3s = new Vector3[v_list.Count];
        foreach (Vector3 vector3 in v_list)
        {
            vector3s[v3s_count] = vector3;
            v3s_count++;
        }
        v3s_count = 0;

    }
    void triangles_ready()
    {

        for (int i = 0; i < 3; i++)
        {
            i_list.Add(Iadd_count + i);
        }
        for (int i = 3; i > 0; i--)
        {

            i_list.Add(Iadd_count + i);

        }
        Iadd_count += 4;

        ints = new int[i_list.Count];
        ints_count = 0;
        foreach (int i2 in i_list)
        {
            ints[ints_count] = i2;
            ints_count++;
            Debug.Log(i2);
        }
    }
    void mesh_Create()
    {
        mesh = new Mesh();

        mesh.vertices = vector3s;
        mesh.triangles = ints;

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshRenderer.material = material;
    }
}
