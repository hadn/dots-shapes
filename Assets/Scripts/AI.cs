using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

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
		if (link == null)
			return getRandomMove();
		pendingMoves.AddRange (checkNode (link.n1 , link.n2) );
		removeNoLongerPossibleMoves();
		if (pendingMoves.Count > 0){
			var move = pendingMoves[0];
			pendingMoves.RemoveAt(0);
			return move;
		}
		else 
			return getRandomMove();
	}

	Move getRandomMove () {
		List <Node> nodes = new List<Node> (Board.Instance.nodes);
		Move candidateMove = new Move(null,null);
		bool foundMove = false;
		while (nodes.Count > 0 && !foundMove) {
			var node =  nodes[Random.Range (0,nodes.Count)];
			nodes.Remove(node);
			foreach (var viz in Board.Instance.nodes)
			{
				if (node.Id == viz.Id)
					continue;
				if (Board.Instance.CanConnectNodes (node,viz)){
					candidateMove = new Move (node,viz);
					if (node.neighbours.Count < 1 && viz.neighbours.Count < 1 ){
						foundMove = true;
						break;
					}
				}
			}
		}
		return candidateMove;
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
