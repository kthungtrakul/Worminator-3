public static class CalculateFloatBar
{
    public static float Compute(float x, float inMin, float inMax, float outMin, float outMax)
    {
        /*
         * x = current ammo
         * inMin = minimum ammo (e.g. 0)
         * inMax = maximum ammo
         * outMin = min X position (no ammo)
         * outMax = max X position (max ammo)
         */
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
