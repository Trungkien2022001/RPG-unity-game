using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class SimpleScript : MonoBehaviour
    {

        private TextMeshPro m_textMeshPro;
        //private TMP_FontAsset m_FontAsset;

        private const string label = "The <#0050FF>count is: </color>{0:2}";
        private float m_frame;


        void Start()
        {
            // Add new TextMesh Pro Component
            m_textMeshPro = gameObject.AddComponent<TextMeshPro>();

            m_textMeshPro.autoSizeTextContainer = true;

            // Load the Font Asset to be used.
            //m_FontAsset = Resources.Load("Fonts & Materials/LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
            //m_textMeshPro.font = m_FontAsset;

            // Assign Material to TextMesh Pro Component
            //m_textMeshPro.fontSharedMaterial = Resources.Load("Fonts & Materials/LiberationSans SDF - Bevel", typeof(Material)) as Material;
            //m_textMeshPro.fontSharedMaterial.EnableKeyword("BEVEL_ON");
            
            // Set various font settings.
            m_textMeshPro.fontSize = 48;

            m_textMeshPro.alignment = TextAlignmentOptions.Center;
            
            //m_textMeshPro.anchorDampening = true; // Has been deprecated but under consideration for re-implementation.
            //m_textMeshPro.enableAutoSizing = true;

            //m_textMeshPro.characterSpacing = 0.2f;
            //m_textMeshPro.wordSpacing = 0.1f;

            //m_textMeshPro.enableCulling = true;
            m_textMeshPro.enableWordWrapping = false;

            //textMeshPro.fontColor = new Color32(255, 255, 255, 255);
        }


        void Update()
        {
            m_textMeshPro.SetText(label, m_frame % 1000);
            m_frame += 1 * Time.deltaTime;
        }

    }
}

// Đây là một đoạn code đơn giản cho việc hiển thị một chuỗi văn bản có độ dài thay đổi theo thời gian trong một TextMeshPro Component của Unity.

// Cụ thể, đoạn code sẽ tạo ra một đối tượng TextMeshPro và đặt các thuộc tính của nó, bao gồm cỡ chữ, căn chỉnh văn bản, và chế độ bọc từ. Mỗi khung hình (frame) trong Update() sẽ cập nhật đối tượng TextMeshPro để hiển thị một chuỗi văn bản được định dạng bằng chuỗi format label và giá trị của biến m_frame.

// Để làm được điều này, đoạn code sử dụng phương thức SetText() để thiết lập giá trị của đối tượng TextMeshPro bằng cách chuyển chuỗi format label và giá trị của biến m_frame dưới dạng đối số. Ngoài ra, mỗi khung hình, biến m_frame được tăng lên với giá trị delta time để tạo hiệu ứng chuỗi văn bản có độ dài thay đổi theo thời gian.