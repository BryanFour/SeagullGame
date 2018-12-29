using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstanciatingCubes : MonoBehaviour
{
	public GameObject[] groundSegs;
	private List<GameObject> groundSegsList;
	GameObject lastAddedToList;
	float lastAddedMaxZPos;
	void Start()
	{
		groundSegsList = new List<GameObject>();
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
		GameObject groundSegGO;

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
	}
}