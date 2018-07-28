using System.Collections;
using UnityEngine;

public class Perceptron : MonoBehaviour
{
    const float bias = 10f;
    private float learningRate = 0.05f;
    public float[] weights;
    public int dataPoints = 500;
    public Coordinate[] dataSet;
    public float totalError = 0f;
    private bool firstIteration = true;
    public float slope;
    public float intercept;

    private Coordinate dataPoint;
    private float errorcount = 0f;
    public LineRenderer theLine;
    public LineRenderer solutionLine;
    public GameObject pointPrefab;
    public Transform pointParent;

    void Start()
    {
        InitializeValues();
        CreateDataset();
        SetSolutionCoordinates();
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

    private void InitializeValues()
    {
        weights = new float[3] { Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-10f, 10f) };
        slope = Random.Range(-3f, 3f);
        intercept = Random.Range(-5f, 5);
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
            if (firstIteration)
            {
                SpawnDatapoint(dataPoint);
            }


            if (dataPoint.error != 0f)
            {
                Backpropagate(dataPoint);
            }
            Debug.Log(dataPoint.Values() + ", Wx: " + weights[0] + ", Wy: " + weights[1] + ", Wb: " + weights[2]);
            SetLineCoordinates();
            yield return new WaitForSeconds(0.01f);
        }
        firstIteration = false;
        totalError = totalError / dataSet.Length;
        if ( totalError > .05f)
        {
            learningRate = learningRate *.9f;
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
        theLine.SetPosition(0, new Vector3(-10f, OutY(-10f)));
        theLine.SetPosition(1, new Vector3(10f, OutY(10f)));
    }


    private void SetSolutionCoordinates()
    {
        solutionLine.SetPosition(0, new Vector3(-10f, SolveY(-10f)));
        solutionLine.SetPosition(1, new Vector3(10f, SolveY(10f)));
    }

    private float OutY(float x)
    {
        return -(weights[2] / weights[1]) * bias - (weights[0] / weights[1]) * x;
    }

    private float SolveY(float x)
    {
        return slope * x + intercept;
    }

    public float Solve(float x, float y)
    {
        if (y >= (slope * x + intercept))
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
