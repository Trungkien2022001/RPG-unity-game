using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class TMP_UiFrameRateCounter : MonoBehaviour
    {
        // Đây là lớp TMP_UiFrameRateCounter, cung cấp tính năng hiển thị FPS (khung hình/giây) trên giao diện người dùng.
        // Các biến và thành phần:
        // Biến UpdateInterval (kiểu float) để xác định khoảng thời gian cập nhật FPS, mặc định là 5.0 giây.
        // Biến m_LastInterval (kiểu float) là thời điểm thời gian trước đó FPS được cập nhật.
        // Biến m_Frames (kiểu int) là số khung hình đã vẽ từ thời điểm cuối cùng FPS được cập nhật.
        // Enum FpsCounterAnchorPositions cho phép chọn vị trí điểm neo để hiển thị FPS trên giao diện người dùng: TopLeft, BottomLeft, TopRight, BottomRight.
        // Biến AnchorPosition (kiểu FpsCounterAnchorPositions) để lưu vị trí điểm neo được chọn.
        // Biến htmlColorTag (kiểu string) chứa mã màu HTML được sử dụng để định dạng chữ và màu sắc FPS.
        // Biến fpsLabel (kiểu string) là chuỗi định dạng để hiển thị FPS trên màn hình.
        // Thành phần m_TextMeshPro là một đối tượng TextMeshProUGUI được sử dụng để hiển thị chữ.
        // Thành phần m_frameCounter_transform là một đối tượng RectTransform được sử dụng để thực hiện các phép biến đổi vị trí của đối tượng TMP_UiFrameRateCounter trên giao diện.
        // Biến last_AnchorPosition (kiểu FpsCounterAnchorPositions) được sử dụng để lưu vị trí điểm neo trước đó.
        public float UpdateInterval = 5.0f;
        private float m_LastInterval = 0;
        private int m_Frames = 0;

        public enum FpsCounterAnchorPositions { TopLeft, BottomLeft, TopRight, BottomRight };

        public FpsCounterAnchorPositions AnchorPosition = FpsCounterAnchorPositions.TopRight;

        private string htmlColorTag;
        private const string fpsLabel = "{0:2}</color> <#8080ff>FPS \n<#FF8000>{1:2} <#8080ff>MS";

        private TextMeshProUGUI m_TextMeshPro;
        private RectTransform m_frameCounter_transform;

        private FpsCounterAnchorPositions last_AnchorPosition;

        // Hàm Awake được gọi khi đối tượng được khởi tạo.
        // Trong hàm này:
        // Kiểm tra xem thành phần được kích hoạt hay không, nếu không thực hiện không gì cả.
        // Tìm ra FPS tối đa của ứng dụng, của đối tượng FPS Counter, và đặt FPS tối đa của ứng dụng là 1000 FPS.
        // Tạo một đối tượng mới, đặt tên là "Frame Counter", và thêm một thành phần RectTransform cho nó.
        // Gán đối tượng cha cho thành phần RectTransform của đối tượng Frame Counter, bằng cách sử dụng phương thức SetParent của RectTransform.
        // Thêm một thành phần TextMeshProUGUI và phát triển vòi phun cho đối tượng Frame Counter đó.
        // Gán font và vật liệu chia sẻ vào thành phần TextMeshPro.
        // Vô hiệu hóa tính năng xếp chữ.
        // Thiết lập kích thước phông chữ.
        // Thiết lập isOverlay thành true để cho phép đối tượng Frame Counter hiện trên giao diện người dùng.
        // Đặt vị trí ban đầu của Frame Counter bằng cách sử dụng hàm Set_FrameCounter_Position với tham số AnchorPosition và lưu AnchorPosition vào biến last_AnchorPosition.
        void Awake()
        {
            if (!enabled)
                return;

            Application.targetFrameRate = 1000;

            GameObject frameCounter = new GameObject("Frame Counter");
            m_frameCounter_transform = frameCounter.AddComponent<RectTransform>();

            m_frameCounter_transform.SetParent(this.transform, false);

            m_TextMeshPro = frameCounter.AddComponent<TextMeshProUGUI>();
            m_TextMeshPro.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
            m_TextMeshPro.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Overlay");

            m_TextMeshPro.enableWordWrapping = false;
            m_TextMeshPro.fontSize = 36;

            m_TextMeshPro.isOverlay = true;

            Set_FrameCounter_Position(AnchorPosition);
            last_AnchorPosition = AnchorPosition;
        }

        // Hàm Start được gọi khi đối tượng được kích hoạt và bắt đầu.
        // Trong hàm này:
        // Thiết lập giá trị cho biến m_LastInterval bằng cách sử dụng hàm realtimeSinceStartup của lớp Time để lấy thời gian thực của GameManager.
        // Thiết lập giá trị ban đầu cho biến m_Frames là 0.
        void Start()
        {
            m_LastInterval = Time.realtimeSinceStartup;
            m_Frames = 0;
        }


        // Hàm Update được gọi mỗi khung hình.
        // Trong hàm này:
        // Kiểm tra xem vị trí điểm neo được chọn có khác với vị trí điểm neo trước đó không. Nếu đúng, gọi hàm Set_FrameCounter_Position với tham số AnchorPosition để đặt vị trí mới cho Frame Counter.
        // Lưu vị trí điểm neo hiện tại vào biến last_AnchorPosition.
        // Tăng m_Frames lên 1.
        // Lấy thời điểm thực hiện của hàm realtimeSinceStartup.
        // Nếu thời gian thực hiện hiện tại lớn hơn thời điểm cập nhật cuối cùng (m_LastInterval) cộng với khoảng thời gian cập nhật (UpdateInterval), tính toán FPS và thời gian mà một khung hình được vẽ (ms).
        // Thiết lập giá trị cho biến htmlColorTag bằng cách sử dụng các điều kiện IF, cắt các mục FPS với màu sắc khác nhau để hiển thị trên giao diện người dùng.
        // Sử dụng phương thức SetText của đối tượng TextMeshProUGUI để đặt chuỗi hiển thị FPS trên đối tượng Frame Counter.
        // Thiết lập giá trị ban đầu cho biến m_Frames bằng 0 và cập nhật giá trị thời điểm cuối cùng m_LastInterval thành thời điểm hiện tại timeNow.
        void Update()
        {
            if (AnchorPosition != last_AnchorPosition)
                Set_FrameCounter_Position(AnchorPosition);

            last_AnchorPosition = AnchorPosition;

            m_Frames += 1;
            float timeNow = Time.realtimeSinceStartup;

            if (timeNow > m_LastInterval + UpdateInterval)
            {
                // display two fractional digits (f2 format)
                float fps = m_Frames / (timeNow - m_LastInterval);
                float ms = 1000.0f / Mathf.Max(fps, 0.00001f);

                if (fps < 30)
                    htmlColorTag = "<color=yellow>";
                else if (fps < 10)
                    htmlColorTag = "<color=red>";
                else
                    htmlColorTag = "<color=green>";

                m_TextMeshPro.SetText(htmlColorTag + fpsLabel, fps, ms);

                m_Frames = 0;
                m_LastInterval = timeNow;
            }
        }

        // Hàm Set_FrameCounter_Position được sử dụng để đặt vị trí cho đối tượng Frame Counter.
        // Trong hàm này:
        // Sử dụng switch-case với tham số anchor_position để xác định vị trí điểm neo của Frame Counter.
        // Thiết lập giá trị liên quan đến vị trí cho thành phần RectTransform của Frame Counter, bao gồm: tùy chọn căn chỉnh văn bản (alignment), pivot, anchorMin, anchorMax và anchoredPosition.
        // Các giá trị này được thiết lập để cho phù hợp với vị trí của điểm neo được chọn.
        void Set_FrameCounter_Position(FpsCounterAnchorPositions anchor_position)
        {
            switch (anchor_position)
            {
                case FpsCounterAnchorPositions.TopLeft:
                    m_TextMeshPro.alignment = TextAlignmentOptions.TopLeft;
                    m_frameCounter_transform.pivot = new Vector2(0, 1);
                    m_frameCounter_transform.anchorMin = new Vector2(0.01f, 0.99f);
                    m_frameCounter_transform.anchorMax = new Vector2(0.01f, 0.99f);
                    m_frameCounter_transform.anchoredPosition = new Vector2(0, 1);
                    break;
                case FpsCounterAnchorPositions.BottomLeft:
                    m_TextMeshPro.alignment = TextAlignmentOptions.BottomLeft;
                    m_frameCounter_transform.pivot = new Vector2(0, 0);
                    m_frameCounter_transform.anchorMin = new Vector2(0.01f, 0.01f);
                    m_frameCounter_transform.anchorMax = new Vector2(0.01f, 0.01f);
                    m_frameCounter_transform.anchoredPosition = new Vector2(0, 0);
                    break;
                case FpsCounterAnchorPositions.TopRight:
                    m_TextMeshPro.alignment = TextAlignmentOptions.TopRight;
                    m_frameCounter_transform.pivot = new Vector2(1, 1);
                    m_frameCounter_transform.anchorMin = new Vector2(0.99f, 0.99f);
                    m_frameCounter_transform.anchorMax = new Vector2(0.99f, 0.99f);
                    m_frameCounter_transform.anchoredPosition = new Vector2(1, 1);
                    break;
                case FpsCounterAnchorPositions.BottomRight:
                    m_TextMeshPro.alignment = TextAlignmentOptions.BottomRight;
                    m_frameCounter_transform.pivot = new Vector2(1, 0);
                    m_frameCounter_transform.anchorMin = new Vector2(0.99f, 0.01f);
                    m_frameCounter_transform.anchorMax = new Vector2(0.99f, 0.01f);
                    m_frameCounter_transform.anchoredPosition = new Vector2(1, 0);
                    break;
            }
        }
    }
}