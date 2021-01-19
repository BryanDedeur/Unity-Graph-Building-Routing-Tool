using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{

    public Dictionary<string, Robot> travelers = new Dictionary<string, Robot>();

    public GameObject robotPrefab;
    public Graph graph;

    public List<Color> colors;

    public void AddRoute(string robot, List<int> vertices)
    {
        if (!travelers.ContainsKey(robot))
        {
            Transform rob = Instantiate(robotPrefab.transform);
            travelers.Add(robot, rob.GetComponent<Robot>());
        }
        travelers[robot].UpdateRoute(vertices);

    }
}

