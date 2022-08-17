using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelImageEffect : MonoBehaviour
{
    public Material EffectMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        RenderTexture RT = RenderTexture.GetTemporary(source.width/2, source.height/2, 0, source.format);
        Graphics.Blit(source, RT);
        Graphics.Blit(RT, destination, EffectMaterial);

        RenderTexture.ReleaseTemporary(RT);
    }
}
