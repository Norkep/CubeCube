using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ReadTwoArduinoValuesExample : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "/dev/cu.usbmodem14101"; //valeure variable depuis l'IDE Arduino
    public int baudrate = 115200;

    // Déclaration d'un tableau destiné à recevoir les valeurs des capteurs arduino.
    public float[] values = { 0, 0, 1, 2, 3, 4 };

    public float xpos = 0;
    public float ypos = 0;

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
                values[0] = float.Parse(sensorValues[0]);

                // Ref: MyPlayerControllerByArduino.cs

                // déplacement horizontal
                xpos = float.Parse(sensorValues[1]);
                // passer de 0 - 1792 à 0 - 1
                xpos = xpos / 1792;
                // valeur de déplacement entre 0 et 2
                xpos *= 35;
                xpos += 215; // Amplitude de déplacement

                // déplacement vertical
                ypos = float.Parse(sensorValues[2]);
                // passer de 0 - 1023 à 0 - 1
                ypos = ypos / 1792;
                // valeur de déplacement entre 0 et 2
                ypos *= 27;
                ypos += 450; // Amplitude de déplacement

               // Debug.Log(xpos);
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
