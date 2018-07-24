using UnityEngine;

public class Perceptron : MonoBehaviour
{
    const float bias = 1f;
    const float learningRate = 0.1f;
    public float[] weights;
    public int dataPoints = 100;
    public Coordinate[] dataSet;


    private Coordinate dataPoint;
    private float error = 0f;

    void Start()
    {
        InitializeWeights();
        CreateDataset();
        FeedForward();
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

    private void FeedForward()
    {
        for (int i = 0; i < dataSet.Length; i++)
        {
            dataPoint = dataSet[i];
            dataPoint.sum = Sum(dataPoint);
            dataPoint.estimate = Activate(dataPoint);
            dataPoint.error = CalculateError(dataPoint);
            Debug.Log(dataPoint.Values());
        }
    }

    private float CalculateError(Coordinate coord)
    {
        return coord.answer - coord.estimate;
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

    //private void Train(int i)
    //{
    //    error = inputs[i].z - guess;
    //    if (error != 0f)
    //    {
    //        weights.x += error * inputs[i].x * learningRate;
    //        weights.y += error * inputs[i].y * learningRate;
    //        weights.z += error * inputs[i].z * learningRate;
    //        Debug.Log("Weights are " + weights);
    //    }
}
