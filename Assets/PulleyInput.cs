using System.Collections;
using System;
using System.IO.Ports;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

public  class PulleyInput : MonoBehaviour
{
    
    public static PulleyInput Instance {get; private set;}
    SerialPort serial;
    string portName = "COM3"; // Укажи правильный COM-порт!
    int baudRate = 9600;
    string latestValue;

    float AxisValue;

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
        serial.ReadTimeout = 5;
        try {
            serial.Open();
            Debug.Log("Serial port opened");
        } catch (Exception e) {
            Debug.LogError("Could not open serial port: " + e.Message);
        }
        StartCoroutine(CheckData());
    }

    public float GetPulleyDirection(){
        return (AxisValue > 50) ? 1 : -1;
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
                    Debug.Log("Received: " + data);
                    print(latestValue);
                    try
                    {
                        AxisValue = (float) Convert.ToDouble(latestValue,CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch(FormatException){}
                    
                    
                }
                catch (TimeoutException) { }
            }
            yield return null;
        }
    }
}
