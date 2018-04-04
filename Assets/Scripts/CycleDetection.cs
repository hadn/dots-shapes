using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class CycleDetection {

	static int [,] M;
	public static List < List <int >> getCycles (int[,] adjacencyMatrix) {
		M = adjacencyMatrix;
		
		return null;
	}

	static List <int> getNeighbours (int nodeId) {
		List<int> neighbours = new List<int>();
		for (int i = 0;i<M.GetLength(0);i++){
			if (M[nodeId,i] == 1)
				neighbours.Add(i);
		}
		return neighbours;
	}

	static void printAdjacentMatrix () {
		StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < M.GetLength(0); i++)
        {
            for (int j = 0; j < M.GetLength(1); j++)
            {
                sb.Append(M[i, j]).Append(" ");
            }
            sb.Append("\n");
        }
        Debug.Log(sb.ToString());
	}

}
