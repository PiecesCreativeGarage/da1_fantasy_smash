using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshaaa : MonoBehaviour {
    public Material mat;

    MeshFilter meshFilter;

    MeshRenderer meshRenderer;
    
    public int x;
    public int y;
    
    public int vertices_size;
    public int triangles_size;
    
    public Vector3[] vertices;
    public int[] triangles;
    int i;

    public float aaa;
    
	void Start () {

        var mesh = new Mesh ();
        
        vertices_size = (x + 1) * (y + 1);

        vertices = new Vector3[vertices_size];
        
        for(int iy = 0; iy < y + 1; iy++)
        {
            for(int ix = 0; ix < x + 1; ix++)
            {
                vertices[i] = new Vector3(ix * 1, iy * 1, 0);
                i++;
            }
        }
        
        i = 0;

        triangles_size = x * y * 6;

        triangles = new int[triangles_size];


        for (int iy = 0; iy < y; iy++)
        {
            int a3 = (x + 1) * iy;
            for (int ix = 0; ix < x; ix++)
            {
                int a4 = a3 + ix;
                for (int a = 0; a < 6; a++)
                {
                    
                    switch (a)
                    {
                        case 0:
                            triangles[i] = a4;
                            break;
                        case 1:
                            triangles[i] = a4 + 1;
                            break;
                        case 2:
                            triangles[i] = a4 + x + 1;
                            break;
                        case 3:
                            triangles[i] = a4 + x + 1 + 1;
                            break;
                        case 4:
                            triangles[i] = a4 + x + 1;
                            break;
                        case 5:
                            triangles[i] = a4 + 1;
                            break;
                    }
                    i++;
                }
            }
        }
        

        












        

        mesh.vertices = vertices;
        mesh.triangles = triangles;


        mesh.RecalculateNormals();

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = mat;

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;
        
    
    

        

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
