using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Graph : MonoBehaviour
{
    public List<Edge> edges = new List<Edge>();
    public Dictionary<int, Vertex> vertices = new Dictionary<int, Vertex>();
    public int numVertices = 0;

    public GameObject vertexPrefab;
    public GameObject edgePrefab;

    public string edgeEncoding;
    public string vertexEncoding;

    public InputField edgeText;
    public InputField vertexText;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void UpdateEncoding()
    {
        // Convert graph to adjaceny dictionary
        Dictionary<int, Dictionary<int, float>> adjacencyMatrix = new Dictionary<int, Dictionary<int, float>>();
        foreach (Edge edge in edges)
        {
            if (!adjacencyMatrix.ContainsKey(edge.vertex1.id))
            {
                adjacencyMatrix.Add(edge.vertex1.id, new Dictionary<int, float>());
            }

            if (!adjacencyMatrix.ContainsKey(edge.vertex2.id))
            {
                adjacencyMatrix.Add(edge.vertex2.id, new Dictionary<int, float>());
            }

            adjacencyMatrix[edge.vertex1.id][edge.vertex2.id] = edge.cost;
            adjacencyMatrix[edge.vertex2.id][edge.vertex1.id] = edge.cost;
        }

        edgeEncoding = "";

        for (int i = 0; i < numVertices; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                if (adjacencyMatrix.ContainsKey(i))
                {
                    if (adjacencyMatrix[i].ContainsKey(j))
                    {
                        edgeEncoding += adjacencyMatrix[i][j].ToString();
                    }
                    else
                    {
                        edgeEncoding += "0";

                    }
                } else
                {
                    edgeEncoding += "0";
                }

                if (j < numVertices - 1)
                {
                    edgeEncoding += ",";
                }
            }
            edgeEncoding += "\n";
        }

        edgeText.text = edgeEncoding; // NOT WORKING AS INTENDED

        vertexEncoding = "";
        foreach(var keyValuePair in vertices)
        {
            vertexEncoding += keyValuePair.Key.ToString() + "," + keyValuePair.Value.transform.position.x + "," + keyValuePair.Value.transform.position.y + "\n";
        }

        vertexText.text = vertexEncoding; // NOT WORKING AS INTENDED

    }

    public Vertex AddVertex(int id)
    {
        Transform trans = Instantiate(vertexPrefab.transform);
        Vertex vertex = trans.GetComponent<Vertex>();
        vertex.UpdateID(id);
        vertices.Add(id, vertex);
        numVertices += 1;
        UpdateEncoding();
        return vertex;
    }

    public Vertex GauranteeVertex(int id)
    {
        if (!vertices.ContainsKey(id))
        {
            AddVertex(id);
        }

        return vertices[id];
    }

    public bool UpdateVertexPos(int v, float xPos, float yPos)
    {
        Vertex vertex = GauranteeVertex(v);
        vertex.UpdatePosition(xPos, yPos);

        return false;
    }

    public bool AddEdge(int v1, int v2, float cost)
    {
        // Check if edge already exists
        foreach (Edge edge in edges)
        {
            if (cost == edge.cost)
            {
                if ((edge.vertex1.id == v1 && edge.vertex2.id == v2) || (edge.vertex1.id == v2 && edge.vertex2.id == v1))
                {
                    // Edge already exists
                    return false;
                }
            }
        }

        Vertex ver1 = GauranteeVertex(v1);
        Vertex ver2 = GauranteeVertex(v2);

        Transform trans = Instantiate(edgePrefab.transform);
        Edge newEdge = trans.GetComponent<Edge>();
        newEdge.vertex1 = ver1;
        newEdge.vertex2 = ver2;
        newEdge.cost = cost;
        newEdge.RefreshConnection();

        edges.Add(newEdge);

        ver1.AddEdge(newEdge);
        ver2.AddEdge(newEdge);
        UpdateEncoding();

        return true;
    }
}
