
texture2D image;
texture2D textureB;
texture2D alpha;

sampler2D imageSampler = sampler_state
{
  Texture=<image>;
  MinFilter=Point;
  MagFilter=Point;
  MipFilter=Point;
};

sampler2D samplerB = sampler_state
{
  Texture=<textureB>;
  MinFilter=Point;
  MagFilter=Point;
  MipFilter=Point;
};

sampler2D samplerAlpha = sampler_state
{
  Texture=<alpha>;
  MinFilter=Point;
  MagFilter=Point;
  MipFilter=Point;
};

struct VertexShaderOutput
{
    float2 UV : TEXCOORD0;
};

float4 TwoTextureAlphaMap(VertexShaderOutput input) : COLOR0
{
	float4 colorA = tex2D(imageSampler,input.UV);
	float4 colorB = tex2D(samplerB,input.UV);
	float4 alpha = tex2D(samplerAlpha,input.UV);
	float4 output;

	output = alpha.r > 0 && colorB.a > 0.5 ? colorB * colorA : colorA;

    return output;
}

float4 TextureAlphaMap(VertexShaderOutput input) : COLOR0
{
	float4 colorA = tex2D(imageSampler,input.UV);
	float4 alpha = tex2D(samplerAlpha,input.UV);
	colorA.rgb *= alpha.rgb;
	colorA.a = alpha.r;
    return colorA;
}


technique TwoTextureAlpha
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 TwoTextureAlphaMap();
    }
}

technique TextureAlpha
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 TextureAlphaMap();
    }
}
