using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI  {
	static Board _board;
	static Board board {
		get {
			if (_board == null)
				_board = Board.Instance;
			return _board;
		}
	}

	List<Move> pendingMoves = new List<Move>();

	public Move TakeTurn (Link link) {
		pendingMoves.AddRange (checkNode (link.n1 , link.n2) );
		removeNoLongerPossibleMoves();
		if (pendingMoves.Count > 0){
			var move = pendingMoves[0];
			pendingMoves.RemoveAt(0);
			return move;
		}
		else 
			return new Move(null,null);// any viable move.
	}

	void removeNoLongerPossibleMoves () {
		for (int i = pendingMoves.Count-1;i>=0; i --)
		{
			var move = pendingMoves[i];
			if (!board.CanConnectNodes (move.n1, move.n2)){
				pendingMoves.Remove(move);
			}
		}
	}

	List<Move> checkNode (Node n1 , Node n2) {
		List<Move> moves = new List<Move>();
		foreach (var potential in n1.neighbours)
		{
			if (board.CanConnectNodes (potential, n2)){
				moves.Add ( new Move(potential,n2) );
			}
		}
		foreach (var potential in n2.neighbours)
		{
			if (board.CanConnectNodes (potential, n1)){
				moves.Add ( new Move (potential,n1));
			}		
		}
		return moves;
	}
}

public struct Move {
	public Node n1;
	public Node n2;
	public Move (Node n1, Node n2){
		this.n1 = n1;
		this.n2 = n2;
	}
}
