using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD> {
	public CanvasGroup[] indicators;
	public Image[] background ;
	public Text[] name;
	public Text[] score;

	public void setPlayerColor (int player , string name , Color color) 
	{
		background[player].color = color;

		//score[player].color = color;
		//name[player].text = name;
		//score[player].text = "0";
	}

	public void UpdateScore (int player , float score) {
		Debug.Log (score[0]);
	}

	public void setPlayerTurn (int player) {
		indicators[player].alpha =  1;
		indicators[ (player+1) %2].alpha = .2f;
	}
}
