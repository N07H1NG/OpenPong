using System.Collections;
using System;
using System.IO.Ports;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public  class PulleyInput : MonoBehaviour
{
    
    public static PulleyInput Instance {get; private set;}
    SerialPort serial;
    string portName = "COM3"; // Укажи правильный COM-порт!
    int baudRate = 9600;
    string latestValue;

    float AxisValue;
    public float AvgValue {get; private set;} 
    static int ValueBuffer = 5;

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    void Start()
    {
        serial = new SerialPort(portName, baudRate);
        serial.ReadTimeout = 1000;
        try {
            serial.Open();
            Debug.Log("Serial port opened");
        } catch (Exception e) {
            Debug.LogError("Could not open serial port: " + e.Message);
        }
        StartCoroutine(CheckData());
    }

    public float GetPulleyDirection(){
        return (AxisValue > 57.5) ? 1 : -1;
    }

    void OnApplicationQuit()
    {
        if (serial != null && serial.IsOpen)
            serial.Close();
    }


    IEnumerator CheckData(){
        while(true){
            if (serial != null && serial.IsOpen)
            {
                try
                {
                    string data = serial.ReadLine();
                    latestValue = data;

                    print(latestValue);
                    try
                    {
                        AxisValue = (float)Convert.ToDouble(latestValue, CultureInfo.InvariantCulture.NumberFormat);
                        AxisValue = math.clamp(AxisValue, 30, 85);
                        AvgValue = AvgValue * (1.0f - 1.0f / ValueBuffer) + AxisValue * (1.0f / ValueBuffer);
                        AvgValue = math.clamp(AvgValue, 30, 85);
                        //AvgValue = AxisValue;
                    }
                    catch (FormatException)
                    {
                        Debug.Log("Cant read");
                    }
                    
                    
                }
                catch (TimeoutException) { }
            }
            yield return null;
        }
    }
}
