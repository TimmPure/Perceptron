using UnityEngine;

public class Perceptron : MonoBehaviour
{
    const float bias = 1f;
    const float learningRate = 0.1f;
    public float[] weights;
    public int dataPoints = 100;
    public Coordinate[] dataSet;


    private Coordinate dataPoint;

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

            if (dataPoint.error != 0f)
            {
            Backpropagate(dataPoint);
            }
            Debug.Log(dataPoint.Values() + ", Wx: " + weights[0] + ", Wy: " + weights[1]);
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
}
