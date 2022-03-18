using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (RenderTexture))]
public class FogProjector : MonoBehaviour
{
    public RenderTexture fogTexture;
    public Shader blurShader;
    [Range(1, 4)]
    public int upsample = 2;
    public float blendSpeed = 8f;
    public float updateRate = 0.01f;

    private RenderTexture prevFog, currFog;
    private Material blurMaterial;
    private Projector projector;

    float blend;
    bool blendable = true;

    private void OnEnable()
    {
        projector = transform.GetComponent<Projector>();

        if (!projector)
        {
            return;
        }

        prevFog = GetFogTexture();
        currFog = GetFogTexture();
        blurMaterial = new Material(blurShader);

        blend = 1f;
        projector.material.SetFloat("_Blend", blend);
        projector.material.SetTexture("_CurrFogTex", currFog);
        projector.material.SetTexture("_PrevFogTex", prevFog);
        Graphics.Blit(fogTexture, currFog);
        UpdateFog();
    }

    private void Start()
    {
        StartCoroutine(UpdateFogLoop(updateRate));
    }

    private void Update() {}

    public void UpdateFog()
    {
        Graphics.Blit(currFog, prevFog);
        Graphics.Blit(fogTexture, currFog);

        RenderTexture tmp = RenderTexture.GetTemporary(
            fogTexture.width,
            fogTexture.height,
            0,
            fogTexture.format
        );
        tmp.filterMode = FilterMode.Bilinear;

        Graphics.Blit(currFog, tmp, blurMaterial);
        Graphics.Blit(tmp, currFog);
        StartCoroutine(BlendTexture());

        RenderTexture.ReleaseTemporary(tmp);
    }

    RenderTexture GetFogTexture()
    {
        RenderTexture ret = new RenderTexture(
            fogTexture.width * upsample,
            fogTexture.height * upsample,
            0,
            fogTexture.format
        );
        ret.filterMode = FilterMode.Bilinear;
        ret.antiAliasing = fogTexture.antiAliasing;
        return ret;
    }

    IEnumerator BlendTexture()
    {
        blendable = false;
        blend = 0;
        projector.material.SetFloat("_Blend", blend);
        while (blend < 1)
        {
            blend = Mathf.MoveTowards(blend, 1, blendSpeed * Time.deltaTime);
            projector.material.SetFloat("_Blend", blend);
            yield return null;
        }
        blendable = true;
    }

    IEnumerator UpdateFogLoop(float delay)
    {
        while (true)
        {
            UpdateFog();
            yield return new WaitForSeconds(delay);
        }
    }
}
