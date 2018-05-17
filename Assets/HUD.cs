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

		this.score[player].color = color;
		this.name[player].color = color;
		this.name[player].text = name;
		this.score[player].text = "0";
	}

	public void UpdateScore (int player , float score) {
		this.score[player].text = score.ToString("0.0");
	}

	public void setPlayerTurn (int player) {
		indicators[player].alpha =  1;
		indicators[ (player+1) %2].alpha = .2f;
	}
}
