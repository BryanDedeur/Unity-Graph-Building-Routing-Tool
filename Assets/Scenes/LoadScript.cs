using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadScript : MonoBehaviour
{
    public Graph graph = null;
    public Router router = null;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void LoadEdges(string str)
    {
        string[] lines = str.Split('\n');
        int v1, v2, cost;

        int row = 0;
        foreach (string line in lines)
        {

            if (line.Contains("coste") && line.Contains("demanda")) // DAT format
            {
                string temp = line;
                temp = temp.Replace(" ( ", "");
                temp = temp.Replace(")  coste ", ",");
                temp = temp.Replace(" demanda ", ",");

                string[] tokens = temp.Split(',');
                v1 = Int32.Parse(tokens[0]) - 1;
                v2 = Int32.Parse(tokens[1]) - 1;
                cost = Int32.Parse(tokens[2]);

                graph.AddEdge(v1, v2, cost);
            }
            else // Check if CSV format
            {
                string[] tokens = line.Split(',');

                if (tokens.Length > 1)
                {
                    int col = 0;
                    foreach (string token in tokens)
                    {
                        float value = float.Parse(token);
                        if (value > 0)
                        {
                            graph.AddEdge(row, col, value);
                        }
                        col++;
                    }
                    row++;
                }

            }

        }
    }

    public void LoadVertices(string str)
    {
        string[] lines = str.Split('\n');
        int v;
        float xPos, yPos;
        foreach (string line in lines)
        {
            string[] tokens = line.Split(',');
            if (tokens.Length == 3)
            {
                v = Int32.Parse(tokens[0]);
                xPos = float.Parse(tokens[1]);
                yPos = float.Parse(tokens[2]);

                graph.UpdateVertexPos(v, xPos, yPos);

            }
        }
    }

    public void LoadRoutes(string str)
    {
        string[] lines = str.Split('\n');
        foreach (string line in lines)
        {
            string robot = "";
            string[] tokens = line.Split(' ');
            if (tokens.Length > 0)
            {
                List<int> vertices = new List<int>();
                robot = tokens[0];
                for (int i = 1; i < tokens.Length; i++)
                {
                    vertices.Add(Int32.Parse(tokens[i]));
                }
                router.AddRoute(robot, vertices);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
