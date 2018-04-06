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
		Vector2 midPoint = (Vector2)(P1 + P2) * 0.5f;
        Vector2 v = P2-P1;
        float magiConstant = Mathf.Sqrt(Mathf.Pow(v.x, 2f) + Mathf.Pow(v.y, 2f));
        Vector2 P3 = new Vector2(-v.y, v.x) / (magiConstant) * 3;
        Vector2 P4 = new Vector2(-v.y, v.x) / (magiConstant) * -3;
        Vector2Int roundedP3 = getNearestPoint(midPoint+P3);
        Vector2Int roundedP4 = getNearestPoint(midPoint+P4);
        
        if (!pointIsOutOfBounds(roundedP3)){
            FloodFill(roundedP3,0,1);
        }
        if (!pointIsOutOfBounds(roundedP4)){
            FloodFill(roundedP4, 0, 1);
        }
	}

    Vector2Int getNearestPoint (Vector2 p) {
        return new Vector2Int (Mathf.RoundToInt(p.x),Mathf.RoundToInt(p.y));
    }

    void paintPixel (Vector2Int p , int color ) {
        pixels[p.x,p.y] = color;
    }

    bool pointIsOutOfBounds (Vector2Int p) {
        return p.x < 0 || p.x >= pixels.GetLength(0) ||
        p.y < 0 || p.y >= pixels.GetLength(1);
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

    void FloodFill(Vector2Int p, int targetColor, int replacementColor)
    {
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        List<Vector2Int> shouldPaintThese = new List<Vector2Int>();
        bool breakOuterLoop = false;
        q.Enqueue(p);
        while (q.Count > 0 && !breakOuterLoop)
        {
            var n = q.Dequeue();
            if (pixels[n.x, n.y] != targetColor)
                continue;
            Vector2Int w = n, e = new Vector2Int(n.x + 1, n.y);
            while (true)
            {
                if (pointIsOutOfBounds(w))
                {
                    breakOuterLoop = true;
                    break;
                }
                if (pixels[w.x, w.y] != targetColor)
                    break;
                pixels[w.x, w.y] = replacementColor;
                shouldPaintThese.Add(w);
                if (w.y == 0 || w.y == pixels.GetLength(1) - 1 || w.x == 0 || w.x == pixels.GetLength(0) - 1)
                {
                    breakOuterLoop = true;
                    break;
                }
                if (pixels[w.x, w.y - 1] == targetColor)
                    q.Enqueue(new Vector2Int(w.x, w.y - 1));
                if (pixels[w.x, w.y + 1] == targetColor)
                    q.Enqueue(new Vector2Int(w.x, w.y + 1));
                w.x--;
            }
            if (breakOuterLoop)
                break;
            while (true)
            {
                if (pointIsOutOfBounds(e))
                {
                    breakOuterLoop = true;
                    break;
                }
                if (pixels[e.x, e.y] != targetColor)
                    break;
                pixels[e.x, e.y] = replacementColor;
                shouldPaintThese.Add(e);
                if (e.y == 0 || e.y == pixels.GetLength(1) || e.x == 0 || e.x == pixels.GetLength(0))
                {
                    breakOuterLoop = true;
                    break;
                }
                if (e.y > 0 && pixels[e.x, e.y - 1] == targetColor)
                    q.Enqueue(new Vector2Int(e.x, e.y - 1));
                if (pixels[e.x, e.y + 1] == targetColor)
                    q.Enqueue(new Vector2Int(e.x, e.y + 1));
                e.x++;
            }
        }
        if (breakOuterLoop)
        {
            foreach (var P in shouldPaintThese)
            {
                pixels[P.x, P.y] = targetColor;
            }
        }
        else
        {
            Debug.Log("Poligono encontrado");
        }
    }
}
