using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class LinkFactory : MonoBehaviour {
	public Dictionary<int,Link> linkSet = new Dictionary<int,Link>();
    int linkId = 1;
	ObjectPool pool;

	void Awake () {
		pool = GetComponent<ObjectPool>();
	}

	public Link getLinkById (int id) {
		Link link = null;
		linkSet.TryGetValue(id,out link);
		return link;
	}

	public Link CreateLink (Node n1 , Node n2) {
		linkId++;
		Link link = pool.getObject().GetComponent<Link>();
		link.transform.position =  Vector3.zero;
		link.transform.rotation = Quaternion.identity;
		setupLineRenderer(n1,n2,link);
		setupEdgeCollider(n1,n2,link);
		link.name = n1.Id + "-" + n2.Id;
		link.Id = linkId;
		link.n1 = n1;
		link.n2 = n2;
		linkSet.Add(link.Id,link);
		return link;
	}

	void setupLineRenderer (Node n1, Node n2, Link link) {
		LineRenderer lineRenderer = link.GetComponent<LineRenderer>();
        Vector3[] points = new Vector3[] {
            n1.transform.position,
            n2.transform.position};
        lineRenderer.SetPositions(points);
	}

	void setupEdgeCollider (Node n1, Node n2, Link link) {
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
