using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public string StringToPrint = "Surf bort";

    private float timeSincePrint = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrintHelloWorld(StringToPrint);
    }

    // Update is called once per frame
    void Update()
    {

        timeSincePrint += Time.deltaTime;
        PrintHelloWorld(timeSincePrint.ToString());
    }

    public void PrintHelloWorld(string toPrint)
    {
        Debug.Log(toPrint);
    }
}
