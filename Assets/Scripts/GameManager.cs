using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
	public enum State {
		RUNNING, PAUSED,STOPED
	}
	public State gameState ;
	public Image currentPlayerIndicator;
	List<Player> players;
	Player currentPlayer;

	public void startANewGame (Player player1, Player player2, Vector2Int boardSize,
		bool allowDiagonals,bool allowFreeMode) {
		Board.Instance.ClearBoard ();
		Board.Instance.StartBoard(boardSize,allowDiagonals,allowFreeMode);
		players = new List<Player> ( new Player[] {player1,player2} );
		currentPlayer = player1;
		updatePlayerIndicator ();
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

	void updatePlayerIndicator () {
		if (!currentPlayerIndicator.gameObject.activeInHierarchy)
			currentPlayerIndicator.gameObject.SetActive(true);
		currentPlayerIndicator.color = currentPlayer.color;
	}

	void nextPlayer () {
		currentPlayer = players [ (players.IndexOf (currentPlayer) + 1 ) % players.Count];
		updatePlayerIndicator();
	}

	public void newPolygonCreated (GameObject polygon, List<Node> borderNodes) {
		if (currentPlayer.mat == null ){
			currentPlayer.mat = new Material(Shader.Find("Unlit/Color"));
			currentPlayer.mat.color = currentPlayer.color;
		}
		polygon.GetComponent<MeshRenderer>().material = currentPlayer.mat;
		// calculate points;
	}

	public void PlayerMoved (Board.PlayerMoveResult result) {
		switch (result)
		{
			case Board.PlayerMoveResult.NEW_EDGE:
				nextPlayer();
				break;
			case Board.PlayerMoveResult.NEW_POLYGON:
				break;
		}
	}

	public void ReloadGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}