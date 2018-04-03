using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFactory : Singleton<BoardFactory> {

	public GameObject prefab;

	public void GenerateBoard (int width, int height) {
		for (int i = 0; i < height ;i ++) 
			for (int j=0;j<width;j++)
				Instantiate (prefab,
				new Vector2 (i,j),
				Quaternion.identity,transform);
	}

	void Start () {
		GenerateBoard (5,5);
	}
}
