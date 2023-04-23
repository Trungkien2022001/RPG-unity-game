using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{

    public class VertexColorCycler : MonoBehaviour
    {

        private TMP_Text m_TextComponent;

        void Awake()
        {
            m_TextComponent = GetComponent<TMP_Text>();
        }


        void Start()
        {
            StartCoroutine(AnimateVertexColors());
        }


        /// <summary>
        /// Method to animate vertex colors of a TMP Text object.
        /// </summary>
        /// <returns></returns>
        IEnumerator AnimateVertexColors()
        {
            // Force the text object to update right away so we can have geometry to modify right from the start.
            m_TextComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = m_TextComponent.textInfo;
            int currentCharacter = 0;

            Color32[] newVertexColors;
            Color32 c0 = m_TextComponent.color;

            while (true)
            {
                int characterCount = textInfo.characterCount;

                // If No Characters then just yield and wait for some text to be added
                if (characterCount == 0)
                {
                    yield return new WaitForSeconds(0.25f);
                    continue;
                }

                // Get the index of the material used by the current character.
                int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

                // Get the vertex colors of the mesh used by this text element (character or sprite).
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                // Get the index of the first vertex used by this text element.
                int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;

                // Only change the vertex color if the text element is visible.
                if (textInfo.characterInfo[currentCharacter].isVisible)
                {
                    c0 = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

                    newVertexColors[vertexIndex + 0] = c0;
                    newVertexColors[vertexIndex + 1] = c0;
                    newVertexColors[vertexIndex + 2] = c0;
                    newVertexColors[vertexIndex + 3] = c0;

                    // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                    m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                    // This last process could be done to only update the vertex data that has changed as opposed to all of the vertex data but it would require extra steps and knowing what type of renderer is used.
                    // These extra steps would be a performance optimization but it is unlikely that such optimization will be necessary.
                }

                currentCharacter = (currentCharacter + 1) % characterCount;

                yield return new WaitForSeconds(0.05f);
            }
        }

    }
}


/*
Đoạn code trên là một script được viết bằng ngôn ngữ lập trình C# trong Unity Engine. 
Script này được sử dụng để tạo hiệu ứng đổi màu chữ chạy liên tục cho văn bản của TextMeshPro (TMP_Text).

Đầu tiên, script khai báo namespace TMPro.Examples và lớp VertexColorCycler, kế thừa từ MonoBehaviour. 
Nó có một biến m_TextComponent kiểu TMP_Text để tham chiếu đến đối tượng TMP_Text được gán vào script.

Hàm Awake() được gọi khi script được khởi tạo. Nó sẽ tìm kiếm đối tượng TMP_Text gắn vào script và 
gán nó cho biến m_TextComponent thông qua hàm GetComponent().

Hàm Start() được gọi khi đối tượng được khởi tạo. 
Nó sẽ gọi IEnumerator AnimateVertexColors() bằng hàm StartCoroutine() để bắt đầu vòng lặp vô hạn của hiệu ứng đổi màu chữ.

Trong IEnumerator AnimateVertexColors(), trước khi bắt đầu vòng lặp, m_TextComponent.ForceMeshUpdate() 
được gọi để đảm bảo rằng bảng điểm của văn bản được tạo ra ngay lập tức để có thể sửa đổi từ đầu.

Trong vòng lặp, đoạn code lấy thông tin về văn bản được gán vào m_TextComponent, 
lấy ra một ký tự hiện tại và đổi màu của các đỉnh của ký tự đó.

Các màu sắc mới được tạo bằng hàm Random.Range() và được áp dụng vào tất cả bốn đỉnh của ký tự hiện tại. 
Sau đó, hàm UpdateVertexData() được gọi để cập nhật dữ liệu đỉnh của văn bản. 
Cuối cùng, currentCharacter được cập nhật để chuyển sang ký tự tiếp theo, và vòng lặp sẽ tiếp tục chạy.

Hàm WaitForSeconds() được sử dụng để chờ một khoảng thời gian nhất định trước khi tiếp tục chạy vòng lặp. 
Thời gian chờ này được thiết lập ở giá trị 0.05f cho mỗi ký tự và 0.25f nếu không có ký tự nào trong văn bản.

Vì script này được thực hiện theo cơ chế Coroutine, nó có thể được tạm dừng và tiếp tục thực hiện từ điểm dừng đó, 
đồng thời không làm đóng ứng giao diện người dùng của ứng dụng.
*/