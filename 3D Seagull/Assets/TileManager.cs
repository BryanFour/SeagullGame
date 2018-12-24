using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// https://www.youtube.com/watch?v=HIsEqKPoJXM&index=3&list=PLT2nuNpL_CMTeUlEkWRRQ1llnhV2k9qpt&t=621s
// https://www.youtube.com/watch?v=WnvW6m4Fqmo&list=PLT2nuNpL_CMTeUlEkWRRQ1llnhV2k9qpt&index=3

public class TileManager : MonoBehaviour
{

	public GameObject[] tilePrefabs;

	private Transform playerTransform;
	private float spawnZ = -20.0f;
	private float tileLength = 40.085f;
	private float safeZone = 40f;
	private int amountOfTilesOnScreen = 7;
	private int lastPrefabIndex = 0;

	private List<GameObject> activeTilesList;

	void Start()
    {
		activeTilesList = new List<GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

		for(int i = 0; i < amountOfTilesOnScreen; i++)
		{
			if (i < 2)
			{
				SpawnTile(0);
			}
			else
			{
				SpawnTile();
			}
		}
	}

    void Update()
    {

		//////////////////////////////////
		// Test code to get last tile in list.

		var lastTileInList = activeTilesList.Last();

		///////////////////////////////////

		if (playerTransform.position.z - safeZone > (spawnZ - amountOfTilesOnScreen * tileLength))
		{
			SpawnTile();
			DeleteTile();
		}
    }

	void SpawnTile(int prefabIndex = -1)
	{
		GameObject go;

		if (prefabIndex == -1)
		{
			go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
		}
		else
		{
			go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
		}
		go.transform.SetParent(transform);
		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
		activeTilesList.Add(go);

	}

	void DeleteTile()
	{
		Destroy(activeTilesList[0]);
		activeTilesList.RemoveAt(0);
	}

	private int RandomPrefabIndex()
	{
		if(tilePrefabs.Length <= 1)
		{
			return 0;
		}
		int randomIndex = lastPrefabIndex;
		while (randomIndex == lastPrefabIndex)
		{
			randomIndex = Random.Range(0, tilePrefabs.Length);
		}

		lastPrefabIndex = randomIndex;
		return randomIndex;
	}
}
