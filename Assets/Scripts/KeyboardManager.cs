using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class KeyboardManager : MonoBehaviour
{

    public string port = "/dev/cu.HC-06-SPPDev";
    private SerialPort puerto;

    private string _inputValue;

    public bool running = false;
    public bool calibrated = false;
    public bool debugOn = false;
    private bool activate = false;

    void StartListenPort()
    {
        puerto = new SerialPort(port, 9600);
        puerto.Open();
        puerto.ReadTimeout = 1;
        activate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate) parseInput();
    }

    void parseInput()
    {
        _inputValue = getInputData();
        
        string[] paramsArray = _inputValue.Split(char.Parse("\t"));
        
        // Si el control esta calibrandose
        if (paramsArray.Length == 1 && paramsArray[0].Equals("C"))
        {
            calibrated = false;
        } else if (paramsArray.Length > 1 && paramsArray[0].Equals("K"))
        {
            calibrated = true;
        }

        if (calibrated && paramsArray.Length > 1)
        {
            if (paramsArray[1].Equals("R"))
            {
                running = true;
            }
            else if (paramsArray[1].Equals("D"))
            {
                running = false;
            }
        }

        if (debugOn)
        {
            for (int i = 0; i < paramsArray.Length; i++)
            {
                Debug.Log(i + " value " + paramsArray[i]);
            }
        }
        

    }

    string getInputData()
    {
        if (puerto.IsOpen)
        {
            try
            {
                return puerto.ReadLine();
            }
            catch (System.Exception)
            {

            }
        }

        return "";
    }
}