using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    public class TextConsoleSimulator : MonoBehaviour
    {
        // Đây là một phần của script TextMeshPro để tìm kiếm các thay đổi trong văn bản và bắt đầu hiệu ứng.
        // Trong script này, hàm ON_TEXT_CHANGED được sử dụng để bắt sự kiện khi văn bản trong đối tượng đã được thay đổi. Khi sự kiện này được kích hoạt, biến hasTextChanged sẽ được thiết lập thành true, cho biết rằng văn bản đã được thay đổi và cần cập nhật lại hiệu ứng mới.
        // Trong Start(), hàm RevealCharacters hoặc RevealWords được gọi để bắt đầu hiển thị văn bản.
        // Trong OnEnable(), Text Console Simulator đăng ký sự kiện để bắt đầu theo dõi thay đổi nội dung của TMP_Text. Hàm này sẽ được gọi khi đối tượng được kích hoạt hoặc bật lại.
        // Trong OnDisable(), Text Console Simulator huỷ đăng ký sự kiện tồn tại để text object không được theo dõi khi đối tượng bị tắt hoặc vô hiệu hóa.
        // Mục đích của script này là theo dõi sự thay đổi văn bản của đối tượng và bắt đầu hiển thị nó bằng cách sử dụng các hàm RevealCharacters hoặc RevealWords đã định nghĩa.
        private TMP_Text m_TextComponent;
        private bool hasTextChanged;

        void Awake()
        {
            m_TextComponent = gameObject.GetComponent<TMP_Text>();
        }


        void Start()
        {
            StartCoroutine(RevealCharacters(m_TextComponent));
            //StartCoroutine(RevealWords(m_TextComponent));
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


        // Event received when the text object has changed.
        void ON_TEXT_CHANGED(Object obj)
        {
            hasTextChanged = true;
        }

        // Đây là hàm RevealCharacters trong script TextConsoleSimulator nhằm hiển thị chữ từng ký tự một.
        // Hàm này sử dụng ForceMeshUpdate() để thiết lập lại thông tin văn bản của TMP_Text và lấy số lượng ký tự hiển thị được từ textInfo (totalVisibleCharacters). Biến visibleCount được sử dụng để kiểm soát số lượng ký tự được hiển thị trên TMP_TextComponent, với giá trị ban đầu bằng 0.
        // Hàm while (true) được sử dụng để liên tục cập nhật số lượng ký tự hiển thị trên màn hình. Nếu biến hasTextChanged được thiết lập thành true, hàm sẽ lấy số lượng ký tự mới nhất từ textInfo và cập nhật số ký tự hiển thị trên TMP_TextComponent.
        // Nếu số lượng ký tự hiển thị lớn hơn số ký tự tối đa được định nghĩa trong TMP_TextComponent, hàm sẽ chờ đợi 1 giây và thiết lập visibleCount bằng 0, tức là bắt đầu lại từ đầu.
        // Mỗi lần trong vòng lặp, hàm sẽ cập nhật số ký tự hiển thị trên TMP_TextComponent và tăng visibleCount lên 1, và sau đó chờ đợi một khung hình (yield return null) để tiếp tục vòng lặp.
        /// <summary>
        /// Method revealing the text one character at a time.
        /// </summary>
        /// <returns></returns>
        IEnumerator RevealCharacters(TMP_Text textComponent)
        {
            textComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = textComponent.textInfo;

            int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
            int visibleCount = 0;

            while (true)
            {
                if (hasTextChanged)
                {
                    totalVisibleCharacters = textInfo.characterCount; // Update visible character count.
                    hasTextChanged = false; 
                }

                if (visibleCount > totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                    visibleCount = 0;
                }

                textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                visibleCount += 1;

                yield return null;
            }
        }
        // Đây là hàm RevealWords trong script TextConsoleSimulator nhằm hiển thị chữ từng từ một.
        // Hàm này cũng sử dụng ForceMeshUpdate() để thiết lập lại thông tin văn bản của TMP_Text và lấy số lượng từ và số lượng ký tự hiển thị được từ textInfo (totalWordCount và totalVisibleCharacters). Biến visibleCount được sử dụng để kiểm soát số lượng từ được hiển thị trên TMP_TextComponent.
        // Hàm while (true) được sử dụng để liên tục cập nhật số lượng từ hiển thị trên màn hình. Biến currentWord được sử dụng để theo dõi từ hiện tại đang được hiển thị. Nếu currentWord bằng 0, không có từ nào được hiển thị. Nếu currentWord nhỏ hơn totalWordCount, tất cả các từ khác được hiển thị ngoại trừ từ cuối cùng. Nếu currentWord bằng totalWordCount, từ cuối cùng và tất cả các ký tự còn lại được hiển thị.
        // Sau khi visibleCount được tính toán, hàm cập nhật số ký tự tối đa có thể hiển thị trên TMP_TextComponent và chờ đợi cho đến khi tất cả các ký tự được hiển thị. Nếu số lượng ký tự hiển thị đạt đến số lượng ký tự tối đa được định nghĩa trong TMP_TextComponent, hàm sẽ chờ đợi 1 giây và bắt đầu lại từ đầu
        // Mỗi vòng lặp, biến counter được cập nhật, và hàm chờ đợi 0.1 giây để kéo dài thời gian hiển thị từ.

        /// <summary>
        /// Method revealing the text one word at a time.
        /// </summary>
        /// <returns></returns>
        IEnumerator RevealWords(TMP_Text textComponent)
        {
            textComponent.ForceMeshUpdate();

            int totalWordCount = textComponent.textInfo.wordCount;
            int totalVisibleCharacters = textComponent.textInfo.characterCount; // Get # of Visible Character in text object
            int counter = 0;
            int currentWord = 0;
            int visibleCount = 0;

            while (true)
            {
                currentWord = counter % (totalWordCount + 1);

                // Get last character index for the current word.
                if (currentWord == 0) // Display no words.
                    visibleCount = 0;
                else if (currentWord < totalWordCount) // Display all other words with the exception of the last one.
                    visibleCount = textComponent.textInfo.wordInfo[currentWord - 1].lastCharacterIndex + 1;
                else if (currentWord == totalWordCount) // Display last word and all remaining characters.
                    visibleCount = totalVisibleCharacters;

                textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                // Once the last character has been revealed, wait 1.0 second and start over.
                if (visibleCount >= totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                }

                counter += 1;

                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}