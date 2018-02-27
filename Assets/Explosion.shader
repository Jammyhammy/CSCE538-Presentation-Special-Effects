Shader "Custom/Explosion"
{
    Properties 
    {
        //The ramp texture is used to determine the colors of the explosion, the alpha value is used for emission.
        _RampTex ("Color Ramp", 2D) = "white" {} 
        _DispTex ("Displacement Texture", 2D) = "gray" {}
        _Displacement ("Displacement", Range(0, 1.0)) = 0.1
        _ChannelFactor ("ChannelFactor (r,g,b)", Vector) = (1,0,0)
        _Range ("Range (min,max)", Vector) = (0,0.5,0)
        _ClipRange ("ClipRange [0,1]", float) = 0.8
    }
 
    SubShader 
    {
        Tags { "RenderType"="Opaque" }
        Cull Off
        LOD 300
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:disp nolightmap
        #pragma target 3.0
        #pragma glsl
 
        sampler2D _DispTex;
        float _Displacement;
        float3 _ChannelFactor;
        float2 _Range;
        float _ClipRange;
 
        struct Input 
        {
            float2 uv_DispTex;
        };

        //We offset the vertices based on a texture lookup using the displacement texture.
        //The color we get from this contains the displacement value according to all 3 displacement textures (RGB).
        //By using the float3 property I called _ChannelFactor (that is set in a script each frame, see lower), we determine the actual displacement, by taking the sum of the 3 values multiplied by the corresponding RGB value.
        void disp (inout appdata_full v)
        {
            float3 dcolor = tex2Dlod (_DispTex, float4(v.texcoord.xy,0,0));
            float d = (dcolor.r*_ChannelFactor.r + dcolor.g*_ChannelFactor.g + dcolor.b*_ChannelFactor.b);
            v.vertex.xyz += v.normal * d * _Displacement;
        }
 
        sampler2D _RampTex;
 
        //n the Surface shader, we do the same texture lookup,
        //using that value as the UV.x (UV.y can be anything), we do another lookup using the ramp texture.
        //We assign that color as the Albedo color, and the color multiplied by the alpha as Emission.
        //We could also assign the color in the Vertex shader, but then the colors would be interpolated between the vertices, whereas if we calculate the color in the surface/fragment shader, we get a correcter result. 
        //But assigning it there is faster, as it would discard the need to do the displacement texture lookup in the surface shader again, so decide yourself what’s more important.

        void surf (Input IN, inout SurfaceOutput o) 
        {
            float3 dcolor = tex2D (_DispTex, IN.uv_DispTex);
            float d = (dcolor.r*_ChannelFactor.r + dcolor.g*_ChannelFactor.g + dcolor.b*_ChannelFactor.b) * (_Range.y-_Range.x) + _Range.x;
            //The clipping value determines at what displacement amount to clip (cut holes in the mesh), by animating this (in an AnimationClip, or in code), you can fade out the explosion over time.            
            clip (_ClipRange-d);
            half4 c = tex2D (_RampTex, float2(d,0.5));
            o.Albedo = c.rgb;
            o.Emission = c.rgb*c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}