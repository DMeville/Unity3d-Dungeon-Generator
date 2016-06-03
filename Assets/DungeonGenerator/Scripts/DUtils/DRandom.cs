using System;
using System.Collections;

public class DRandom  {

    public int seed = 0;
    public Random random;

    public bool init = false;
    public void Init(int _seed = 0) {
        if (!init) {
            DLogger.Log("DRandom::Initializing RNG - Seed = " + _seed);
            init = true;

            seed = _seed;
            random = new Random(seed);
        }
    }

    public float value() {
        Init(seed);
        return (float)random.NextDouble();
    }

    /// <summary>
    /// returns a random int between min and max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public int range(int min, int max) {
        return (int)((max - min + 1)*value())+min;
    }
}
