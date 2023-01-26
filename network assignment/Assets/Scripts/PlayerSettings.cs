using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    private string _name;
    private Color _color;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private SpriteRenderer playerRepresentation;
    [SerializeField] private Multiplayer multiplayer;

    void Start()
    {
        LoadPlayerData();
        UpdateUI();
        AddInputListeners();
    }

    private void UpdatePlayerName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            newName = GetAName();
            inputField.text = newName;
        }

        _name = newName;
        PlayerPrefs.SetString(PlayerPrefsVariables.Name, _name);
    }

    private void UpdatePlayerColorRed(float val)
    {
        _color.r = val;
        PlayerPrefs.SetFloat(PlayerPrefsVariables.Red, val);
        UpdatePlayerRepresentation();
    }

    private void UpdatePlayerColorGreen(float val)
    {
        _color.g = val;
        PlayerPrefs.SetFloat(PlayerPrefsVariables.Green, val);
        UpdatePlayerRepresentation();
    }

    private void UpdatePlayerColorBlue(float val)
    {
        _color.b = val;
        PlayerPrefs.SetFloat(PlayerPrefsVariables.Blue, val);
        UpdatePlayerRepresentation();
    }

    private void UpdatePlayerRepresentation()
    {
        playerRepresentation.color = _color;
    }

    private void UpdateUI()
    {
        inputField.text = _name;
        redSlider.value = _color.r;
        greenSlider.value = _color.g;
        blueSlider.value = _color.b;
        UpdatePlayerRepresentation();
    }

    private void AddInputListeners()
    {
        inputField.onEndEdit.AddListener(UpdatePlayerName);
        redSlider.onValueChanged.AddListener(UpdatePlayerColorRed);
        greenSlider.onValueChanged.AddListener(UpdatePlayerColorGreen);
        blueSlider.onValueChanged.AddListener(UpdatePlayerColorBlue);
    }

    private void LoadPlayerData()
    {
        _name = PlayerPrefs.GetString(PlayerPrefsVariables.Name);
        if (string.IsNullOrWhiteSpace(_name))
        {
            _name = GetAName();

            PlayerPrefs.SetString(PlayerPrefsVariables.Name, _name);
        }

        _color = new Color(
            PlayerPrefs.GetFloat(PlayerPrefsVariables.Red, 1f),
            PlayerPrefs.GetFloat(PlayerPrefsVariables.Green, 1f),
            PlayerPrefs.GetFloat(PlayerPrefsVariables.Blue, 1f));
        PlayerPrefs.SetFloat(PlayerPrefsVariables.Red, _color.r);
        PlayerPrefs.SetFloat(PlayerPrefsVariables.Green, _color.g);
        PlayerPrefs.SetFloat(PlayerPrefsVariables.Blue, _color.b);
    }

    private string GetAName()
    {
        //Changed from 'string name = multplayer.Me.Name' to Below thingie
        string name = NetworkManager.Multiplayer.Me.Name;
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "Player";
        }

        return name;
    }

    private void Reset()
    {
        multiplayer = FindObjectOfType<Multiplayer>();
    }
}