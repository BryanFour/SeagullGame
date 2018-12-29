using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// https://www.youtube.com/watch?v=HIsEqKPoJXM&index=3&list=PLT2nuNpL_CMTeUlEkWRRQ1llnhV2k9qpt&t=621s
// https://www.youtube.com/watch?v=WnvW6m4Fqmo&list=PLT2nuNpL_CMTeUlEkWRRQ1llnhV2k9qpt&index=3

public class TileManager : MonoBehaviour
{

	public GameObject[] groundSegs;         // Array of Prefabs to spawn.
	private List<GameObject> groundSegsList;   // A list of GameObjects labeled "activeTilesList"
	GameObject lastAddedToList;
	float lastAddedMaxZPos;

	//private Transform playerTransform;		// The Player's Transform, used to determin when its time to spawn/delete a tile. ----- Might be need for the safezone.
	private float spawnZ = 0f;					// Where we will spawn the tile along the Z axis. (-20 will spawn the tile 20 units behind the player.)(Could probally have a value of "0").
	//private float safeZone = 20f;				// Used to make the game wait a bit before Deleting a tile.
	private int amountOfTilesOnScreen = 3;		// The maximum amount of tiles allowed on screen at any given moment.
	private int lastPrefabIndex = 0;			// Needed for the RandomPrefabIndex function.
	

	void Start()
    {
		groundSegsList = new List<GameObject>();									// We must Instanciate the list, we do it here.
		//playerTransform = GameObject.FindGameObjectWithTag("Player").transform;   // The Player's Transform, used to determin when its time to spawn/delete a tile. ---- Might be need for the safe zone.

		for (int i = 0; i < amountOfTilesOnScreen; i++)
		{
			if (i < 2)				// Second vid 11mins
			{
				SpawnGroundSeg(0);		// Spawn the first prefab in the array the first 2 times
			}
			else
			{						// Else spawn a random Prefab
				SpawnGroundSeg();
			}
		}
	}

    void Update()
    {
		
	}

	public void SpawnGroundSeg(int prefabIndex = -1)		// The function used to spawn tiles.
	{
		GameObject groundSegGO; // A GameObject that we are labeling "groundSegGO".

		if (groundSegsList.Count == 0)
		{
			groundSegGO = Instantiate(groundSegs[Random.Range(0, 3)], new Vector3(0, 0, 0),
				Quaternion.identity);
			groundSegsList.Add(groundSegGO);
		}
		else
		{
			lastAddedToList = groundSegsList[groundSegsList.Count - 1];
			Debug.Log(lastAddedToList);
			lastAddedMaxZPos = lastAddedMaxZPos + (lastAddedToList.GetComponent<MeshRenderer>().bounds.size.z / 2);
			Debug.Log(lastAddedMaxZPos);
			groundSegGO = Instantiate(groundSegs[Random.Range(0, 3)]);
			lastAddedMaxZPos += groundSegGO.GetComponent<MeshRenderer>().bounds.size.z / 2;
			groundSegGO.transform.position = new Vector3(0f, 0f, lastAddedMaxZPos);
			groundSegsList.Add(groundSegGO);
		}
		groundSegGO.transform.SetParent(transform);					// Set the parent Object of the GameObject "go" to be the TileManager GameObject, to avoid clutter in the hierarchy.
		groundSegGO.transform.position = Vector3.forward * spawnZ;   // The transform of the Object/Tile we just created is equal to Vector3.forward(The Z axis) multiplyed by the value of SpawnZ.
		
	}

	public void DeleteTile()
	{
		//Destroy(activeTilesList[0]);						// Destroy the first element in the list...
		//activeTilesList.RemoveAt(0);						// Then remove it from the list.
	}

	private int RandomPrefabIndex()
	{
		if(groundSegs.Length <= 1)		// If there is only 1 prefab in the list return "0"
		{
			return 0;
		}

		int randomIndex = lastPrefabIndex;		// Declair randomIndex witch = lastPrefabIndex.

		while (randomIndex == lastPrefabIndex)	// While randomIndex is equal to lastPrefabIndex.(While we have the same prefabIndex value as we did previously)...
		{
			randomIndex = Random.Range(0, groundSegs.Length);	// Make the randomIndex a random value between 0 and the amount of prefabs in the tilePrefabs array.
		}

		lastPrefabIndex = randomIndex;			// If the lastPrefabIndex is different from the last then just return randomIndex.(Vid 2 8Mins)
		return randomIndex;
	}

	
}
