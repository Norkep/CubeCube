using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ReadArduinoInputExample : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM3";
    public int baudrate = 9600;

    // Déclaration d'un tableau destiné à recevoir les valeurs des capteurs arduino.
    public float[] value = {0,0,0,0};
    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort(portName, baudrate);
        
        try
        {
            serialPort.Open();
        }
        catch
        {
            Debug.Log("Arduino not connected");
        }

        try
        {

            serialPort.ReadTimeout = 1;

        }
        catch
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                // Lire les valeures de l'arduino par ligne. En séparant les valeurs à chaque virgule.
                string[] sensorValues = serialPort.ReadLine().Split(',');
                // convertir le text en chiffre decimale et le ranger dans la 1ere case du tableau de valeurs 
                value[0] = float.Parse(sensorValues[0]);
                // passer de 0 - 1023 à 0 - 1
                value[0] = value[0] / 1023;
                // valeur entre 0 et 2
                value[0] *= 2;
                // valeur entre -1 et 1
                value[0] -= 1;
                
                // convertir le text en chiffre decimale et le ranger dans la 2ere case du tableau de valeurs 
                value[1] = float.Parse(sensorValues[1]);
                // Traitez mathématiquement la valeur au besoin.
                // ...etc
                value[2] = float.Parse(sensorValues[2]);
                value[3] = float.Parse(sensorValues[3]);
                
                
                Debug.Log(value);
            }
            catch (System.Exception e)
            {
            }
            
        }
    }
    
    private void OnDisable()
    {
        serialPort.Close();
    }
}
