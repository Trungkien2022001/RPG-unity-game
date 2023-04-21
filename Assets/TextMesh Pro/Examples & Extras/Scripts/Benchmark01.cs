using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{

    public class Benchmark01 : MonoBehaviour
    {
        // Lớp Benchmark01 kế thừa từ MonoBehaviour và được sử dụng để đo hiệu suất của các phần mềm đồ họa.
        // Biến BenchmarkType được sử dụng để thiết lập kiểu đo hiệu suất, mặc định là 0.
        // Biến TMProFont được sử dụng để lưu trữ phông chữ đã đặt theo dạng TextMesh Pro FontAsset. Biến TextMeshFont được sử dụng để lưu trữ phông chữ đã đặt theo dạng TextMesh.
        // Biến m_textMeshPro được sử dụng để lưu trữ đối tượng TextMeshPro, biến m_textContainer được sử dụng để lưu trữ đối tượng TextContainer của TextMeshPro và biến m_textMesh được sử dụng để lưu trữ đối tượng TextMesh.
        // Chuỗi label01 là một biến hằng chứa định dạng chuỗi cho TextMeshPro. Chuỗi label02 là một biến hằng chứa định dạng chuỗi cho TextMesh.
        // Các biến m_material01 và m_material02 được sử dụng để lưu trữ tài liệu để áp dụng cho đối tượng văn bản.
        public int BenchmarkType = 0;

        public TMP_FontAsset TMProFont;
        public Font TextMeshFont;

        private TextMeshPro m_textMeshPro;
        private TextContainer m_textContainer;
        private TextMesh m_textMesh;

        private const string label01 = "The <#0050FF>count is: </color>{0}";
        private const string label02 = "The <color=#0050FF>count is: </color>";

        //private string m_string;
        //private int m_frame;

        private Material m_material01;
        private Material m_material02;


        // Hàm IEnumerator Start() được sử dụng để bắt đầu tính toán hiệu suất.
        // Nếu biến BenchmarkType được đặt là 0, một đối tượng TextMeshPro sẽ được thêm vào đối tượng được gán cho biến gameObject. Nếu TMProFont khác null, font được sử dụng của đối tượng TextMeshPro sẽ được gán cho giá trị của biến TMProFont. Kích thước font của đối tượng TextMeshPro được đặt là 48 và canh chỉnh trong trung tâm. Biến m_material01 sẽ được sử dụng cho material mặc định của đối tượng TextMeshPro.
        // Nếu biến BenchmarkType được đặt là 1, một đối tượng TextMesh sẽ được thêm vào đối tượng được gán cho biến gameObject. Nếu biến TextMeshFont khác null, font sẽ được sử dụng của đối tượng TextMesh sẽ được gán cho giá trị của biến TextMeshFont. Kích thước font của đối tượng TextMesh được đặt là 48 và được căn giữa.
        // Cả hai loại đối tượng văn bản này sẽ được lặp lại 1000000 lần và được cập nhật với các giá trị định dạng chuỗi tương ứng với mỗi lần lặp lại để đo hiệu suất. Nếu BenchmarkType là 0, font materials được chuyển đổi giữa m_material01 và m_material02 với mỗi 1000 bước lặp lại.
        // Cuối cùng, yield return null sẽ được sử dụng để dừng việc tính toán hiệu suất.
        IEnumerator Start()
        {



            if (BenchmarkType == 0) // TextMesh Pro Component
            {
                m_textMeshPro = gameObject.AddComponent<TextMeshPro>();
                m_textMeshPro.autoSizeTextContainer = true;

                //m_textMeshPro.anchorDampening = true;

                if (TMProFont != null)
                    m_textMeshPro.font = TMProFont;

                //m_textMeshPro.font = Resources.Load("Fonts & Materials/Anton SDF", typeof(TextMeshProFont)) as TextMeshProFont; // Make sure the Anton SDF exists before calling this...
                //m_textMeshPro.fontSharedMaterial = Resources.Load("Fonts & Materials/Anton SDF", typeof(Material)) as Material; // Same as above make sure this material exists.

                m_textMeshPro.fontSize = 48;
                m_textMeshPro.alignment = TextAlignmentOptions.Center;
                //m_textMeshPro.anchor = AnchorPositions.Center;
                m_textMeshPro.extraPadding = true;
                //m_textMeshPro.outlineWidth = 0.25f;
                //m_textMeshPro.fontSharedMaterial.SetFloat("_OutlineWidth", 0.2f);
                //m_textMeshPro.fontSharedMaterial.EnableKeyword("UNDERLAY_ON");
                //m_textMeshPro.lineJustification = LineJustificationTypes.Center;
                m_textMeshPro.enableWordWrapping = false;
                //m_textMeshPro.lineLength = 60;          
                //m_textMeshPro.characterSpacing = 0.2f;
                //m_textMeshPro.fontColor = new Color32(255, 255, 255, 255);

                m_material01 = m_textMeshPro.font.material;
                m_material02 = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Drop Shadow"); // Make sure the LiberationSans SDF exists before calling this...  


            }
            else if (BenchmarkType == 1) // TextMesh
            {
                m_textMesh = gameObject.AddComponent<TextMesh>();

                if (TextMeshFont != null)
                {
                    m_textMesh.font = TextMeshFont;
                    m_textMesh.GetComponent<Renderer>().sharedMaterial = m_textMesh.font.material;
                }
                else
                {
                    m_textMesh.font = Resources.Load("Fonts/ARIAL", typeof(Font)) as Font;
                    m_textMesh.GetComponent<Renderer>().sharedMaterial = m_textMesh.font.material;
                }

                m_textMesh.fontSize = 48;
                m_textMesh.anchor = TextAnchor.MiddleCenter;

                //m_textMesh.color = new Color32(255, 255, 0, 255);
            }



            for (int i = 0; i <= 1000000; i++)
            {
                if (BenchmarkType == 0)
                {
                    m_textMeshPro.SetText(label01, i % 1000);
                    if (i % 1000 == 999)
                        m_textMeshPro.fontSharedMaterial = m_textMeshPro.fontSharedMaterial == m_material01 ? m_textMeshPro.fontSharedMaterial = m_material02 : m_textMeshPro.fontSharedMaterial = m_material01;



                }
                else if (BenchmarkType == 1)
                    m_textMesh.text = label02 + (i % 1000).ToString();

                yield return null;
            }


            yield return null;
        }


        /*
        void Update()
        {
            if (BenchmarkType == 0)
            {
                m_textMeshPro.text = (m_frame % 1000).ToString();
            }
            else if (BenchmarkType == 1)
            {
                m_textMesh.text = (m_frame % 1000).ToString();
            }

            m_frame += 1;
        }
        */
    }
}
