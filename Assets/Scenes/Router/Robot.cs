using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public List<int> route = new List<int>();
    public List<int> queue = new List<int>();

    Vector3 targetPoint = Vector3.zero;
    public float speed = .1f;
    public Vector3 direction;

    Graph graph = null;

    LineRenderer lr = null;

    Color color;

    public void Awake()
    {

        graph = GameObject.Find("Graph").GetComponent<Graph>();
        transform.parent = GameObject.Find("Router").transform;
        lr = transform.GetComponent<LineRenderer>();

        color = new Color(
            Random.value,
            Random.value,
            Random.value
        );
        transform.GetComponent<Renderer>().material.SetColor("_Color", color);
        transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);

        lr.startColor = color;
        lr.endColor = color;
        lr.material.SetColor("_Color", color);
        lr.material.SetColor("_EmissionColor", color);
        lr.startWidth = .2f;
        lr.endWidth = .2f;
    }

    public void UpdateRoute(List<int> vertices)
    {
        route = vertices;

        int count = 0;
        Vector3 lastPos = Vector3.zero;
        foreach (int id in vertices)
        {
            count += 1;
            if (lr.positionCount < count + 1)
            {
                lr.positionCount += 1;
            }
            Vector3 newPos = graph.vertices[id].transform.position;
            Vector3 zOffset = new Vector3(0, 0, -1);
            Vector3 perpOffset = (newPos - lastPos).normalized * .01f;
            perpOffset = new Vector3(perpOffset.y, perpOffset.x);
            Vector3 finalPos = newPos + zOffset + perpOffset;
            lr.SetPosition(count, new Vector3(finalPos.x, finalPos.y, -1f));
            lastPos = newPos;
        }
    }

    public void Update()
    {
        if (queue.Count == 0)
        {
            queue = route;
        }

        if ((transform.position - targetPoint).magnitude < .05)
        {
            if (queue.Count > 0)
            {
                targetPoint = graph.vertices[queue[0]].transform.position;
                queue.RemoveAt(0);
            }
        }


        direction = (targetPoint - transform.position);
        transform.position += direction.normalized * speed;

    }
}
