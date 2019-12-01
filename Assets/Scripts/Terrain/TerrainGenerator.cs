
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	public int depth = 25;

	public int width = 256;
	public int height = 256;

	public float scale = 5;

	public float offsetX = 100f;
	public float offsetY = 100f;

	void Update()
	{

		//offsetX += 0.05f;

		Terrain terrain = GetComponent<Terrain>();

		terrain.terrainData = GenerateTerrain(terrain.terrainData);

	}

	TerrainData GenerateTerrain(TerrainData terrainData)
	{

		terrainData.heightmapResolution = width + 1;

		terrainData.size = new Vector3(width, depth, height);

		terrainData.SetHeights(0, 0, GenerateHeights());

		return terrainData;

	}

	float[,] GenerateHeights()
	{
		float[,] heights = new float[height, width];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				heights[x, y] = calculateHeight(x, y);
			}
		}

		return heights;

	}

	float calculateHeight(int x, int y)
	{
		float xCoord = (float)x / width * scale + offsetX;
		float yCoord = (float)y / width * scale + offsetY;

		return Mathf.PerlinNoise(xCoord, yCoord);
	}

}
