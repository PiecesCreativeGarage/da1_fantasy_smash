using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_sword : MonoBehaviour {

    public GameObject posi1;
    public GameObject posi2;

    Vector3 posi1_1fbefore = Vector3.zero;
    Vector3 posi2_1fbefore = Vector3.zero;
    Vector3 posi1_2fbefore = Vector3.zero;
    Vector3 posi2_2fbefore = Vector3.zero;
  

    public Vector3[] vertices;
    int[] triangles;

    
    
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    int i2;
    int i;
    int a;

    public int Time;
    public int Time2;
    public int aaa;

    void Update () {
        vertices = new Vector3[8];
        triangles = new int[18];

        vertices[0] = posi1.transform.position;
        vertices[1] = posi2.transform.position;
        vertices[2] = posi1_1fbefore;/*posi1.transform.position + Vector3.up * 2;*/
        vertices[3] = posi2_1fbefore;/*posi2.transform.position + Vector3.up * 2;*/
        vertices[4] = posi1_1fbefore;
        vertices[5] = posi2_1fbefore;
        vertices[6] = posi1_2fbefore;
        vertices[7] = posi2_2fbefore;
        /*
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;
        */
        for (i2 = 0; i2 < 3; i2++)
        {
            for(a = 0; a < 6; a++)
            {
                switch (a)
                {
                    case 0:
                        triangles[i] = i2;
                        break;
                    case 1:
                        triangles[i] =  i2 + 1;
                        break;
                    case 2:
                        triangles[i] =  i2 + 3 + 1;
                        break;
                    case 3:
                        triangles[i] =  i2 + 3 + 1 + 1;
                        break;
                    case 4:
                        triangles[i] =  i2 + 3 + 1;
                        break;
                    case 5:
                        triangles[i] =  i2 + 1;
                        break;
                }
                i++;
            }
        }
        i = 0;
        Mesh mesh = new Mesh();
        mesh.Clear();


        

        mesh.vertices = vertices;
        mesh.triangles = triangles;


        mesh.RecalculateNormals();

        meshRenderer = GetComponent<MeshRenderer>();
       

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;


        if (aaa == Time2)
        {
            posi1_2fbefore = posi1_1fbefore;
            posi2_2fbefore = posi2_1fbefore;
            aaa = 0;
        }
        if(aaa == Time)
        {
            posi1_1fbefore = posi1.transform.position;
            posi2_1fbefore = posi2.transform.position;

            
        }

    

    /*aaa++;*/
    }
}
