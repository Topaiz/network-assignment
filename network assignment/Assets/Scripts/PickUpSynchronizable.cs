using UnityEngine;
using Alteruna;

/// <summary>
/// Class <c>ExampleSynchronizable</c> is an example of how a <c>Synchronizable</c> can be defined.
/// </summary>
/// 
public class PickUpSynchronizable : Synchronizable
{
    public int level;
    private int oldLevel;

    //private SpriteRenderer spriteRenderer;

    //public float Score = 0f;

    public override void DisassembleData(Reader reader, byte LOD)
    {
        //base.DisassembleData(reader, LOD);
        level = reader.ReadInt();

        oldLevel = level;

        gameObject.GetComponentInParent<PickUp>().PickUpConstructor(level);
    }

    public override void AssembleData(Writer writer, byte LOD)
    {
        //base.AssembleData(writer, LOD);
        writer.Write(level);
    }

    private void Update()
    {
        //base.Update();

        level = gameObject.GetComponentInParent<PickUp>().level;
        if (level != oldLevel)
        {
            oldLevel = level;

            Commit();
        }

        base.SyncUpdate();
    }
}
