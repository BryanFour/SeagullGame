using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundSegSpawnManager : MonoBehaviour
{
	public GameObject[] testCubes;
	private List<GameObject> cubesList;
	GameObject lastAddedToList;
	float lastAddedMaxZPos;

	private int amountOfTilesOnScreen = 4;      // The maximum amount of tiles allowed on screen at any given moment.

	void Start()
	{
		cubesList = new List<GameObject>();

		for (int i = 0; i < amountOfTilesOnScreen; i++)
		{
			if (i < 2)              // Second vid 11mins
			{
				SpawnGroundSeg(0);      // Spawn the first prefab in the array the first 2 times
			}
			else
			{                       // Else spawn a random Prefab
				SpawnGroundSeg();
			}
		}
	}
	void Update()
	{
		if(cubesList.Count >= 6)
		{
			DeleteGroundSeg();
		}
	}
	
	public void SpawnGroundSeg(int prefabIndex = -1)
	{
		GameObject cubePrefab;

		if (cubesList.Count == 0)
		{
			cubePrefab = Instantiate(testCubes[Random.Range(0, 3)], new Vector3(0, 0, 0),
				Quaternion.identity);
			cubesList.Add(cubePrefab);
		}
		else
		{
			lastAddedToList = cubesList[cubesList.Count - 1];
			Debug.Log(lastAddedToList);
			lastAddedMaxZPos = lastAddedMaxZPos + (lastAddedToList.GetComponentInChildren<MeshRenderer>().bounds.size.z / 2);
			Debug.Log(lastAddedMaxZPos);
			cubePrefab = Instantiate(testCubes[Random.Range(0, 3)]);
			lastAddedMaxZPos += cubePrefab.GetComponentInChildren<MeshRenderer>().bounds.size.z / 2;
			cubePrefab.transform.position = new Vector3(0f, 0f, lastAddedMaxZPos);
			cubesList.Add(cubePrefab);
		}
	}

	public void DeleteGroundSeg()
	{
		Destroy(cubesList[0]);						// Destroy the first element in the list...
		cubesList.RemoveAt(0);						// Then remove it from the list.
	}
}