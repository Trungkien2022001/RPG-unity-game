using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class TeleType : MonoBehaviour
    {

/* được sử dụng để tạo hiệu ứng chữ viết dần dần (Typewriter) cho văn bản sử dụng gói TextMesh Pro (TMP).

Trong đoạn mã, có một biến chuỗi "label01" chứa nội dung cần hiển thị ban đầu, và một biến chuỗi "label02" 
chứa nội dung cần thay thế sau khi hiển thị xong "label01".*/

        //[Range(0, 100)]
        //public int RevealSpeed = 50;

        private string label01 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=1>";
        private string label02 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=2>";


        private TMP_Text m_textMeshPro;
/*Trong phương thức "Awake()", đoạn mã lấy tham chiếu đến thành phần TextMeshPro và thiết lập các thuộc tính của nó, 
bao gồm cả nội dung, thuộc tính enableWordWrapping, và sự căn chỉnh (alignment).*/

        void Awake()
        {
            // Get Reference to TextMeshPro Component
            m_textMeshPro = GetComponent<TMP_Text>();
            m_textMeshPro.text = label01;
            m_textMeshPro.enableWordWrapping = true;
            m_textMeshPro.alignment = TextAlignmentOptions.Top;



            //if (GetComponentInParent(typeof(Canvas)) as Canvas == null)
            //{
            //    GameObject canvas = new GameObject("Canvas", typeof(Canvas));
            //    gameObject.transform.SetParent(canvas.transform);
            //    canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            //    // Set RectTransform Size
            //    gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 300);
            //    m_textMeshPro.fontSize = 48;
            //}


        }

/*Trong phương thức "Start()", đoạn mã bắt đầu một coroutine để hiển thị nội dung dần dần. 
Đầu tiên, một lần cập nhật lưới (mesh) được thực hiện để có thông tin về ký tự có thể hiển thị trong văn bản. 
Sau đó, đoạn mã lặp lại một vòng lặp vô hạn, lấy số lượng ký tự hiển thị và tăng biến đếm cho mỗi lần lặp. 
Sau đó, đoạn mã đặt số lượng ký tự hiển thị cho thành phần TextMeshPro bằng cách thiết lập giá trị của thuộc tính maxVisibleCharacters. 
Nếu tất cả các ký tự đã được hiển thị, đoạn mã sẽ chờ 1 giây trước khi chuyển sang hiển thị nội dung mới.*/
        IEnumerator Start()
        {

            // Force and update of the mesh to get valid information.
            m_textMeshPro.ForceMeshUpdate();


            int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
            int counter = 0;
            int visibleCount = 0;

            while (true)
            {
                visibleCount = counter % (totalVisibleCharacters + 1);

                m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                // Once the last character has been revealed, wait 1.0 second and start over.
                if (visibleCount >= totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                    m_textMeshPro.text = label02;
                    yield return new WaitForSeconds(1.0f);
                    m_textMeshPro.text = label01;
                    yield return new WaitForSeconds(1.0f);
                }

                counter += 1;
/*Với mỗi lần lặp lại vòng lặp, đoạn mã sử dụng phương thức WaitForSeconds() để chờ một thời gian 
ngắn trước khi lặp lại quá trình hiển thị. Cuối cùng, đoạn mã sẽ hiển thị hoàn tất khi coroutine 
kết thúc và việc hiển thị hoàn thành.*/
                yield return new WaitForSeconds(0.05f);
            }

            //Debug.Log("Done revealing the text.");
        }

    }
}