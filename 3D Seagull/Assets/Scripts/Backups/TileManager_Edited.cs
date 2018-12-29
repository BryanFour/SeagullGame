using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// https://www.youtube.com/watch?v=HIsEqKPoJXM&index=3&list=PLT2nuNpL_CMTeUlEkWRRQ1llnhV2k9qpt&t=621s
// https://www.youtube.com/watch?v=WnvW6m4Fqmo&list=PLT2nuNpL_CMTeUlEkWRRQ1llnhV2k9qpt&index=3

public class TileManager_Edited : MonoBehaviour
{

	public GameObject[] tilePrefabs;			// Array of Prefabs to spawn.
	private Transform playerTransform;			// The Player's Transform, used to determin when its time to spawn/delete a tile.
	private float spawnZ = 20f;					// Where we will spawn the tile along the Z axis. (-20 will spawn the tile 20 units behind the player.)(Could probally have a value of "0").
	//private float safeZone = 20f;				// Used to make the game wait a bit before Deleting a tile.
	private int amountOfTilesOnScreen = 7;		// The maximum amount of tiles allowed on screen at any given moment.
	private int lastPrefabIndex = 0;			// Needed for the RandomPrefabIndex function.
	private List<GameObject> activeTilesList;   // A list of GameObjects labeled "activeTilesList"

	// My Code
	
	//End My Code

	void Start()
    {
		activeTilesList = new List<GameObject>();	// We must Instanciate the list, we do it here.
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;     // The Player's Transform, used to determin when its time to spawn/delete a tile.

		for (int i = 0; i < amountOfTilesOnScreen; i++)
		{
			if (i < 2)				// Second vid 11mins
			{
				SpawnTile(0);		// Spawn the first prefab in the array the first 2 times
			}
			else
			{						// Else spawn a random Prefab
				SpawnTile();
			}
		}
	}

    void Update()
    {
		
	}

	public void SpawnTile(int prefabIndex = -1)		// The function used to spawn tiles.
	{
		GameObject go; // A GameObject that we are labeling "go".

		if (prefabIndex == -1)
		{
			go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;   // Instanciate a tilePrefab with the RandomPrefabIndex.
			activeTilesList.Add(go);
		}
		else
		{
			go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
			activeTilesList.Add(go);
		}
		go.transform.SetParent(transform);					// Set the parent Object of the GameObject "go" to be the TileManager GameObject, to avoid clutter in the hierarchy.
		go.transform.position = Vector3.forward * spawnZ;   // The transform of the Object/Tile we just created is equal to Vector3.forward(The Z axis) multiplyed by the value of SpawnZ.
		//activeTilesList.Add(go);
		
		// My Code.
		var lastTileInList = activeTilesList.Last();
		float lengthOfLastInList = lastTileInList.GetComponentInChildren<MeshRenderer>().bounds.size.z;
		// My Code End.

		spawnZ += lengthOfLastInList;						// Now that the tile has (spawned??) we set the value of SpawnZ to equal (SpawnZ + TileLength).													// When we spawn a tile GameObject(go) Add it to the list of spawned tiles.
		Debug.Log("SpawnTile Function - Length of Last Tile " + lengthOfLastInList);
		//Debug.Log("SpawnTile Function - spawnZ Value " + spawnZ);
	}

	public void DeleteTile()
	{
		Destroy(activeTilesList[0]);						// Destroy the first element in the list...
		activeTilesList.RemoveAt(0);						// Then remove it from the list.
	}

	private int RandomPrefabIndex()
	{
		if(tilePrefabs.Length <= 1)		// If there is only 1 prefab in the list return "0"
		{
			return 0;
		}

		int randomIndex = lastPrefabIndex;	// Declair randomIndex witch = lastPrefabIndex.

		while (randomIndex == lastPrefabIndex)	// While randomIndex is equal to lastPrefabIndex.(While we have the same prefabIndex value as we did previously)...
		{
			randomIndex = Random.Range(0, tilePrefabs.Length);	// Make the randomIndex a random value between 0 and the amount of prefabs in the tilePrefabs array.
		}

		lastPrefabIndex = randomIndex;		//If the lastPrefabIndex is different from the last then just return randomIndex.(Vid 2 8Mins)
		return randomIndex;
	}

	
}
