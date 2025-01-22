using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using TMPro;
using System;

public class CalcularSpeedTurnos : MonoBehaviour
{
    public TextMeshProUGUI pepe;

    public List<int> Speeds;
    List<float> AVInitial = new List<float>();

    int AV_Div = 10000;

    void Start()
    {
        for (int i = 0; i < Speeds.Last(); i++)
        {
            int temp = AV_Div / Speeds.ElementAt(i);
            AVInitial.Add(temp);
            Debug.Log(temp);
            pepe.text += "\nPlayer " + i + ": " + temp;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
