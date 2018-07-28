﻿using System.Collections;
using UnityEngine;

public class Perceptron : MonoBehaviour
{
    const float bias = 1f;
    private float learningRate = 0.05f;
    public float[] weights;
    public int dataPoints = 100;
    public Coordinate[] dataSet;
    public float totalError = 0f;

    private Coordinate dataPoint;
    private float errorcount = 0f;
    public LineRenderer theLine;
    public GameObject pointPrefab;
    public Transform pointParent;

    void Start()
    {
        InitializeWeights();
        CreateDataset();
        StartCoroutine(FeedForward());
    }

    private void CreateDataset()
    {
        dataSet = new Coordinate[dataPoints];

        for (int i = 0; i < dataSet.Length; i++)
        {
            dataSet[i] = new Coordinate();
            dataSet[i].Initialize();
        }
    }

    private void InitializeWeights()
    {
        weights = new float[3] { Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f) };
    }

    private IEnumerator FeedForward()
    {
        totalError = 0f;

        for (int i = 0; i < dataSet.Length; i++)
        {
            dataPoint = dataSet[i];
            dataPoint.sum = Sum(dataPoint);
            dataPoint.estimate = Activate(dataPoint);
            dataPoint.error = CalculateError(dataPoint);
            totalError += Mathf.Abs(dataPoint.error);
            Debug.Log(totalError);
            SpawnDatapoint(dataPoint);

            if (dataPoint.error != 0f)
            {
                Backpropagate(dataPoint);
            }
            Debug.Log(dataPoint.Values() + ", Wx: " + weights[0] + ", Wy: " + weights[1] + ", Wb: " + weights[2]);
            SetLineCoordinates();
            yield return new WaitForSeconds(0.01f);
        }

        if(totalError/dataSet.Length > .05f)
        {
            //learningRate = learningRate / 2;
            Debug.Log("Next iteration");
            StartCoroutine(FeedForward());
        }
    }

    private float Sum(Coordinate coord)
    {
        return coord.x * weights[0] + coord.y * weights[1] + bias * weights[2];
    }

    private float Activate(Coordinate coord)
    {
        if (coord.sum >= 0f)
        {
            return 1f;
        } else
        {
            return -1f;
        }
    }

    private float CalculateError(Coordinate coord)
    {
        return coord.answer - coord.estimate;
    }

    private void Backpropagate(Coordinate coord)
    {
        weights[0] += coord.error * coord.x * learningRate;
        weights[1] += coord.error * coord.y * learningRate;
        weights[2] += coord.error * bias * learningRate;
    }

    private void SpawnDatapoint(Coordinate coord)
    {
        Instantiate(pointPrefab, new Vector3(coord.x, coord.y), Quaternion.identity, pointParent);
    }

    private void SetLineCoordinates()
    {
        theLine.SetPosition(0, new Vector3(0f, OutY(0f)));
        theLine.SetPosition(1, new Vector3(10f, OutY(10f)));
    }

    private float OutY(float x)
    {
        return -(weights[2] / weights[1]) * bias - (weights[0] / weights[1]) * x;
    }

    public static float Solve(float x, float y)
    {
        if (x >= y)
        {
            return -1f;
        } else
        {
            return 1f;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
