using UnityEngine;

public class Perceptron : MonoBehaviour
{

    public float[] inputs;
    public float[] weights;
    const float bias = 1f;
    const float learningRate = 0.1f;
    public Coordinate[] dataSet;

    private int dataPoints = 100;
    private float guess = 0f;
    private float error = 0f;

    void Start()
    {
        InitializeWeights();
        CreateDataset();

        //Process();
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

    private void InitializeWeights()
    {
        weights = new float[3] { Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f) };
    }

    private void CreateDataset()
    {
        dataSet = new Coordinate[dataPoints];
        for (int i = 0; i < dataSet.Length; i++)
        {
            dataSet[i] = new Coordinate();
            dataSet[i].Initialize();
            Debug.Log(dataSet[i].Values());
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

//private float Sum(int i)
//{
//    return inputs[i].x * weights.x + inputs[i].y * weights.y + bias * weights.z;
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
