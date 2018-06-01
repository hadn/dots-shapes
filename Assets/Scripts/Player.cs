using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	public enum Type {
		HUMAN, 
		AI
	}
	public Type playerType;
	public float score;
	public Color color;
	public Material mat;
	public AI ai = new AI();

	public Player (Type type , Color col) {
		this.playerType = type;
		this.color = col;
	}
}
