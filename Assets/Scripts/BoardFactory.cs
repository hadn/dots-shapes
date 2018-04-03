using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFactory : Singleton<BoardFactory> {

	public GameObject prefab;

	public void GenerateBoard (int width, int height) {
		Vector2 centralizationOffset = new Vector2 ( 	
			(height-1) /2f,
			(width-1)/2f);

		for (int i = 0; i < height ;i ++)
			for (int j=0;j<width;j++)
				Instantiate (prefab,
				new Vector2 (i,j) - centralizationOffset,
				Quaternion.identity,transform);
	}

}
