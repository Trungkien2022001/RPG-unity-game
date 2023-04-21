using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class CameraController : MonoBehaviour
    {
        // Lớp CameraController kế thừa từ MonoBehaviour và được sử dụng để điều khiển việc di chuyển của camera trong trò chơi.
        // Biến cameraTransform được sử dụng để lưu trữ thông tin vị trí và hướng của camera. Biến dummyTarget được sử dụng như mục tiêu giả lập để camera di chuyển theo, giúp tránh khúc mắc khi nhầm lẫn với đối tượng được theo dõi.
        // Biến CameraTarget được sử dụng để đặt đối tượng mà camera sẽ theo dõi.
        // Biến FollowDistance là khoảng cách từ camera đến CameraTarget khi ở chế độ Follow. Biến MaxFollowDistance và MinFollowDistance là khoảng cách tối đa và tối thiểu mà camera có thể đến từ CameraTarget.
        // Biến ElevationAngle là góc nghiêng camera quanh hướng chiều thẳng đứng (hướng lên trên). Biến MaxElevationAngle và MinElevationAngle là giá trị tối đa và tối thiểu của góc nghiêng camera.
        // Biến OrbitalAngle là góc quay camera quanh CameraTarget.
        // Biến CameraMode là enum CameraModes, đại diện cho các chế độ camera Follow, Isometric và Free.
        // Biến MovementSmoothing và RotationSmoothing được sử dụng để đánh dấu việc sử dụng hoặc không sử dụng smoothing cho chuyển động và xoay của camera. MovementSmoothingValue và RotationSmoothingValue là giá trị mà smoothing có thể được thiết lập.
        // Biến MoveSensitivity là độ nhậy của chuyển động khi camera di chuyển.
        // Biến currentVelocity, desiredPosition, mouseX, mouseY, moveVector, và mouseWheel là các biến được sử dụng để điều khiển việc di chuyển của camera và xoay camera.
        // Hai biến hằng sử dụng để đặt các trường hợp cho sử kiện slider.
        public enum CameraModes { Follow, Isometric, Free }

        private Transform cameraTransform;
        private Transform dummyTarget;

        public Transform CameraTarget;

        public float FollowDistance = 30.0f;
        public float MaxFollowDistance = 100.0f;
        public float MinFollowDistance = 2.0f;

        public float ElevationAngle = 30.0f;
        public float MaxElevationAngle = 85.0f;
        public float MinElevationAngle = 0f;

        public float OrbitalAngle = 0f;

        public CameraModes CameraMode = CameraModes.Follow;

        public bool MovementSmoothing = true;
        public bool RotationSmoothing = false;
        private bool previousSmoothing;

        public float MovementSmoothingValue = 25f;
        public float RotationSmoothingValue = 5.0f;

        public float MoveSensitivity = 2.0f;

        private Vector3 currentVelocity = Vector3.zero;
        private Vector3 desiredPosition;
        private float mouseX;
        private float mouseY;
        private Vector3 moveVector;
        private float mouseWheel;

        // Controls for Touches on Mobile devices
        //private float prev_ZoomDelta;


        private const string event_SmoothingValue = "Slider - Smoothing Value";
        private const string event_FollowDistance = "Slider - Camera Zoom";

        // Hàm Awake() được gọi khi đối tượng được kích hoạt và được sử dụng để thiết lập các giá trị ban đầu cho camera.
        // Nếu vSync được đặt thành 1, thì mục tiêu FrameRate là 60 khung hình/giây, ngược lại thì mục tiêu là không giới hạn.
        // Nếu ứng dụng đang chạy trên thiết bị iOS hoặc Android, thì việc simuluatte Mouse sẽ không được kích hoạt.
        // Biến cameraTransform được thiết lập để chứa thông tin về vị trí và hướng của camera.
        // previousSmoothing là giá trị đánh dấu cho việc smoothing đã được sử dụng hay không.
        void Awake()
        {
            if (QualitySettings.vSyncCount > 0)
                Application.targetFrameRate = 60;
            else
                Application.targetFrameRate = -1;

            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                Input.simulateMouseWithTouches = false;

            cameraTransform = transform;
            previousSmoothing = MovementSmoothing;
        }


        // Hàm Start() được gọi một lần khi đối tượng đã có Active và được sử dụng để thiết lập camera target.
        // Nếu CameraTarget không được gán, một đối tượng giả lập được tạo ra và sử dụng để đại diện cho mục tiêu giả lập trung tâm của cảnh. Khi đó, CameraTarget sẽ được đặt là đối tượng giả lập này.
        // Use this for initialization
        void Start()
        {
            if (CameraTarget == null)
            {
                // If we don't have a target (assigned by the player, create a dummy in the center of the scene).
                dummyTarget = new GameObject("Camera Target").transform;
                CameraTarget = dummyTarget;
            }
        }

        // Hàm LateUpdate() được gọi sau mỗi Update() và được sử dụng để điều khiển các chuyển động của camera.
        // Hàm GetPlayerInput() được gọi để xử lý các chuẩn đế của người chơi như di chuyển hoặc xoay camera.
        // Kiểm tra nếu CameraTarget vẫn còn hiệu lực. Nếu có, vị trí mà camera muốn đến sẽ được tính toán dựa trên các thông số cài đặt của camera như khoảng cách theo trục z, góc nghiêng và góc quay.
        // Nếu đang sử dụng smoothing cho chuyển động, camera sẽ được di chuyển theo từng khung hình dựa trên giá trị currentVelocity và MovementSmoothingValue. Nếu không sử dụng smoothing, camera sẽ di chuyển một lần đến vị trí mới.
        // Nếu đang sử dụng smoothing cho xoay camera, camera sẽ được xoay từ vị trí hiện tại đến vị trí mới dựa trên RotationSmoothingValue. Nếu không sử dụng Smoothing, camera sẽ tự động xoay để nhìn về CameraTarget.
        // Update is called once per frame
        void LateUpdate()
        {
            GetPlayerInput();


            // Check if we still have a valid target
            if (CameraTarget != null)
            {
                if (CameraMode == CameraModes.Isometric)
                {
                    desiredPosition = CameraTarget.position + Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) * new Vector3(0, 0, -FollowDistance);
                }
                else if (CameraMode == CameraModes.Follow)
                {
                    desiredPosition = CameraTarget.position + CameraTarget.TransformDirection(Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) * (new Vector3(0, 0, -FollowDistance)));
                }
                else
                {
                    // Free Camera implementation
                }

                if (MovementSmoothing == true)
                {
                    // Using Smoothing
                    cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, desiredPosition, ref currentVelocity, MovementSmoothingValue * Time.fixedDeltaTime);
                    //cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, Time.deltaTime * 5.0f);
                }
                else
                {
                    // Not using Smoothing
                    cameraTransform.position = desiredPosition;
                }

                if (RotationSmoothing == true)
                    cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.LookRotation(CameraTarget.position - cameraTransform.position), RotationSmoothingValue * Time.deltaTime);
                else
                {
                    cameraTransform.LookAt(CameraTarget);
                }

            }

        }


        // Hàm GetPlayerInput() được gọi trong LateUpdate() để xử lý các chuẩn độ của người chơi, bao gồm phím và chuyển động của chuột.
        // Đầu tiên, hàm sẽ đặt moveVector là Vector3.zero, tức là không có chuyển động gì, và lấy giá trị của mouseWheel từ Input.GetAxis("Mouse ScrollWheel").
        // Nếu người chơi nhấn phím Shift hoặc có chạm trên thiết bị di động, giá trị của mouseWheel sẽ được nhân với 10.
        // Nếu người chơi nhấn các phím I, F, S trên bàn phím, thì camera sẽ chuyển đổi giữa các chế độ, và các nhấn đóng hoặc mở chế độ Smoothing.
        // Nếu người chơi nhấn chuột phải, thì camera sẽ được xoay theo chuyển động của chuột trên trục Y và X, ứng với ElevationAngle và OrbitalAngle của camera. Nếu đang sử dụng thiết bị di động, chuyển động tương tự cũng có thể được thực hiện.
        // Nếu người chơi nhấn chuột trái, một Raycast sẽ được thực hiện để tìm ra đối tượng mà ta muốn chọn làm CameraTarget. Nếu đối tượng này đã được chọn, sẽ thiết lập OrbitalAngle thành 0. Nếu chưa, một đối tượng giả lập sẽ được tạo ra để làm CameraTarget và Smoothing sẽ được đặt lại.
        // Nếu người chơi nhấn chuột trung, camera sẽ bị kéo theo theo chuyển động của chuột. Nếu đang sử dụng thiết bị di động, chuyển động kéo cũng có thể được thực hiện.
        // Nếu người chơi sử dụng cảm ứng hai ngón tay (pinch), camera sẽ zoom vào hoặc ra.
        // Cuối cùng, nếu người chơi cuộn chuột, camera sẽ zoom vào hoặc ra.
        void GetPlayerInput()
        {
            moveVector = Vector3.zero;

            // Check Mouse Wheel Input prior to Shift Key so we can apply multiplier on Shift for Scrolling
            mouseWheel = Input.GetAxis("Mouse ScrollWheel");

            float touchCount = Input.touchCount;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || touchCount > 0)
            {
                mouseWheel *= 10;

                if (Input.GetKeyDown(KeyCode.I))
                    CameraMode = CameraModes.Isometric;

                if (Input.GetKeyDown(KeyCode.F))
                    CameraMode = CameraModes.Follow;

                if (Input.GetKeyDown(KeyCode.S))
                    MovementSmoothing = !MovementSmoothing;


                // Check for right mouse button to change camera follow and elevation angle
                if (Input.GetMouseButton(1))
                {
                    mouseY = Input.GetAxis("Mouse Y");
                    mouseX = Input.GetAxis("Mouse X");

                    if (mouseY > 0.01f || mouseY < -0.01f)
                    {
                        ElevationAngle -= mouseY * MoveSensitivity;
                        // Limit Elevation angle between min & max values.
                        ElevationAngle = Mathf.Clamp(ElevationAngle, MinElevationAngle, MaxElevationAngle);
                    }

                    if (mouseX > 0.01f || mouseX < -0.01f)
                    {
                        OrbitalAngle += mouseX * MoveSensitivity;
                        if (OrbitalAngle > 360)
                            OrbitalAngle -= 360;
                        if (OrbitalAngle < 0)
                            OrbitalAngle += 360;
                    }
                }

                // Get Input from Mobile Device
                if (touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;

                    // Handle elevation changes
                    if (deltaPosition.y > 0.01f || deltaPosition.y < -0.01f)
                    {
                        ElevationAngle -= deltaPosition.y * 0.1f;
                        // Limit Elevation angle between min & max values.
                        ElevationAngle = Mathf.Clamp(ElevationAngle, MinElevationAngle, MaxElevationAngle);
                    }


                    // Handle left & right 
                    if (deltaPosition.x > 0.01f || deltaPosition.x < -0.01f)
                    {
                        OrbitalAngle += deltaPosition.x * 0.1f;
                        if (OrbitalAngle > 360)
                            OrbitalAngle -= 360;
                        if (OrbitalAngle < 0)
                            OrbitalAngle += 360;
                    }

                }

                // Check for left mouse button to select a new CameraTarget or to reset Follow position
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 300, 1 << 10 | 1 << 11 | 1 << 12 | 1 << 14))
                    {
                        if (hit.transform == CameraTarget)
                        {
                            // Reset Follow Position
                            OrbitalAngle = 0;
                        }
                        else
                        {
                            CameraTarget = hit.transform;
                            OrbitalAngle = 0;
                            MovementSmoothing = previousSmoothing;
                        }

                    }
                }


                if (Input.GetMouseButton(2))
                {
                    if (dummyTarget == null)
                    {
                        // We need a Dummy Target to anchor the Camera
                        dummyTarget = new GameObject("Camera Target").transform;
                        dummyTarget.position = CameraTarget.position;
                        dummyTarget.rotation = CameraTarget.rotation;
                        CameraTarget = dummyTarget;
                        previousSmoothing = MovementSmoothing;
                        MovementSmoothing = false;
                    }
                    else if (dummyTarget != CameraTarget)
                    {
                        // Move DummyTarget to CameraTarget
                        dummyTarget.position = CameraTarget.position;
                        dummyTarget.rotation = CameraTarget.rotation;
                        CameraTarget = dummyTarget;
                        previousSmoothing = MovementSmoothing;
                        MovementSmoothing = false;
                    }


                    mouseY = Input.GetAxis("Mouse Y");
                    mouseX = Input.GetAxis("Mouse X");

                    moveVector = cameraTransform.TransformDirection(mouseX, mouseY, 0);

                    dummyTarget.Translate(-moveVector, Space.World);

                }

            }

            // Check Pinching to Zoom in - out on Mobile device
            if (touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

                float prevTouchDelta = (touch0PrevPos - touch1PrevPos).magnitude;
                float touchDelta = (touch0.position - touch1.position).magnitude;

                float zoomDelta = prevTouchDelta - touchDelta;

                if (zoomDelta > 0.01f || zoomDelta < -0.01f)
                {
                    FollowDistance += zoomDelta * 0.25f;
                    // Limit FollowDistance between min & max values.
                    FollowDistance = Mathf.Clamp(FollowDistance, MinFollowDistance, MaxFollowDistance);
                }


            }

            // Check MouseWheel to Zoom in-out
            if (mouseWheel < -0.01f || mouseWheel > 0.01f)
            {

                FollowDistance -= mouseWheel * 5.0f;
                // Limit FollowDistance between min & max values.
                FollowDistance = Mathf.Clamp(FollowDistance, MinFollowDistance, MaxFollowDistance);
            }


        }
    }
}