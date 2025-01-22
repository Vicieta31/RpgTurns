using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using TMPro;
using System;
using System.Buffers.Text;

public class CalcularSpeedTurnos : MonoBehaviour
{
    public TextMeshProUGUI pepe;

    public List<int> Speeds;
    private List<KeyValuePair<int, float>> AvCharacter = new List<KeyValuePair<int, float>>();
    private Dictionary<int, float> BaseAVs = new Dictionary<int, float>(); // Guardar valores base de acción

    int AV_Div = 10000;
    private bool various0Av = false;
    private int CurrentTurnPlayer = -1;

    void Start()
    {
        for (int i = 0; i < Speeds.Count(); i++)
        {
            int temp = AV_Div / Speeds.ElementAt(i);
            AvCharacter.Add(new KeyValuePair<int, float>(i, temp));
            BaseAVs[i] = temp;
            Debug.Log(temp);
        }
        AvCharacter = AvCharacter.OrderBy(pair => pair.Value).ToList();

        ShowAV();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (various0Av == false)
            {
                NextTurn();
            }
            else
            {

            }
        }
    }

    void NextTurn()
    {
        pepe.text = "";
        float tempAV = AvCharacter[0].Value;
        CurrentTurnPlayer = AvCharacter[0].Key;
        float newValue = 0;

        for (int i = 0; i < AvCharacter.Count; i++)
        {
            var pair = AvCharacter[i];
            newValue = pair.Value - tempAV;
            
            AvCharacter[i] = new KeyValuePair<int, float>(pair.Key, newValue);
        }
        if (AvCharacter[0].Value == 0)
        {
            newValue = BaseAVs[AvCharacter[0].Key]; // Reiniciar al valor base
            AvCharacter[0] = new KeyValuePair<int, float>(AvCharacter[0].Key, newValue);
        }

        AvCharacter = AvCharacter.OrderBy(pair => pair.Value).ToList();

        // Mostrar quién tiene el turno actual
        ShowAV();
    }


    void ShowAV()
    {
        pepe.text = "";
        pepe.text = $"<b>Turno del jugador: {CurrentTurnPlayer}</b>\n" + pepe.text;
        foreach (var pair in AvCharacter)
        {
            pepe.text += $"Player {pair.Key}: {pair.Value}\n";
        }
    }
}
