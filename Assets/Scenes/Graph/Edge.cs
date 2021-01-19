using System.Collections;
using UnityEngine;

[System.Serializable]
public class Edge : MonoBehaviour {

    public Vertex vertex1 = null;
    public Vertex vertex2 = null;
    public float cost = 0;

    public void Awake()
    {
        transform.parent = GameObject.Find("Graph").transform;
    }
    public void RefreshConnection()
    {
        Vector3 offset = vertex2.transform.position - vertex1.transform.position;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, offset.magnitude);
        transform.position = vertex1.transform.position + (offset / 2f);
        transform.LookAt(vertex2.transform);
    }

}
