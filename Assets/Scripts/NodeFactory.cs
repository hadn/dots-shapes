using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFactory : MonoBehaviour{
	public GameObject nodePrefab;

	public void CreateNodes (int height, int width) {
		Vector2 centralizationOffset = new Vector2 (	
			(width-1) /2f,
			(height-1)/2f);

		for (int i = 0; i < height ;i ++){
			for (int j=0;j<width;j++){
				createNode(i * width + j,i,j,centralizationOffset);
			}
		}
	}

	Node createNode ( int nodeId, int i , int j , Vector2 offset) {
		Node node =  Instantiate(nodePrefab,
        new Vector2(j, i) - offset,
        Quaternion.identity, transform).GetComponent<Node>();
		node.Init(nodeId, i,j);
		node.gameObject.name = "Node "  + nodeId; 
		return node;
	}
}
