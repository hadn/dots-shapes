using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class LinkFactory : MonoBehaviour {
	ObjectPool pool;

	void Awake () {
		pool = GetComponent<ObjectPool>();
	}


	public void CreateLink (Node n1 , Node n2) {
		GameObject link = pool.getObject();
		link.transform.position =  Vector3.zero;
		link.transform.rotation = Quaternion.identity;
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

	public void Clear () {
		for (int i = 0;i<transform.childCount;i++) {
			if (transform.GetChild(i).gameObject.activeInHierarchy) {
				pool.returnObject(transform.GetChild(i).gameObject);
			}
		}
	}

}
