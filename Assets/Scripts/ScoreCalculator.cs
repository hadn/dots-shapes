using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator {

	public static float getScore (GameObject go, List<Node> borderNodes) {
		Vector2 topRight = Vector2.zero;
		Vector2 bottomLeft = Vector2.zero;
		foreach (var node in borderNodes)
		{
			Vector2 p = node.transform.position;
			if (p.x > topRight.x)
				topRight.x = p.x;
			else if (p.x < bottomLeft.x)
				bottomLeft.x = p.x;

			if (p.y > topRight.y)
				topRight.y = p.y;
			else if (p.y < bottomLeft.y)
				bottomLeft.y = p.y;
		}
		
		var myCol = go.GetComponent<PolygonCollider2D>();
		List<Node> nodes = Board.Instance.nodes;
		float I = 0;
		float B = borderNodes.Count;
		foreach (var node in nodes)
		{
			Vector2 p = node.transform.position;
			if (p.x < bottomLeft.x || p.x > topRight.x ||
				p.y < bottomLeft.y || p.y > topRight.y){
					continue;
				}
			
			if (borderNodes.Contains (node)){
				continue;
			}
			var col = Physics2D.OverlapPoint (p);
			if (myCol == col)
				I++;
		}

		return B/2 + I -1;
	}
}
