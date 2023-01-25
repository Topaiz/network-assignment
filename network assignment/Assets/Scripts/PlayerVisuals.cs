using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Alteruna.Trinity;
using UnityEngine;
using Avatar = Alteruna.Avatar;

public class PlayerVisuals : AttributesSync
{
    [SerializeField] private Avatar avatar;
    [SerializeField] private SpriteRenderer body;
    private string displayName;
    private Color _color;

    void Start()
    {
        Multiplayer.RegisterRemoteProcedure(nameof(MyProcedure), MyProcedure);

        if (!avatar.IsMe)
        {
            return;
        }

        _color = new Color(
            PlayerPrefs.GetFloat(PlayerPrefsVariables.Red),
            PlayerPrefs.GetFloat(PlayerPrefsVariables.Green),
            PlayerPrefs.GetFloat(PlayerPrefsVariables.Blue));
        SetColor(_color);

        Debug.Log($"register {nameof(MyProcedure)}");
        CallMyProcedure();
    }

    void CallMyProcedure()
    {
        Debug.Log(nameof(CallMyProcedure));
        ProcedureParameters parameters = new ProcedureParameters();
        // The Alteruna documentation says you can send floats but you can't, so we use ints instead 
        parameters.Set("r", (int) Mathf.Round(_color.r * 1000));
        parameters.Set("g", (int) Mathf.Round(_color.g * 1000));
        parameters.Set("b", (int) Mathf.Round(_color.b * 1000));
        Multiplayer.InvokeRemoteProcedure(nameof(MyProcedure), UserId.All, parameters);
    }

    private void MyProcedure(ushort fromUser, ProcedureParameters parameters, uint callId,
        ITransportStreamReader processor)
    {
        Debug.Log(nameof(MyProcedure));
        // The Alteruna documentation says you can send floats but you can't, so we use ints instead 
        _color = new Color(
            parameters.Get("r", 0) / 1000f,
            parameters.Get("g", 0) / 1000f,
            parameters.Get("b", 0) / 1000f);
        SetColor(_color);
    }

    [SynchronizableMethod]
    private void SetColor(Color color)
    {
        body.color = color;
    }
}