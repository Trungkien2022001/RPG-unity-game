using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class ShaderPropAnimator : MonoBehaviour
    {

        private Renderer m_Renderer;
        private Material m_Material;

        public AnimationCurve GlowCurve;

        public float m_frame;

        void Awake()
        {
            // Cache a reference to object's renderer
            m_Renderer = GetComponent<Renderer>();

            // Cache a reference to object's material and create an instance by doing so.
            m_Material = m_Renderer.material;
        }

        void Start()
        {
            StartCoroutine(AnimateProperties());
        }

        IEnumerator AnimateProperties()
        {
            //float lightAngle;
            float glowPower;
            m_frame = Random.Range(0f, 1f);

            while (true)
            {
                //lightAngle = (m_Material.GetFloat(ShaderPropertyIDs.ID_LightAngle) + Time.deltaTime) % 6.2831853f;
                //m_Material.SetFloat(ShaderPropertyIDs.ID_LightAngle, lightAngle);

                glowPower = GlowCurve.Evaluate(m_frame);
                m_Material.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);

                m_frame += Time.deltaTime * Random.Range(0.2f, 0.3f);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

// Đoạn code trên là một class được đặt tên là ShaderPropAnimator trong namespace TMPro.Examples. Class này được kế thừa từ class MonoBehaviour.

// Các thành phần trong class bao gồm:

// private Renderer m_Renderer;: biến lưu trữ renderer của đối tượng
// private Material m_Material;: biến lưu trữ vật liệu của đối tượng
// public AnimationCurve GlowCurve;: đường cong được sử dụng để tạo độ sáng nhấp nháy
// public float m_frame;: thời gian được tính toán để sử dụng trong việc tạo độ sáng nhấp nháy
// Phương thức Awake() được gọi khi đối tượng được khởi tạo. Phương thức này lấy reference đến renderer của đối tượng và tạo một instance của vật liệu bằng cách lấy reference đến vật liệu hiện tại.

// Phương thức Start() được gọi khi đối tượng được bắt đầu. Phương thức này sử dụng coroutine để gọi phương thức AnimateProperties().

// Phương thức AnimateProperties() là một coroutine, được sử dụng để tạo độ sáng nhấp nháy. Trong phương thức này, biến glowPower được đặt bằng giá trị đánh giá của đường cong GlowCurve với đầu vào là m_frame. Sau đó, m_frame được tăng thêm một giá trị thời gian được tính toán ngẫu nhiên và coroutine đợi đến cuối khung hình. Quá trình này được lặp lại vô hạn.

// Tóm lại, class ShaderPropAnimator được sử dụng để tạo độ sáng nhấp nháy trên một đối tượng bằng cách thay đổi độ sáng của vật liệu sử dụng một đường cong đã cho.