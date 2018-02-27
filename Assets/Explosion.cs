using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//in the update loop we will determine the ChannelFactor, and assign it to the shader.
//I used 3 sin functions (1 for each channel), and made sure their sum is 1 (though this is not necessary).

//To make the explosion grow, simply animate it’s scale in an AnimationClip or in code.

//Optional, you can place a light and a smoke particle effect for more realistic explosion.
public class Explosion : MonoBehaviour
{
    public MeshRenderer renderer;
    public float loopduration;
    void Update()
    {
        float r = Mathf.Sin((Time.time / loopduration) * (2 * Mathf.PI)) * 0.5f + 0.25f;
        float g = Mathf.Sin((Time.time / loopduration + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
        float b = Mathf.Sin((Time.time / loopduration + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
        float correction = 1 / (r + g + b);
        r *= correction;
        g *= correction;
        b *= correction;
        renderer.material.SetVector("_ChannelFactor", new Vector4(r,g,b,0));    
    }

}

