using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{

    public class VertexShakeA : MonoBehaviour
    {

        // Đây là script VertexShakeA được sử dụng để tạo hiệu ứng rung các đỉnh của văn bản đối với đối tượng TMP_Text.
        // Script này bao gồm các biến AngleMultiplier, SpeedMultiplier, ScaleMultiplier và RotationMultiplier, được sử dụng để điều chỉnh đầu vào và hiệu ứng của văn bản.
        // Hàm Awake() được sử dụng để lấy tham chiếu đến đối tượng TMP_Text.
        // Hàm OnEnable() và OnDisable() được sử dụng để đăng ký và hủy đăng ký sự kiện TMPRO_EventManager.TEXT_CHANGED_EVENT, sự kiện sẽ được kích hoạt khi đối tượng văn bản được phục hồi.
        // Hàm Start() được sử dụng để bắt đầu Coroutine để tạo hiệu ứng rung các đỉnh.
        // Hàm ON_TEXT_CHANGED() được sử dụng để đánh dấu là văn bản được thay đổi khi TEXT_CHANGED_EVENT được kích hoạt.
        // Thông thường, script này sẽ cần thêm các hàm khác để thực hiện hiệu ứng rung các đỉnh và cập nhật văn bản, tùy thuộc vào thiết kế cụ thể của ứng dụng.
        public float AngleMultiplier = 1.0f;
        public float SpeedMultiplier = 1.0f;
        public float ScaleMultiplier = 1.0f;
        public float RotationMultiplier = 1.0f;

        private TMP_Text m_TextComponent;
        private bool hasTextChanged;


        void Awake()
        {
            m_TextComponent = GetComponent<TMP_Text>();
        }

        void OnEnable()
        {
            // Subscribe to event fired when text object has been regenerated.
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        }

        void OnDisable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        }


        void Start()
        {
            StartCoroutine(AnimateVertexColors());
        }


        void ON_TEXT_CHANGED(Object obj)
        {
            if (obj = m_TextComponent)
                hasTextChanged = true;
        }

        // Đây là hàm AnimateVertexColors được sử dụng để tạo hiệu ứng rung các đỉnh văn bản trên đối tượng TMP_Text.
        // Đầu tiên, hàm sử dụng ForceMeshUpdate để cập nhật thông tin văn bản của đối tượng TMP_Text.
        // Sau đó, hàm lấy thông tin về văn bản và tạo ma trận lưu trữ tạm thời cho các đỉnh của văn bản.
        // Hàm sử dụng vòng lặp while để thực hiện hiệu ứng rung các đỉnh.
        // Dòng lệnh "if (hasTextChanged)" được sử dụng để kiểm tra xem văn bản có bị thay đổi hay không, nếu có, hàm sẽ cấp phát lại ma trận lưu trữ tạm thời cho các đỉnh.
        // Hàm dùng để tính toán trung tâm của mỗi dòng và xoay động các ký tự của mỗi dòng, sau đó dùng ma trận để thực hiện các thay đổi trong vòng lặp. Hàm dùng để cập nhật lại cấu trúc mạng của dữ liệu đồ họa của văn bản được thiết lập trên đối tượng TMP_Text.
        // Hàm kết thúc bằng lệnh WaitForSeconds() để chờ đợi một khoảng thời gian nhất định giữa các lần rung đỉnh văn bản.
        // Hàm được thực thi liên tục cho đến khi lệnh break được thực hiện hoặc đối tượng bị tắt.
        /// <summary>
        /// Method to animate vertex colors of a TMP Text object.
        /// </summary>
        /// <returns></returns>
        IEnumerator AnimateVertexColors()
        {

            // We force an update of the text object since it would only be updated at the end of the frame. Ie. before this code is executed on the first frame.
            // Alternatively, we could yield and wait until the end of the frame when the text object will be generated.
            m_TextComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = m_TextComponent.textInfo;

            Matrix4x4 matrix;
            Vector3[][] copyOfVertices = new Vector3[0][];

            hasTextChanged = true;

            while (true)
            {
                // Allocate new vertices 
                if (hasTextChanged)
                {
                    if (copyOfVertices.Length < textInfo.meshInfo.Length)
                        copyOfVertices = new Vector3[textInfo.meshInfo.Length][];

                    for (int i = 0; i < textInfo.meshInfo.Length; i++)
                    {
                        int length = textInfo.meshInfo[i].vertices.Length;
                        copyOfVertices[i] = new Vector3[length];
                    }

                    hasTextChanged = false;
                }

                int characterCount = textInfo.characterCount;

                // If No Characters then just yield and wait for some text to be added
                if (characterCount == 0)
                {
                    yield return new WaitForSeconds(0.25f);
                    continue;
                }

                int lineCount = textInfo.lineCount;

                // Iterate through each line of the text.
                for (int i = 0; i < lineCount; i++)
                {

                    int first = textInfo.lineInfo[i].firstCharacterIndex;
                    int last = textInfo.lineInfo[i].lastCharacterIndex;

                    // Determine the center of each line
                    Vector3 centerOfLine = (textInfo.characterInfo[first].bottomLeft + textInfo.characterInfo[last].topRight) / 2;
                    Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-0.25f, 0.25f) * RotationMultiplier);

                    // Iterate through each character of the line.
                    for (int j = first; j <= last; j++)
                    {
                        // Skip characters that are not visible and thus have no geometry to manipulate.
                        if (!textInfo.characterInfo[j].isVisible)
                            continue;

                        // Get the index of the material used by the current character.
                        int materialIndex = textInfo.characterInfo[j].materialReferenceIndex;

                        // Get the index of the first vertex used by this text element.
                        int vertexIndex = textInfo.characterInfo[j].vertexIndex;

                        // Get the vertices of the mesh used by this text element (character or sprite).
                        Vector3[] sourceVertices = textInfo.meshInfo[materialIndex].vertices;

                        // Need to translate all 4 vertices of each quad to aligned with center of character.
                        // This is needed so the matrix TRS is applied at the origin for each character.
                        copyOfVertices[materialIndex][vertexIndex + 0] = sourceVertices[vertexIndex + 0] - centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 1] = sourceVertices[vertexIndex + 1] - centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 2] = sourceVertices[vertexIndex + 2] - centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 3] = sourceVertices[vertexIndex + 3] - centerOfLine;

                        // Determine the random scale change for each character.
                        float randomScale = Random.Range(0.995f - 0.001f * ScaleMultiplier, 1.005f + 0.001f * ScaleMultiplier);

                        // Setup the matrix rotation.
                        matrix = Matrix4x4.TRS(Vector3.one, rotation, Vector3.one * randomScale);

                        // Apply the matrix TRS to the individual characters relative to the center of the current line.
                        copyOfVertices[materialIndex][vertexIndex + 0] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 0]);
                        copyOfVertices[materialIndex][vertexIndex + 1] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 1]);
                        copyOfVertices[materialIndex][vertexIndex + 2] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 2]);
                        copyOfVertices[materialIndex][vertexIndex + 3] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 3]);

                        // Revert the translation change.
                        copyOfVertices[materialIndex][vertexIndex + 0] += centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 1] += centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 2] += centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 3] += centerOfLine;
                    }
                }

                // Push changes into meshes
                for (int i = 0; i < textInfo.meshInfo.Length; i++)
                {
                    textInfo.meshInfo[i].mesh.vertices = copyOfVertices[i];
                    m_TextComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}