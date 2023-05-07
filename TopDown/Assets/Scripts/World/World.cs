using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class World : MonoBehaviour
{
  

    public Dictionary<Vector2Int, ChunkData> ChunkDatas = new Dictionary<Vector2Int, ChunkData>();
    public Chunk chuncpref;
    private const int chunkCount = 4;

    public GameObject pine;

    private Camera mainCamera;
    private Vector2Int currentPlayerChunk;

    private mapObj[] allMapObjects;
    
    void Start()
    {
      allMapObjects = Resources.LoadAll<mapObj>("");
       
       
        mainCamera = Camera.main;

        Generate();

       
    }
    void Update()
    {
        Vector3Int playerWorldPos = Vector3Int.FloorToInt(mainCamera.transform.position);
        Vector2Int playerChunk = GetChunkContainsObject(playerWorldPos);
        if (playerChunk != currentPlayerChunk)
        {
            currentPlayerChunk = playerChunk;
            Generate();
        }
    }
    
    

    
    private void Generate()
    {
        for (int x = currentPlayerChunk.x - chunkCount; x < currentPlayerChunk.x + chunkCount; x++)
        {
            for (int y = currentPlayerChunk.y - chunkCount; y < currentPlayerChunk.y + chunkCount; y++)
            {
                
                if (ChunkDatas.ContainsKey(new Vector2Int(x, y))) continue;
                var chunkData = new ChunkData();
                
                int xPos = x * Chunk.ChunkWidth;
                int zPos = y * Chunk.ChunkWidth;

                chunkData.ChunkPosition = new Vector2Int(x, y);
                chunkData.planes = TerrainGenerator.GenerateTerrain(xPos, zPos);
                ChunkDatas.Add(new Vector2Int(x, y), chunkData);


                var chunk = Instantiate(chuncpref, new Vector3(xPos, 0, zPos), Quaternion.identity);
                chunk.ChunkData = chunkData;
                chunk.parent = this;


                foreach (var mapObj in allMapObjects)
                {
                    var chancerwsult = Random.Range(0, 100);
                    if (mapObj.chance >= chancerwsult)
                    {
                        GenerateObjects( mapObj.ptrfab, new Vector3(Random.Range(xPos, xPos + Chunk.ChunkWidth), 0, Random.Range(zPos, zPos + Chunk.ChunkWidth)));

                    }


                }
            }
        }
    }

    public void GenerateObjects(GameObject mapobj, Vector3 objpos)
    {
  
            Instantiate(mapobj, objpos, Quaternion.identity);
        

    }


    public Vector2Int GetChunkContainsObject(Vector3Int objWorldPos)
    {
        return new Vector2Int(objWorldPos.x / Chunk.ChunkWidth, objWorldPos.z / Chunk.ChunkWidth);
    }
}
