using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class NodeFactory : MonoBehaviour{
    public Dictionary<int, Node> nodeSet = new Dictionary<int, Node>();
    int linkId ;
	ObjectPool pool;


	void Awake (){ 
		pool = GetComponent<ObjectPool>();
		linkId = 1;
	}

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
		Node node = pool.getObject().GetComponent<Node>();
        node.transform.position = new Vector2(j, i) - offset;
		node.transform.rotation = Quaternion.identity;
		node.Init(nodeId, i,j);
		node.gameObject.name = "Node "  + nodeId; 
		return node;
	}

    public void Clear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                pool.returnObject(transform.GetChild(i).gameObject);
            }
        }
    }
}
