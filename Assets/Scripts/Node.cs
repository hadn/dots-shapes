﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	public Vector2 coord;
	public int Id;

	static Node selectedNode;

	Color defaultColor ;


	void Start () {
		defaultColor = GetComponent<SpriteRenderer> ().color;
	}

	public void Init (int nodeId, int i , int j) {
		coord = new Vector2 (i,j);
		this.Id = nodeId;
	}

	public Board.PlayerMoveResult OnClick () {
		if (selectedNode == null) {
			selectedNode = this;
			ToggleIsSelected (true);
			return Board.PlayerMoveResult.INVALID;
		}
		else {
			Board.PlayerMoveResult result = Board.Instance.CreateConnection (this,selectedNode);
			selectedNode.ToggleIsSelected(false);
			selectedNode = null;
			return result;
		}
	}

	void ToggleIsSelected (bool isSelected) {
		GetComponent<SpriteRenderer>().color = 
			isSelected ? Color.cyan : defaultColor;
	}

	public static void OnClickNothing () {
		if (selectedNode!= null)
		{
			selectedNode.ToggleIsSelected(false);
			selectedNode = null;
		}
	}

}
