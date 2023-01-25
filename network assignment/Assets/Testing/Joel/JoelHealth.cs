using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using UnityEngine.UI;

public class JoelHealth : Synchronizable {
    [SerializeField] public float MaxHealth;
    public float CurHealth;
    private float oldCurHealth;
    
    [SerializeField] private Image healthbar;
    public float HealthFill;
    private float oldHealthFill;

    private JoelRespawn respawn;
    private Alteruna.Avatar avatar;
    
    private void Awake() {
        avatar = transform.parent.GetComponent<Alteruna.Avatar>();
        CurHealth = MaxHealth;
        HealthFill = healthbar.fillAmount;
        respawn = GameObject.Find("Respawn").GetComponent<JoelRespawn>();
    }

    public override void DisassembleData(Reader reader, byte LOD)
    {
        CurHealth = reader.ReadFloat();
        oldCurHealth = CurHealth;

        HealthFill = reader.ReadFloat();
        oldHealthFill = HealthFill;
    }
    
    public override void AssembleData(Writer writer, byte LOD)
    {
        writer.Write(CurHealth);
        writer.Write(HealthFill);
    }
    
    public void TakeDamage(int damage) {
        CurHealth -= damage;
    }
    
    void Update()
    {
        healthbar.fillAmount = CurHealth / MaxHealth;
        transform.rotation = Quaternion.identity;
        transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 1);
        

        if (CurHealth != oldCurHealth) {
            oldCurHealth = CurHealth;
            Commit();
        }

        if (HealthFill != oldHealthFill) {
            oldHealthFill = HealthFill;
            Commit();
        }

        if (CurHealth <= 0) {
            //BroadcastRemoteMethod("Death");
            //InvokeRemoteMethod("");
            Death();
        }

        SyncUpdate();
    }

    [SynchronizableMethod]
    void Death() {
        if (avatar != transform.parent.GetComponent<JoelPlayer>().avatar)
            return;
        respawn.Respawn(transform.parent.gameObject, this);
    }
}
