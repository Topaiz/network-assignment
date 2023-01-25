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
    private bool _colorIsSet = false;

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
        CallMyProcedure((ushort) UserId.All);

        NetworkManager.Multiplayer.OtherUserJoined.AddListener(CallMyProcedureFromListener);
    }

    void CallMyProcedureFromListener(Multiplayer m, User u)
    {
        Debug.Log($"User {u.Index} joined");
        //CallMyProcedure(u.Index);
        StartCoroutine(asd(u.Index));
    }

    IEnumerator asd(ushort userId) // Coroutines are evil, not optimal but kinda works
    {
        yield return new WaitForSeconds(.1f);
        CallMyProcedure(userId);
    }

    void CallMyProcedure(ushort targetUser)
    {
        Debug.Log($"Senting {_color} to user {targetUser} my ID: {GetComponent<UniqueID>().UIDString}");
        ProcedureParameters parameters = new ProcedureParameters();
        // The Alteruna documentation says you can send floats but you can't, so we use ints instead 
        parameters.Set("r", (int) Mathf.Round(_color.r * 1000));
        parameters.Set("g", (int) Mathf.Round(_color.g * 1000));
        parameters.Set("b", (int) Mathf.Round(_color.b * 1000));
        Multiplayer.InvokeRemoteProcedure(nameof(MyProcedure), targetUser, parameters);
    }

    private void MyProcedure(ushort fromUser, ProcedureParameters parameters, uint callId,
        ITransportStreamReader processor)
    {
        // The Alteruna documentation says you can send floats but you can't, so we use ints instead 
        _color = new Color(
            parameters.Get("r", 0) / 1000f,
            parameters.Get("g", 0) / 1000f,
            parameters.Get("b", 0) / 1000f);
        SetColor(_color);
        Debug.Log($"Recieved {_color} from user {fromUser} my ID: {GetComponent<UniqueID>().UIDString}");
    }

    [SynchronizableMethod]
    private void SetColor(Color color)
    {
        if (!_colorIsSet) // Not optimal but it works
        {
            body.color = color;
            _colorIsSet = true;
        }
    }
}