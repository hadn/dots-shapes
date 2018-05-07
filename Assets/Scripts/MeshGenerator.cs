using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {
	public Transform polygonHolder;
    public LinkFactory linkFactory;

    Dictionary<int, GraphNode> graph ;
    bool endRecursion = false;
    List<GraphNode> cycle ;

	public void GeneratePolygon (List<int> edges) {
		if (edges.Count < 3)
			return;
        graph = new Dictionary<int,GraphNode>();

		for (int i = 0 ; i < edges.Count;i++) {
			Link link = linkFactory.getLinkById(edges[i]);
            if (link == null)
                continue;
            if (!graph.ContainsKey(link.n1.Id))
                graph.Add(link.n1.Id,new GraphNode(link.n1));
            if (!graph.ContainsKey(link.n2.Id))
                graph.Add(link.n2.Id, new GraphNode(link.n2));
            graph[link.n1.Id].viz.Add(graph[link.n2.Id]);
            graph[link.n2.Id].viz.Add(graph[link.n1.Id]);
		}

        removeLeafs();
        GraphNode initialNode = null;
        foreach (var node in graph.Values) {
            if (node.viz.Count == 2){
                initialNode = node;
                break;
            }
        }
        
        endRecursion = false;
        cycle = null;
        DFS(initialNode, new List<GraphNode>(), null);
        
        if (cycle != null) {
            List<Vector2> worldPositions = new List<Vector2>();
            foreach (var n in cycle)
            {
                worldPositions.Add(n.worldNode.transform.position);
            }
            createTheMesh(worldPositions);
        }
	}

    void removeLeafs () {
        foreach (var node in graph.Values) {
            GraphNode f = node;
            while (f != null ){
                if (f.viz.Count == 1) {
                    var parent = f.viz[0];
                    f.viz.Clear();
                    parent.viz.Remove (f);
                    f = parent;
                }else {
                    f = null;
                }
            }
        }
    }

    void DFS (GraphNode node, List<GraphNode> path, GraphNode parent) {
        path.Add(node);
        foreach (var v in node.viz){
            if (endRecursion)
                return;
            if (v == parent)
                continue;
            if (path.Contains(v)){
                endRecursion = true;
                cycle = path;
                return;
            }
            DFS (v,path,node);
        }
    }

    void createTheMesh (List<Vector2> vertList) {
        Vector2[] verts = vertList.ToArray();
        Triangulator tr = new Triangulator(verts);
        int[] indices = tr.Triangulate();

        Vector3[] vertices = new Vector3[verts.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(verts[i].x, verts[i].y, 0);
        }

        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        var go = new GameObject();
        go.transform.position = Vector3.zero;
        go.transform.parent = polygonHolder;
        go.AddComponent(typeof(MeshRenderer));
        MeshFilter filter = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        PolygonCollider2D col = go.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        col.points = vertList.ToArray();
        go.layer = LayerMask.NameToLayer("Polygon");
        filter.mesh = msh;
    }


    class GraphNode {
        public List<GraphNode> viz = new List<GraphNode>();
        public GraphNode parent;
        public Node worldNode;

        public GraphNode (Node worldNode) {
            this.worldNode = worldNode;
        }
    }
}
