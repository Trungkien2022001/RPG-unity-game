using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.LowLevel;


namespace TMPro.Examples
{

    public class Benchmark03 : MonoBehaviour
    {
        public enum BenchmarkType { TMP_SDF_MOBILE = 0, TMP_SDF__MOBILE_SSD = 1, TMP_SDF = 2, TMP_BITMAP_MOBILE = 3, TEXTMESH_BITMAP = 4 }

        public int NumberOfSamples = 100;
        public BenchmarkType Benchmark;

        public Font SourceFont;


        void Awake()
        {

        }


        void Start()
        {
            TMP_FontAsset fontAsset = null;

            // Create Dynamic Font Asset for the given font file.
            switch (Benchmark)
            {
                case BenchmarkType.TMP_SDF_MOBILE:
                    fontAsset = TMP_FontAsset.CreateFontAsset(SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic);
                    break;
                case BenchmarkType.TMP_SDF__MOBILE_SSD:
                    fontAsset = TMP_FontAsset.CreateFontAsset(SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic);
                    fontAsset.material.shader = Shader.Find("TextMeshPro/Mobile/Distance Field SSD");
                    break;
                case BenchmarkType.TMP_SDF:
                    fontAsset = TMP_FontAsset.CreateFontAsset(SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic);
                    fontAsset.material.shader = Shader.Find("TextMeshPro/Distance Field");
                    break;
                case BenchmarkType.TMP_BITMAP_MOBILE:
                    fontAsset = TMP_FontAsset.CreateFontAsset(SourceFont, 90, 9, GlyphRenderMode.SMOOTH, 256, 256, AtlasPopulationMode.Dynamic);
                    break;
            }

            for (int i = 0; i < NumberOfSamples; i++)
            {
                switch (Benchmark)
                {
                    case BenchmarkType.TMP_SDF_MOBILE:
                    case BenchmarkType.TMP_SDF__MOBILE_SSD:
                    case BenchmarkType.TMP_SDF:
                    case BenchmarkType.TMP_BITMAP_MOBILE:
                        {
                            GameObject go = new GameObject();
                            go.transform.position = new Vector3(0, 1.2f, 0);

                            TextMeshPro textComponent = go.AddComponent<TextMeshPro>();
                            textComponent.font = fontAsset;
                            textComponent.fontSize = 128;
                            textComponent.text = "@";
                            textComponent.alignment = TextAlignmentOptions.Center;
                            textComponent.color = new Color32(255, 255, 0, 255);

                            if (Benchmark == BenchmarkType.TMP_BITMAP_MOBILE)
                                textComponent.fontSize = 132;

                        }
                        break;
                    case BenchmarkType.TEXTMESH_BITMAP:
                        {
                            GameObject go = new GameObject();
                            go.transform.position = new Vector3(0, 1.2f, 0);

                            TextMesh textMesh = go.AddComponent<TextMesh>();
                            textMesh.GetComponent<Renderer>().sharedMaterial = SourceFont.material;
                            textMesh.font = SourceFont;
                            textMesh.anchor = TextAnchor.MiddleCenter;
                            textMesh.fontSize = 130;

                            textMesh.color = new Color32(255, 255, 0, 255);
                            textMesh.text = "@";
                        }
                        break;
                }
            }
        }

    }
}

/*
Đây là một đoạn mã sử dụng trong Unity Engine để thực hiện một bài kiểm tra hiệu suất về hiển thị chữ trên màn hình. 
Trong đoạn mã này, có một class được tạo ra để thực hiện bài kiểm tra, nó được đặt tên là Benchmark03.
Class này có một số thuộc tính, bao gồm NumberOfSamples để chỉ số lần lặp lại thực hiện bài kiểm tra và Benchmark để lựa chọn kiểu kiểm tra.

Trong phần Start() của class, đoạn mã sẽ tạo ra một Font Asset mới dựa trên font đầu vào được chỉ định trong SourceFont, 
sau đó sẽ lặp lại kiểm tra một số lần tương ứng với NumberOfSamples được chỉ định. 
Trong mỗi lần kiểm tra, đoạn mã sẽ tạo ra một GameObject mới, 
sau đó sử dụng đối tượng TextMeshPro hoặc TextMesh để hiển thị chữ trên màn hình. 
Cách thức hiển thị chữ trên màn hình sẽ được lựa chọn dựa trên giá trị của thuộc tính Benchmark. 
Nếu Benchmark được lựa chọn là TEXTMESH_BITMAP thì chữ sẽ được hiển thị bằng cách sử dụng đối tượng TextMesh. 
Các giá trị khác của Benchmark sẽ sử dụng đối tượng TextMeshPro để hiển thị chữ trên màn hình.
*/