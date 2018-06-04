using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIManager : Singleton<AIManager> {

	public float delay = 0f;
	Link link = null;
	UnityAction<Move> callback;
	Player player;

	public void DelayTakeTurn (Player player, Link link, UnityAction<Move> callback){
		this.link = link;
		this.callback = callback;
		this.player = player;
		StartCoroutine (delayedAction ());
	}

	IEnumerator delayedAction () {
		yield return new WaitForSeconds (delay);
		callback (player.ai.TakeTurn (link));
	}
}
