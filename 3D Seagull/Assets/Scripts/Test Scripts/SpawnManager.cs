using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] testCubes;
	public List<GameObject> cubesList;
	
	void Start()
    {
		cubesList = new List<GameObject>();
	}
    void Update()
    {
		CubeSpawner();
    }
	private void CubeSpawner()
	{
		if (Input.GetMouseButtonDown(0))
		{
			SpawnCube();
		}
	}
	void SpawnCube()
	{
		GameObject cubePrefab;
		
		if (cubesList.Count == 0)
		{
			cubePrefab = Instantiate(testCubes[Random.Range(0, 3)], new Vector3(0, 0, 0), Quaternion.identity);
			cubesList.Add(cubePrefab);
		}
		else 
		{
			var lastAddedToList = cubesList.Last();

			float lastAddedMaxZPos = lastAddedToList.GetComponent<MeshRenderer>().bounds.max.z;
			//float lastAddedSizeZ = lastAddedToList.GetComponent<MeshRenderer>().bounds.size.z;
			//float pos = lastAddedSizeZ / 2 + lastAddedMaxZPos;

			cubePrefab = Instantiate(testCubes[Random.Range(0, 3)], new Vector3(0, 0, lastAddedMaxZPos), Quaternion.identity);
			cubesList.Add(cubePrefab);

			Debug.Log("lastAddedMaxZPos = " + lastAddedMaxZPos);
			//Debug.Log("lastAddedSizeZ = " + lastAddedSizeZ);

			Debug.Log("The last segment added to the list is " + lastAddedToList);
		}
	}
}
