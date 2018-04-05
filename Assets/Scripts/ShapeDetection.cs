using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDetection : MonoBehaviour{
	public bool DEBUG_MODE = false;
	public int cellsPerUnit = 5;
	int[,] pixels;
	Board board;
	double step ;

	public void generatePixelGrid (Board board) {
		this.board = board;
		step = 1 / (double)cellsPerUnit;
		pixels = new int[cellsPerUnit * (board.boardSize.x-1) + 1,cellsPerUnit * (board.boardSize.y-1)+1];
		for (int i = 0 ; i < pixels.GetLength(0);i++) {
			for (int j =0;j< pixels.GetLength(1);j++) {
				pixels[i,j] = 0;
			}
		}
	}

	public void Paint (Node n1 , Node n2) {
		Vector3 p1 = n1.transform.position;
		Vector3 p2 = n2.transform.position;
		p2 += board.getOffset();
        p1 += board.getOffset ();
		p1 *= cellsPerUnit;
		p2 *= cellsPerUnit;
		Vector2Int P1 = new Vector2Int ((int)p1.y, (int)p1.x);
		Vector2Int P2 = new Vector2Int ((int)p2.y, (int)p2.x);
		bresenham(P1,P2);
	}

    

	void OnDrawGizmos () {
        if (pixels == null || !DEBUG_MODE)
            return;
		float height = board.boardSize.x;
		float width = board.boardSize.y;

		Vector3 offset = new Vector3 ( (width-1) / 2f, (height-1)/ 2f , 0f) ;
		Color color = Color.white;
        for (int i = 0; i < pixels.GetLength(0); i++)
        {
            for (int j = 0; j < pixels.GetLength(1); j++)
            {
				color.r=color.g= color.b = pixels[i,j];
				Gizmos.color = color;
				Gizmos.DrawSphere(new Vector3(j*(float)step, i*(float)step, 0) - offset, (float)step/3f);
            }
        }
	}

	void bresenham (Vector2Int P1 , Vector2Int P2) {
		bresenham (P1.x,P1.y,P2.x,P2.y,1);
	}

    void bresenham(int x, int y, int x2, int y2, int color)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            pixels[x, y] = color;
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
    }
}
