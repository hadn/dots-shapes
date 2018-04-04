using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board> {
	public Vector2Int boardSize;
	public LinkFactory linkFactory;
	public NodeFactory nodeFactory;
	int [,] adjacencyMatrix;

	public float linkCollisionToleration ;
	public bool allowDiagonals;
	public bool allowFreeMode;

	public LayerMask linkCollisionMask;

	public void StartBoard (Vector2Int boardSize, bool allowDiagonals, bool allowFreeMode) {
		this.boardSize = boardSize;
		this.allowDiagonals = allowDiagonals;
		this.allowFreeMode = allowFreeMode;
		Init (boardSize.x * boardSize.y);
		nodeFactory.CreateNodes(boardSize.x,boardSize.y);
		//GetComponent<ShapeDetection>().generatePixelGrid(this);
	}

	public void Init (int nodeCount) {
		adjacencyMatrix = new int[nodeCount,nodeCount];
		for(int i = 0 ; i < nodeCount;i++){
			for (int j=0;j<nodeCount;j++) {
				adjacencyMatrix[i,j] = 0;
			}
		}
	}

	bool canConnectNodes (Node n1 , Node n2) {
        if (!allowFreeMode)
        {
            if (!allowDiagonals && !nodesAreAdjacend(n1,n2))
				return false;
			if (allowDiagonals && !nodesAreAdjacend(n1,n2) && !nodesAreDiagonalNeighbours(n1,n2))
				return false;
        }
		return true;
	}

	public void CreateConnection (Node n1, Node n2) {
		if (alreadyConnectedNodes(n1,n2))
			return;
		if (!canConnectNodes(n1,n2))
			return;
		if (newLinkCollides(n1,n2))
			return ;

		adjacencyMatrix[n1.Id,n2.Id] = 1;
		adjacencyMatrix[n2.Id,n1.Id] = 1;
		GameObject link = linkFactory.CreateLink(n1,n2);
		//GetComponent<ShapeDetection>().Paint(n1,n2);		
	}

	bool alreadyConnectedNodes (Node n1 , Node n2) {
		return adjacencyMatrix[n1.Id,n2.Id] == 1;
	}

	int horizontalDistance (Node n1,Node n2) {
		return Mathf.RoundToInt (Mathf.Abs (
			n1.transform.position.x - n2.transform.position.x));
	}
    int verticalDistance(Node n1, Node n2){
        return Mathf.RoundToInt(Mathf.Abs(
            n1.transform.position.y - n2.transform.position.y));
    }

	bool nodesAreAdjacend (Node n1 , Node n2) {
		return nodesAreVerticalNeighbours (n1,n2) ||
			nodesAreHorizontalNeighbours(n1,n2);
	}

	bool nodesAreVerticalNeighbours (Node n1 , Node n2) {
		return verticalDistance(n1,n2) == 1 && horizontalDistance(n1,n2) == 0;
	}

    bool nodesAreHorizontalNeighbours(Node n1, Node n2){
        return horizontalDistance(n1, n2) == 1 && verticalDistance(n1, n2) == 0;
    }

	bool nodesAreDiagonalNeighbours (Node n1 , Node n2) {
		return horizontalDistance(n1,n2) == 1 && verticalDistance(n1,n2 ) == 1;
	}

	bool newLinkCollides (Node n1 , Node n2) {
		Vector3 direction = n1.transform.position - n2.transform.position;
		direction = direction.normalized * linkCollisionToleration;
		Vector3 from = n1.transform.position - direction;
		Vector3 to = n2.transform.position + direction;
		return Physics2D.Linecast(from,to,linkCollisionMask);
	}

	public void ClearBoard () {
		linkFactory.Clear();
		nodeFactory.Clear();
	}
}
