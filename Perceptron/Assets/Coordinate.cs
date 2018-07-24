using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Coordinate
{
    public float x;
    public float y;
    public float answer;

    public void Initialize()
    {
        x = Random.Range(0f, 10f);
        y = Random.Range(0f, 10f);
        answer = Perceptron.Solve(x, y);
    }

    public string Values()
    {
        return "This Coord has X = " + x + ", y = " + y + ", answer = " + answer;
    }
}
