using UnityEngine;

public class Perceptron : MonoBehaviour
{

    public float[] inputs;
    public float[] weights;
    const float bias = 1f;
    const float learningRate = 0.1f;
    public Coordinate[] dataSet;
    public Coordinate dataPoint;

    private int dataPoints = 100;
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


//    private void Process()
//    {
//        inputs = new float[inputCount];

//        for (int i = 0; i < inputs.Length; i++)
//        {
//            Vector3 temp = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
//            temp.z = Label(temp);
//            inputs[i] = temp;
//            guess = Sum(i);
//            Activate();
//            Train(i);
//        }
//    }

//private float Label(Vector3 toLabel)
//{
//    if (toLabel.x >= toLabel.y)
//    {
//        return -1f;
//    } else
//    {
//        return 1f;
//    }
//}


//private void Activate()
//{
//    if (guess >= 0f)
//    {
//        guess = 1f;
//    } else
//    {
//        guess = -1f;
//    }
//    Debug.Log("Guess is " + guess);
//}

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
//}
//// Update is called once per frame
//void Update()
//{

//}
}
