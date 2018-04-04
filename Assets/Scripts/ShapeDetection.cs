using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDetection : MonoBehaviour{
	public LayerMask cellMask;
	public float cellsPerUnit = 5;
	public GameObject cellPrefab;
	public Transform cellHolder;
	GameObject[,] cellGrid;
    Vector2 centralizationOffset;

	public void generatePixelGrid (Board board) {
		GameObject clone = Instantiate (cellPrefab,cellHolder);
		float step = 1/cellsPerUnit;
		clone.GetComponent<BoxCollider2D>().size = new Vector2 (step,step) * 1.1f;
		clone.GetComponent<SpriteRenderer>().size = new Vector2 (step,step);

         centralizationOffset = new Vector2(
            (board.boardSize.y - 1) / 2f,
            (board.boardSize.x - 1) / 2f);
		
		int gridHeight = Mathf.RoundToInt ((board.boardSize.y-1) / step);
        int gridWidth = Mathf.RoundToInt((board.boardSize.x - 1) / step);
		cellGrid = new GameObject[gridHeight,gridWidth];

		int k = 0;
		for (float i = step/2; i < board.boardSize.y - 1;i+=step,k++){
			int l = 0;
			for (float j = step/2 ; j < board.boardSize.x-1;j+=step , l++){
				GameObject cloneOfClone = Instantiate(
					clone,new Vector2(i,j) - centralizationOffset,Quaternion.identity,cellHolder);
                cellGrid[k, l] = cloneOfClone;
				cloneOfClone.name = k + "-" +l ;
			}
		}
		Destroy(clone);
	}

	public void Paint (Node n1 , Node n2) {
		RaycastHit2D[] hits = Physics2D.LinecastAll (n1.transform.position,n2.transform.position,cellMask);
		foreach (var hit in hits) {
			hit.collider.GetComponent<Cell>().nodes = new Node[] {n1,n2};
			hit.collider.GetComponent<SpriteRenderer>().color = Color.black;
		}
	}
}
