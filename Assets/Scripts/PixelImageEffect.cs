using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelImageEffect : MonoBehaviour
{
    public Material EffectMaterial;
    public int divisor;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        RenderTexture RT = RenderTexture.GetTemporary(source.width / divisor, source.height/ divisor, 0, source.format);
        Graphics.Blit(source, RT);
        Graphics.Blit(RT, destination, EffectMaterial);

        RenderTexture.ReleaseTemporary(RT);
    }
}
