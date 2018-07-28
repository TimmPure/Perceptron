using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Coordinate
{
    public float x;
    public float y;
    public float sum;
    public float estimate;
    public float answer;
    public float error;


    public void Initialize()
    {
        x = Random.Range(-10f, 10f);
        y = Random.Range(-10f, 10f);
        estimate = 0f;
        sum = 0f;
        error = 0f;
        answer = Perceptron.Solve(x, y);
    }

    public string Values()
    {
        return "This Coord has x = " + x + ", y = " + y + ", sum = " + sum + ", answer = " + answer + ", estimate = " + estimate + ", error = " + error;
    }
}
