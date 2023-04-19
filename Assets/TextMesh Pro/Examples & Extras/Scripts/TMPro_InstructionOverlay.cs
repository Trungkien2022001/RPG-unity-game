using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class TMPro_InstructionOverlay : MonoBehaviour
    {

        public enum FpsCounterAnchorPositions { TopLeft, BottomLeft, TopRight, BottomRight };

        public FpsCounterAnchorPositions AnchorPosition = FpsCounterAnchorPositions.BottomLeft;

        private const string instructions = "Camera Control - <#ffff00>Shift + RMB\n</color>Zoom - <#ffff00>Mouse wheel.";

        private TextMeshPro m_TextMeshPro;
        private TextContainer m_textContainer;
        private Transform m_frameCounter_transform;
        private Camera m_camera;

        //private FpsCounterAnchorPositions last_AnchorPosition;

        void Awake()
        {
            if (!enabled)
                return;

            m_camera = Camera.main;

            GameObject frameCounter = new GameObject("Frame Counter");
            m_frameCounter_transform = frameCounter.transform;
            m_frameCounter_transform.parent = m_camera.transform;
            m_frameCounter_transform.localRotation = Quaternion.identity;


            m_TextMeshPro = frameCounter.AddComponent<TextMeshPro>();
            m_TextMeshPro.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
            m_TextMeshPro.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Overlay");

            m_TextMeshPro.fontSize = 30;

            m_TextMeshPro.isOverlay = true;
            m_textContainer = frameCounter.GetComponent<TextContainer>();

            Set_FrameCounter_Position(AnchorPosition);
            //last_AnchorPosition = AnchorPosition;

            m_TextMeshPro.text = instructions;

        }




        void Set_FrameCounter_Position(FpsCounterAnchorPositions anchor_position)
        {

            switch (anchor_position)
            {
                case FpsCounterAnchorPositions.TopLeft:
                    //m_TextMeshPro.anchor = AnchorPositions.TopLeft;
                    m_textContainer.anchorPosition = TextContainerAnchors.TopLeft;
                    m_frameCounter_transform.position = m_camera.ViewportToWorldPoint(new Vector3(0, 1, 100.0f));
                    break;
                case FpsCounterAnchorPositions.BottomLeft:
                    //m_TextMeshPro.anchor = AnchorPositions.BottomLeft;
                    m_textContainer.anchorPosition = TextContainerAnchors.BottomLeft;
                    m_frameCounter_transform.position = m_camera.ViewportToWorldPoint(new Vector3(0, 0, 100.0f));
                    break;
                case FpsCounterAnchorPositions.TopRight:
                    //m_TextMeshPro.anchor = AnchorPositions.TopRight;
                    m_textContainer.anchorPosition = TextContainerAnchors.TopRight;
                    m_frameCounter_transform.position = m_camera.ViewportToWorldPoint(new Vector3(1, 1, 100.0f));
                    break;
                case FpsCounterAnchorPositions.BottomRight:
                    //m_TextMeshPro.anchor = AnchorPositions.BottomRight;
                    m_textContainer.anchorPosition = TextContainerAnchors.BottomRight;
                    m_frameCounter_transform.position = m_camera.ViewportToWorldPoint(new Vector3(1, 0, 100.0f));
                    break;
            }
        }
    }
}

// Đoạn code trên là một đoạn mã của một class được định nghĩa trong namespace TMPro.Examples. Class này là lớp MonoBehaviour và kế thừa từ lớp MonoBehaviour của Unity, vì vậy nó có thể được sử dụng trong các đối tượng GameObject của Unity. Nói cách khác, đoạn mã này được sử dụng để thêm một chức năng cho các đối tượng trong trò chơi được tạo bởi Unity.

// Đoạn mã này bao gồm các thành phần sau:

// Khai báo enum FpsCounterAnchorPositions để định nghĩa các vị trí mà một chữ số đếm khung hình được hiển thị trên màn hình.
// Khai báo biến AnchorPosition để lưu trữ vị trí đếm khung hình.
// Khai báo biến instructions để lưu trữ một chuỗi hướng dẫn.
// Khai báo các biến private để lưu trữ các thành phần trong đối tượng GameObject được tạo bởi class này, bao gồm: m_TextMeshPro (là một đối tượng TextMeshPro để hiển thị thông tin đếm khung hình), m_textContainer (là một đối tượng TextContainer để lưu trữ vị trí đếm khung hình), m_frameCounter_transform (là một đối tượng Transform để lưu trữ vị trí của đối tượng hiển thị đếm khung hình trên màn hình), và m_camera (là một đối tượng Camera để định vị trí đếm khung hình trên màn hình).
// Các phương thức Awake() và Set_FrameCounter_Position() để thiết lập đối tượng đếm khung hình và vị trí hiển thị của nó trên màn hình.
// Đoạn mã cũng có sử dụng các hàm được định nghĩa sẵn của Unity để tạo ra đối tượng hiển thị thông tin đếm khung hình trên màn hình.