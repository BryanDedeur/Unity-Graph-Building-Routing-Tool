using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Vertex : MonoBehaviour
{
    public int id;

    public List<Edge> edges = new List<Edge>();

    private const int MAX = 10;
    private const int MIN = -10;

    public void Awake()
    {
        transform.position = new Vector3(Random.Range(MIN, MAX), Random.Range(MIN, MAX), 0);
        transform.parent = GameObject.Find("Graph").transform;
    }

    public void UpdateID(int newId)
    {
        id = newId;
        Transform textmeshpro = transform.Find("TMP");
        TextMeshPro textMesh = textmeshpro.GetComponent<TextMeshPro>();
        textMesh.text = id.ToString();
    }

    public void UpdatePosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
        foreach (var edge in edges)
        {
            edge.RefreshConnection();
        }
    }

    public void AddEdge(Edge edge)
    {
        edges.Add(edge);
    }
}
