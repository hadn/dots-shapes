using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : Singleton<MonoBehaviour> {

	public float nodeSelectionTouchRadius = .15f;
	public LayerMask nodeMask;
	

	void Update () {
		if (GameManager.Instance.gameState != GameManager.State.RUNNING)
			return;
		if (Input.GetMouseButtonDown(0)) {
			Collider2D col =  Physics2D.OverlapCircle(
				getMousePosition(),
				nodeSelectionTouchRadius,
				nodeMask);
			if (col != null){
				var result = col.GetComponent<Node>().OnClick();
				GameManager.Instance.PlayerMoved (result);
			}
			else 
				Node.OnClickNothing();
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			GameManager.instance.ReloadGame();
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
