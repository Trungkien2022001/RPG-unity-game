using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{

    public class VertexShakeB : MonoBehaviour
    {

        public float AngleMultiplier = 1.0f;
        public float SpeedMultiplier = 1.0f;
        public float CurveScale = 1.0f;

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
                    Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-0.25f, 0.25f));

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

                        // Determine the center point of each character at the baseline.
                        Vector3 charCenter = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;

                        // Need to translate all 4 vertices of each quad to aligned with center of character.
                        // This is needed so the matrix TRS is applied at the origin for each character.
                        copyOfVertices[materialIndex][vertexIndex + 0] = sourceVertices[vertexIndex + 0] - charCenter;
                        copyOfVertices[materialIndex][vertexIndex + 1] = sourceVertices[vertexIndex + 1] - charCenter;
                        copyOfVertices[materialIndex][vertexIndex + 2] = sourceVertices[vertexIndex + 2] - charCenter;
                        copyOfVertices[materialIndex][vertexIndex + 3] = sourceVertices[vertexIndex + 3] - charCenter;

                        // Determine the random scale change for each character.
                        float randomScale = Random.Range(0.95f, 1.05f);

                        // Setup the matrix for the scale change.
                        matrix = Matrix4x4.TRS(Vector3.one, Quaternion.identity, Vector3.one * randomScale);

                        // Apply the scale change relative to the center of each character.
                        copyOfVertices[materialIndex][vertexIndex + 0] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 0]);
                        copyOfVertices[materialIndex][vertexIndex + 1] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 1]);
                        copyOfVertices[materialIndex][vertexIndex + 2] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 2]);
                        copyOfVertices[materialIndex][vertexIndex + 3] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 3]);

                        // Revert the translation change.
                        copyOfVertices[materialIndex][vertexIndex + 0] += charCenter;
                        copyOfVertices[materialIndex][vertexIndex + 1] += charCenter;
                        copyOfVertices[materialIndex][vertexIndex + 2] += charCenter;
                        copyOfVertices[materialIndex][vertexIndex + 3] += charCenter;

                        // Need to translate all 4 vertices of each quad to aligned with the center of the line.
                        // This is needed so the matrix TRS is applied from the center of the line.
                        copyOfVertices[materialIndex][vertexIndex + 0] -= centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 1] -= centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 2] -= centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 3] -= centerOfLine;

                        // Setup the matrix rotation.
                        matrix = Matrix4x4.TRS(Vector3.one, rotation, Vector3.one);

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
//Đây là một script được viết bằng C# trong Unity. Nó thuộc về namespace TMPro.Examples và được gọi là VertexShakeB. Script này được sử dụng để tạo ra hiệu ứng rung động trên chữ. Các biến AngleMultiplier, SpeedMultiplier, CurveScale được sử dụng để điều chỉnh tốc độ và độ lớn của hiệu ứng.

// Script này kế thừa MonoBehaviour và ghi đè các phương thức OnEnable, OnDisable, Awake, Start. Nó cũng sử dụng một sự kiện được phát ra bởi TMP để đăng ký hoặc hủy đăng ký một phương thức (ON_TEXT_CHANGED) được gọi khi text object được tái tạo.

// Các phương thức Awake và OnEnable được sử dụng để lấy thông tin từ đối tượng m_TextComponent bằng cách sử dụng GetComponent<TMP_Text>(). Phương thức OnEnable cũng đăng ký phương thức ON_TEXT_CHANGED để đảm bảo rằng sự kiện này được xử lý khi có bất kỳ thay đổi nào liên quan đến text object. Phương thức OnDisable hủy đăng ký sự kiện để không còn xử lý khi đối tượng không được sử dụng nữa.

// Phương thức Start được sử dụng để bắt đầu gọi phương thức coroutine AnimateVertexColors.

// Phương thức ON_TEXT_CHANGED được sử dụng để kiểm tra xem obj đã được thay đổi chưa. Nếu obj = m_TextComponent thì biến hasTextChanged được thiết lập thành true.

// Phương thức AnimateVertexColors là một coroutine sử dụng để tạo ra hiệu ứng rung động. Trong vòng lặp vô hạn, nó lấy thông tin về text object (textInfo), tạo ra một ma trận và một mảng copyOfVertices chứa tất cả các đỉnh của text object. Trong mỗi vòng lặp, nó lặp qua từng dòng của text object, sau đó lặp qua từng ký tự của mỗi dòng đó. Nếu ký tự đó không được hiển thị (isVisible = false), thì nó bỏ qua và tiếp tục với ký tự tiếp theo. Nếu ký tự được hiển thị, nó sẽ lấy thông tin về vertex, scale, matrix và matrix multiplication, sau đó cập nhật copyOfVertices. Cuối cùng, nó gọi phương thức m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32) để cập nhật màu của các vertex. Hiệu ứng rung động được tạo ra bằng cách thay đổi vị trí, scale và màu sắc của các vertex.