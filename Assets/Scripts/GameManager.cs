using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		Camera.main.orthographicSize = (boardSize.x+1)/2;
	}
}