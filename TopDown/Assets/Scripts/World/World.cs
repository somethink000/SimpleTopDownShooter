using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class World : MonoBehaviour
{
  

    public Dictionary<Vector2Int, ChunkData> ChunkDatas = new Dictionary<Vector2Int, ChunkData>();
    public Chunk chuncpref;
    public int chunkCount = 10;

    public GameObject pine;



    private void Start()
    {
        

        for (int x = 0; x < chunkCount; x++)
        {
            for (int y = 0; y < chunkCount; y++)
            {
                var chunkData = new ChunkData();

                int xPos = x * Chunk.ChunkWidth;
                int zPos = y * Chunk.ChunkWidth;

                chunkData.ChunkPosition = new Vector2Int(x, y);
                chunkData.planes = TerrainGenerator.GenerateTerrain(xPos,zPos);
                ChunkDatas.Add(new Vector2Int(x, y), chunkData);


                var chunk = Instantiate(chuncpref, new Vector3(xPos, 0, zPos), Quaternion.identity);
                chunk.ChunkData = chunkData;
                chunk.parent = this;


                GenerateObjects(pine, new Vector3(xPos,0,zPos));
            }
        }

       
    }


    public void GenerateObjects(GameObject obj, Vector3 objpos)
    {

        Instantiate(obj, objpos, Quaternion.identity);

    }

}
