using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


namespace TMPro.Examples
{

    public class TMP_ExampleScript_01 : MonoBehaviour
    {
        public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

        public objectType ObjectType;
        public bool isStatic;

        private TMP_Text m_text;

        //private TMP_InputField m_inputfield;


        private const string k_label = "The count is <#0080ff>{0}</color>";
        private int count;

        void Awake()
        {
            // Get a reference to the TMP text component if one already exists otherwise add one.
            // This example show the convenience of having both TMP components derive from TMP_Text. 
            if (ObjectType == 0)
                m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            else
                m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

            // Load a new font asset and assign it to the text object.
            m_text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Anton SDF");

            // Load a new material preset which was created with the context menu duplicate.
            m_text.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Anton SDF - Drop Shadow");

            // Set the size of the font.
            m_text.fontSize = 120;

            // Set the text
            m_text.text = "A <#0080ff>simple</color> line of text.";

            // Get the preferred width and height based on the supplied width and height as opposed to the actual size of the current text container.
            Vector2 size = m_text.GetPreferredValues(Mathf.Infinity, Mathf.Infinity);

            // Set the size of the RectTransform based on the new calculated values.
            m_text.rectTransform.sizeDelta = new Vector2(size.x, size.y);
        }


        void Update()
        {
            if (!isStatic)
            {
                m_text.SetText(k_label, count % 1000);
                count += 1;
            }
        }

    }
}
// Đây là script TMP_ExampleScript_01 được sử dụng để cập nhật và tùy chỉnh văn bản trên các đối tượng TMP_Text hoặc TMP_TextMeshProUGUI.
// Script này bao gồm hai biến ObjectType và isStatic. Biến ObjectType được sử dụng để xác định đối tượng sẽ sử dụng đối tượng TMP_Text hoặc TMP_TextMeshProUGUI. Biến isStatic được sử dụng để xác định xem văn bản có thay đổi động hay không.
// Trong hàm Awake(), script được sử dụng để thiết lập văn bản và tùy chỉnh hoạt động của đối tượng TMP_Text hoặc TMP_TextMeshProUGUI. Hàm Load a new font asset được sử dụng để tải một phông chữ mới từ tài nguyên và gán nó vào đối tượng văn bản. Hàm Load a new material preset được sử dụng để tải một chất liệu này từ tài nguyên và gán nó vào đối tượng văn bản. Hàm Set the size of the font được sử dụng để thiết lập kích thước của phông chữ. Hàm Set the text được sử dụng để thiết lập nội dung văn bản. Hàm Get the preferred width and height được sử dụng để tính toán kích thước của đối tượng văn bản dựa trên chiều rộng và chiều cao được cung cấp và cập nhật kích thước của đối tượng RectTransform dựa trên giá trị này.
// Trong hàm Update(), nếu isStatic được thiết lập thành false, script sẽ tăng biến count thêm 1 và cập nhật văn bản sử dụng hàm SetText.
