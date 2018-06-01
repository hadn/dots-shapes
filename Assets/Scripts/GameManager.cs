using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
	public enum State {
		RUNNING, PAUSED,STOPED
	}
	public State gameState ;
	List<Player> players;
	Player currentPlayer;


	float totalArea;
	float maxArea;

	public void startANewGame (Player player1, Player player2, Vector2Int boardSize,
		bool allowDiagonals,bool allowFreeMode) {
		Board.Instance.ClearBoard ();
		Board.Instance.StartBoard(boardSize,allowDiagonals,allowFreeMode);
		players = new List<Player> ( new Player[] {player1,player2} );
		currentPlayer = player1;
		gameState = State.RUNNING;
		float cameraHeight = Camera.main.orthographicSize;
		float cameraWidth = cameraHeight * Camera.main.aspect;

		float difHeight = boardSize.x -1 - cameraHeight;
		float difWidth = boardSize.y - 1 - cameraWidth;

		maxArea = (boardSize.x-1) * (boardSize.y-1);

		if (difHeight > difWidth){
			Camera.main.orthographicSize = ( (boardSize.x+1) * 1.1f) / 2f;
		}
		else {
            Camera.main.orthographicSize =  (( (boardSize.y+1)*1.1f)/2f) /Camera.main.aspect;
		}
		HUD.Instance.show();
		HUD.Instance.setPlayerColor (0, "Jogador 1" , player1.color);
		HUD.Instance.setPlayerColor (1, "Jogador 2" , player2.color);
		updatePlayerIndicator ();
	}

	void updatePlayerIndicator () {
		HUD.Instance.setPlayerTurn (players.IndexOf (currentPlayer));
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
		var score = ScoreCalculator.getScore(polygon,borderNodes);
		currentPlayer.score += score;
		var da = polygon.AddComponent<DisplayArea>();
		da.Area = score;
		HUD.Instance.UpdateScore (players.IndexOf (currentPlayer),currentPlayer.score);
		checkGameEndedState(score);
	}

	void checkGameEndedState (float score) {
		totalArea += score;
		if (totalArea == maxArea){
			Debug.Log ("Game has ended");
			gameState = State.STOPED;
		}
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