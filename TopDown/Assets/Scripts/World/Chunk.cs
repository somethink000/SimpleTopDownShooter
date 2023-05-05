using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Chunk : MonoBehaviour
{
    public const int ChunkWidth = 12;

    public World parent;
    public ChunkData ChunkData;

    private List<Vector3> verticles = new List<Vector3>();
    private List<int> triangles = new List<int>();

    
    void Start()
    {
        
        Mesh chunkMesh = new Mesh();


        
        
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int z = 0; z < ChunkWidth; z++)
                {
                    GeneratePlane(x, 0, z);
                }
            }
        


        chunkMesh.vertices = verticles.ToArray();
        chunkMesh.triangles = triangles.ToArray();

        chunkMesh.RecalculateNormals();
        chunkMesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = chunkMesh;
        GetComponent<MeshCollider>().sharedMesh = chunkMesh;


    }




  
    private void GeneratePlane(int x, int y, int z)
    {
        if (ChunkData.planes[x, y, z] == 0)
        {
            return;
        }

        GenerateTop(new Vector3Int(x, y, z));
    }

    private void GenerateTop(Vector3Int planeposition)
       {
        verticles.Add(new Vector3(0, 0, 0) + planeposition);
        verticles.Add(new Vector3(0, 0, 1) + planeposition);
        verticles.Add(new Vector3(1, 0, 0) + planeposition);
        verticles.Add(new Vector3(1, 0, 1) + planeposition);

        triangles.Add(verticles.Count - 4);
        triangles.Add(verticles.Count - 3);
        triangles.Add(verticles.Count - 2);

        triangles.Add(verticles.Count - 3);
        triangles.Add(verticles.Count - 1);
        triangles.Add(verticles.Count - 2);

    }

}
