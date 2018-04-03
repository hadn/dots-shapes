using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkFactory : MonoBehaviour {

	public GameObject linkPrefab;

	public void CreateLink (Node n1 , Node n2) {
		GameObject link = Instantiate (linkPrefab,Vector3.zero,
			Quaternion.identity,transform);
		setupLineRenderer(n1,n2,link);
		setupEdgeCollider(n1,n2,link);
		link.name = n1.Id + "-" + n2.Id;
	}

	void setupLineRenderer (Node n1, Node n2, GameObject link) {
		LineRenderer lineRenderer = link.GetComponent<LineRenderer>();
        Vector3[] points = new Vector3[] {
            n1.transform.position,
            n2.transform.position};
        lineRenderer.SetPositions(points);
	}

	void setupEdgeCollider (Node n1, Node n2, GameObject link) {
		EdgeCollider2D col = link.GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[] {
            n1.transform.position,
            n2.transform.position};
		col.points = points;
	}
}
