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
    
    private void Awake() {
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
        
        if (CurHealth != oldCurHealth) {
            oldCurHealth = CurHealth;
            Commit();
        }

        if (HealthFill != oldHealthFill) {
            oldHealthFill = HealthFill;
            Commit();
        }

        if (CurHealth <= 0) {
            Death();
        }

        SyncUpdate();
    }

    void Death() {
        respawn.Respawn(transform.parent.gameObject, this);
    }
}
