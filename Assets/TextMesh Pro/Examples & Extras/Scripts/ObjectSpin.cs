using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class ObjectSpin : MonoBehaviour
    {

#pragma warning disable 0414

        public float SpinSpeed = 5;
        public int RotationRange = 15;
        private Transform m_transform;

        private float m_time;
        private Vector3 m_prevPOS;
        private Vector3 m_initial_Rotation;
        private Vector3 m_initial_Position;
        private Color32 m_lightColor;
        private int frames = 0;

        public enum MotionType { Rotation, BackAndForth, Translation };
        public MotionType Motion;

        void Awake()
        {
            m_transform = transform;
            m_initial_Rotation = m_transform.rotation.eulerAngles;
            m_initial_Position = m_transform.position;

            Light light = GetComponent<Light>();
            m_lightColor = light != null ? light.color : Color.black;
        }


        // Update is called once per frame
        void Update()
        {
            if (Motion == MotionType.Rotation)
            {
                m_transform.Rotate(0, SpinSpeed * Time.deltaTime, 0);
            }
            else if (Motion == MotionType.BackAndForth)
            {
                m_time += SpinSpeed * Time.deltaTime;
                m_transform.rotation = Quaternion.Euler(m_initial_Rotation.x, Mathf.Sin(m_time) * RotationRange + m_initial_Rotation.y, m_initial_Rotation.z);
            }
            else
            {
                m_time += SpinSpeed * Time.deltaTime;

                float x = 15 * Mathf.Cos(m_time * .95f);
                float y = 10; // *Mathf.Sin(m_time * 1f) * Mathf.Cos(m_time * 1f);
                float z = 0f; // *Mathf.Sin(m_time * .9f);    

                m_transform.position = m_initial_Position + new Vector3(x, z, y);

                // Drawing light patterns because they can be cool looking.
                //if (frames > 2)
                //    Debug.DrawLine(m_transform.position, m_prevPOS, m_lightColor, 100f);

                m_prevPOS = m_transform.position;
                frames += 1;
            }
        }
    }
}
// Đây là thành phần script được tạo bởi TextMeshPro để xoay các đối tượng.
// Trong script này, việc thực hiện xoay đối tượng được quyết định bởi giá trị của biến MotionType. Nếu MotionType được đặt là "Rotation", đối tượng sẽ xoay vô hạn theo trục y với tốc độ SpinSpeed được thiết lập.
// Nếu MotionType được đặt là "BackAndForth", đối tượng sẽ xoay với độ lệch hướng giữa mức ban đầu và một góc trong khoảng [-RotationRange, RotationRange] xung quanh trục y.
// Nếu MotionType được đặt là "Translation", đối tượng sẽ di chuyển lên xuống hoặc trái phải theo hướng xác định bởi hàm Cos.
// Lưu ý rằng việc hoạt động của đối tượng không bị giới hạn bởi khoảng RotationRange nếu đang được xoay hoặc di chuyển. Mathf.Sin và Mathf.Cos được sử dụng để tạo ra các chuyển động mượt mà.
// Một phần của script chứa các đoạn mã đang được để lại trong tình trạng bình luận. Chúng có thể được sử dụng để vẽ các mẫu ánh sáng khi chuyển động các đối tượng, nhưng hiện tại chúng không được hoạt động trong code này.