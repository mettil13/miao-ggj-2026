StructuredBuffer<float3> _SSPoints;

void GetVector_float(int index, out float3 value)
{
    value = _SSPoints[index];
}
 
void GetDist_float(int index,float3 pixelSS, out float value)
{
    value = 0;
    for (int i = 0; i < 3; i++)
    {    
        float dist = saturate(1 - length(_SSPoints[i].rg - pixelSS.rg) * 1.4);
        value += pow(dist, 2);
    }

} 

float _PecoroCameraFade;

void Pecoro_float(float3 pixelSS, out float value, out float value2)
{
    value = saturate(1 - length(pixelSS.rg - float2(0.5, 0.5) )*2.4);
    value2 = saturate(value * 2);
    value2 *= _PecoroCameraFade;
    value *= _PecoroCameraFade;

}