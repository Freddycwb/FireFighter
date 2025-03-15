using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class PuddleManager : MonoBehaviour
{
    public static PuddleManager instance;

    private GraphicsBuffer buffer;
    private Vector4[] puddles = new Vector4[5];

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }

        buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, puddles.Length, Marshal.SizeOf<Vector4>());
        buffer.SetData(puddles);
        Shader.SetGlobalBuffer("_PuddleEffects", buffer);
    }

    public int AddPuddle(Vector3 position, float radius)
    {
        int i = 0;
        for (; i < puddles.Length; ++i)
        {
            if (puddles[i] == Vector4.zero) break;
            else Debug.Log(puddles[i]);
        }
        if (i >= puddles.Length) return i;

        puddles[i] = position;
        puddles[i].w = radius;

        buffer.SetData(puddles, i, i, puddles.Length);

        return i;
    }

    public void RemovePuddle(int index)
    {
        if (index < 0 || index >= puddles.Length) return;
        puddles[index] = Vector4.zero;
        buffer.SetData(puddles, index, index, puddles.Length);
    }

    private void OnDestroy()
    {
        buffer.Dispose();
    }
}
