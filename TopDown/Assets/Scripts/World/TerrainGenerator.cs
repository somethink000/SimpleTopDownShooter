using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TerrainGenerator
{

    
   
    public static int[,,] GenerateTerrain(int xOffset, int zOffset)
    {
        var result = new int[Chunk.ChunkWidth, Chunk.ChunkWidth, Chunk.ChunkWidth];
      
        for (int y = 0; y < Chunk.ChunkWidth; y++)
        {
            for (int x = 0; x < Chunk.ChunkWidth; x++)
            {
                for (int z = 0; z < Chunk.ChunkWidth; z++)
                {
                    result[x, y, z] = 1;
                }
            }
        }
        return result;




    }

   
}
