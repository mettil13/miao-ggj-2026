StructuredBuffer<float3> _SSPoints;
float _PecoroCameraFade;
float _SSPointsCount;

void GetVector_float(int index, out float3 value)
{
    value = _SSPoints[index];
}
 
void GetDist_float(int index,float3 pixelSS, out float value)
{
    value = 0;
    int cap = _SSPointsCount;
    for (int i = 0; i < cap; i++)
    {    
        float dist = saturate(1 - length(_SSPoints[i].rg - pixelSS.rg) * 1);
        value += pow(dist, 3.2);
    }
    value *= _PecoroCameraFade*1;
} 

void Pecoro_float(float3 pixelSS, out float value, out float value2)
{
    value = saturate(1 - length(pixelSS.rg - float2(0.5, 0.5)) * 2 + cos(_Time * 30 + pixelSS.rg*10) * 0.1);
    value2 = saturate(value * 1.5);
    value2 *= _PecoroCameraFade;
    value *= _PecoroCameraFade;
}
void GetPecoroCameraFade_float(out float value)
{
    value = saturate(1 - _PecoroCameraFade);
}
