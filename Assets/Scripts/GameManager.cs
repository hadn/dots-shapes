using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
	public enum State {
		RUNNING, PAUSED,STOPED
	}
	public State gameState ;


	public void startANewGame (Player player1, Player player2, Vector2Int boardSize,
		bool allowDiagonals,bool allowFreeMode) {
		Board.Instance.ClearBoard ();
		Board.Instance.StartBoard(boardSize,allowDiagonals,allowFreeMode);
		gameState = State.RUNNING;
		float cameraHeight = Camera.main.orthographicSize;
		float cameraWidth = cameraHeight * Camera.main.aspect;

		float difHeight = boardSize.x -1 - cameraHeight;
		float difWidth = boardSize.y - 1 - cameraWidth;

		if (difHeight > difWidth){
			Camera.main.orthographicSize = (boardSize.x+1) / 2f;
		}
		else {
            Camera.main.orthographicSize =  ((boardSize.y+1)/2f) /Camera.main.aspect;
		}

	}

	public void ReloadGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}