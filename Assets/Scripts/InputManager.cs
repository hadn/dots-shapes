using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<MonoBehaviour> {

	public float nodeSelectionTouchRadius = .15f;
	public LayerMask nodeMask;
	

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Collider2D col =  Physics2D.OverlapCircle(
				getMousePosition(),
				nodeSelectionTouchRadius,
				nodeMask);
			if (col != null)
				col.GetComponent<Node>().OnClick();
			else 
				Node.OnClickNothing();
		}
	}

	Vector3 getMousePosition () {
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	void OnDrawGizmos () {
        Gizmos.color = Color.cyan;
		Gizmos.DrawSphere (getMousePosition(),nodeSelectionTouchRadius);
	}
}
