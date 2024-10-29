using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurRenderPass : ScriptableRenderPass
{
    private Material material;
    private BlurSettings blurSettings;
    
    private RenderTargetIdentifier source;
    private RenderTargetHandle blurTex; // obsolete -> RTHandle
    private int blurTexID;

    /// <summary>
    /// Creates a blur material if blur settings are active, returning true if successful
    /// </summary>
    /// <param name="renderer"></param>
    /// <returns></returns>
    public bool Setup(ScriptableRenderer renderer)
    {
        source = renderer.cameraColorTarget; // obsolete -> cameraColorTargetHandle
        blurSettings = VolumeManager.instance.stack.GetComponent<BlurSettings>();

        // If any effect is causing errors, try changing to: RenderPassEvent.AfterRenderingPostProcessing;
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

        if (blurSettings != null && blurSettings.IsActive())
        {
            // Able to find the shader in files, no need for it in the heirarchy
            material = new Material(Shader.Find("PostProcessing/Blur"));
            return true;
        }

        return false;
    }

    /// <summary>
    /// Called each frame to create temp resources required this frame
    /// </summary>
    /// <param name="cmd">List of instructions for gpu</param>
    /// <param name="cameraTextureDescriptor">Describes size / sort of info in the texture</param>
    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
        if (blurSettings == null || !blurSettings.IsActive()) { return; }

        // Temporary Texture
        blurTexID = Shader.PropertyToID("_BlurTex");
        blurTex = new RenderTargetHandle();
        blurTex.id = blurTexID;
        cmd.GetTemporaryRT(blurTex.id, cameraTextureDescriptor);

        base.Configure(cmd, cameraTextureDescriptor);
    }

    /// <summary>
    /// Core bulk of the code, once per frame, sets up shader properties, applies shader to camera
    /// </summary>
    /// <param name="context">Conduit for passing instructions to renderer</param>
    /// <param name="renderingData"></param>
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (blurSettings == null || !blurSettings.IsActive()) { return; }

        // Profiler tracks performance of the code
        CommandBuffer cmd = CommandBufferPool.Get("Blur Post Process");

        // Finds the grid size, makes sure there is a central pixel
        int gridSize = Mathf.CeilToInt(blurSettings.strength.value * 6.0f);

        if (gridSize % 2 == 0)
        {
            gridSize++;
        }

        material.SetInteger("_GridSize", gridSize);
        material.SetFloat("_Spread", blurSettings.strength.value);

        // Executes effect of material with two passes 
        cmd.Blit(source, blurTex.id, material, 0);
        cmd.Blit(blurTex.id, source, material, 1);
        context.ExecuteCommandBuffer(cmd);

        cmd.Clear();
        CommandBufferPool.Release(cmd);
    }

    /// <summary>
    /// Cleans up temporary textures each frame
    /// </summary>
    /// <param name="cmd"></param>
    /// 
    /// Not used often in Unity, but some edge cases
    public override void FrameCleanup(CommandBuffer cmd)
    {
        cmd.ReleaseTemporaryRT(blurTexID);
        base.FrameCleanup(cmd);
    }
}
