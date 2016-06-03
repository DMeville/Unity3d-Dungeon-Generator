using System.Collections;
using UnityEngine;

public class DTexture {

    public const int SCALEMODE_POINT = 0;
    public const int SCALEMODE_BILINEAR = 1;

    public int width;
    public int height;
    public float[] colors;

    public DTexture(int _width, int _height) {
        this.width = _width;
        this.height = _height;
        colors = new float[width * height];
    }

    public void SetPixel(int _x, int _y, float color) {
        colors[(_y * (int)this.width) + _x] = color;
    }

    public float GetPixel(int _x, int _y) {
        if (_x < 0 || _x >= width || _y < 0 || _y >= height) {
            return 0f;
        }
        return colors[(_y * (int)this.width) + _x];
    }

    public UnityEngine.Texture2D CreateTexture2D() {
        UnityEngine.Texture2D tex = new UnityEngine.Texture2D(this.width, this.height);
        UnityEngine.Color[] c = new UnityEngine.Color[colors.Length];
        for (int i = 0; i < colors.Length; i++) {
            c[i].r = colors[i];
            c[i].g = colors[i];
            c[i].b = colors[i];
            c[i].a = 1;
        }
        tex.SetPixels(c);
        tex.Apply();
        return tex;
    }

    public DTexture Scale2(int _scaleMode = SCALEMODE_POINT) {
        if (_scaleMode == SCALEMODE_POINT) {
            return PointScale2();
        } else {
            return BilinearScale2();
        }
    }

    private DTexture PointScale2() {
        DTexture tex = new DTexture(this.width*2, this.width*2);
        for (int x = 0; x < this.width; x++) {
            for (int y = 0; y < this.height; y++) {
                tex.SetPixel((x * 2), (y * 2), this.GetPixel(x, y));
                tex.SetPixel((x * 2)+1, (y * 2), this.GetPixel(x, y));
                tex.SetPixel((x * 2), (y * 2)+1, this.GetPixel(x, y));
                tex.SetPixel((x * 2) + 1, (y * 2) + 1, this.GetPixel(x, y));
            }
        }
        return tex;
    }

    private DTexture BilinearScale2() {
        DTexture tex = new DTexture(this.width * 2, this.width * 2);
        for (int x = 0; x < tex.width; x++) {
            for (int y = 0; y < tex.height; y++) {
                //sample and average the four colours around this pixel
                float v = 0;
                v += 0.25f * this.GetPixel(Mathf.FloorToInt((float)(x - 1) / 2f), Mathf.FloorToInt((float)(y) / 2f));
                v += 0.25f * this.GetPixel(Mathf.FloorToInt((float)(x) / 2f), Mathf.FloorToInt((float)(y - 1) / 2f));
                v += 0.25f * this.GetPixel(Mathf.FloorToInt((float)(x) / 2f), Mathf.FloorToInt((float)(y + 1) / 2f));
                v += 0.25f * this.GetPixel(Mathf.FloorToInt((float)(x + 1) / 2f), Mathf.FloorToInt((float)(y) / 2f));
                tex.SetPixel(x, y, v);
            }
        }

        return tex;
    }
}
