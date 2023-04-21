using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;


namespace TMPro
{

    public class TMP_TextEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // Đây là script TMP_TextEventHandler được sử dụng để đăng ký các sự kiện liên quan đến văn bản trên đối tượng TMP_Text, bao gồm các sự kiện lựa chọn ký tự, lựa chọn sprite, lựa chọn từ, lựa chọn dòng và lựa chọn liên kết.
        // Script này bao gồm các lớp CharacterSelectionEvent, SpriteSelectionEvent, WordSelectionEvent, LineSelectionEvent và LinkSelectionEvent, được sử dụng để định nghĩa các sự kiện lựa chọn. Các sự kiện này đều được thừa kế từ UnityEvent và có các tham số riêng cho mỗi loại sự kiện.
        // Trong hàm Awake(), script được sử dụng để đăng ký các sự kiện văn bản. Không có mã trong script này mô tả cách thực hiện các sự kiện, mà nó được sử dụng để kích hoạt các sự kiện trên đối tượng TMP_Text khi các sự kiện tương ứng xảy ra, như nhấp chuột vào vị trí của ký tự, sprite, từ, dòng hoặc liên kết.
        [Serializable]
        public class CharacterSelectionEvent : UnityEvent<char, int> { }

        [Serializable]
        public class SpriteSelectionEvent : UnityEvent<char, int> { }

        [Serializable]
        public class WordSelectionEvent : UnityEvent<string, int, int> { }

        [Serializable]
        public class LineSelectionEvent : UnityEvent<string, int, int> { }

        [Serializable]
        public class LinkSelectionEvent : UnityEvent<string, string, int> { }

        // Đây là khai báo của sự kiện onCharacterSelection trong script TMP_TextEventHandler.
        // Sự kiện này là một đối tượng đại diện cho một hàm có thể được gọi khi trỏ vào một ký tự trên đối tượng TMP_Text.
        // Sự kiện được kích hoạt bằng cách gán một hàm (delegate) cho sự kiện onCharacterSelection.
        // Trong script này, thuộc tính onCharacterSelection là một đối tượng CharacterSelectionEvent, được sử dụng để định nghĩa một đối tượng hàm có tham số là ký tự char và một số nguyên int.
        // Biến m_OnCharacterSelection được sử dụng để lưu trữ hàm được gán cho sự kiện onCharacterSelection. Nó là Serializable nên có thể được cấu hình trong trình chỉnh sửa Unity.
        /// <summary>
        /// Event delegate triggered when pointer is over a character.
        /// </summary>
        public CharacterSelectionEvent onCharacterSelection
        {
            get { return m_OnCharacterSelection; }
            set { m_OnCharacterSelection = value; }
        }
        [SerializeField]
        private CharacterSelectionEvent m_OnCharacterSelection = new CharacterSelectionEvent();

        // Đây là khai báo của sự kiện onSpriteSelection trong script TMP_TextEventHandler.
        // Sự kiện này là một đối tượng đại diện cho một hàm có thể được gọi khi trỏ vào một sprite trên đối tượng TMP_Text.
        // Sự kiện được kích hoạt bằng cách gán một hàm (delegate) cho sự kiện onSpriteSelection.
        // Trong script này, thuộc tính onSpriteSelection là một đối tượng SpriteSelectionEvent, được sử dụng để định nghĩa một đối tượng hàm có tham số là ký tự char và một số nguyên int.
        // Biến m_OnSpriteSelection được sử dụng để lưu trữ hàm được gán cho sự kiện onSpriteSelection. Nó là Serializable nên có thể được cấu hình trong trình chỉnh sửa Unity.
        /// <summary>
        /// Event delegate triggered when pointer is over a sprite.
        /// </summary>
        public SpriteSelectionEvent onSpriteSelection
        {
            get { return m_OnSpriteSelection; }
            set { m_OnSpriteSelection = value; }
        }
        [SerializeField]
        private SpriteSelectionEvent m_OnSpriteSelection = new SpriteSelectionEvent();

        // Đây là khai báo của sự kiện onWordSelection trong script TMP_TextEventHandler.
        // Sự kiện này là một đối tượng đại diện cho một hàm có thể được gọi khi trỏ vào một từ trên đối tượng TMP_Text.
        // Sự kiện được kích hoạt bằng cách gán một hàm (delegate) cho sự kiện onWordSelection.
        // Trong script này, thuộc tính onWordSelection là một đối tượng WordSelectionEvent, được sử dụng để định nghĩa một đối tượng hàm có tham số là chuỗi string, và hai số nguyên int.
        // Biến m_OnWordSelection được sử dụng để lưu trữ hàm được gán cho sự kiện onWordSelection. Nó là Serializable nên có thể được cấu hình trong trình chỉnh sửa Unity.
        /// <summary>
        /// Event delegate triggered when pointer is over a word.
        /// </summary>
        public WordSelectionEvent onWordSelection
        {
            get { return m_OnWordSelection; }
            set { m_OnWordSelection = value; }
        }
        [SerializeField]
        private WordSelectionEvent m_OnWordSelection = new WordSelectionEvent();

        // Đây là khai báo của sự kiện onLineSelection trong script TMP_TextEventHandler.
        // Sự kiện này là một đối tượng đại diện cho một hàm có thể được gọi khi trỏ vào một dòng trên đối tượng TMP_Text.
        // Sự kiện được kích hoạt bằng cách gán một hàm (delegate) cho sự kiện onLineSelection.
        // Trong script này, thuộc tính onLineSelection là một đối tượng LineSelectionEvent, được sử dụng để định nghĩa một đối tượng hàm có tham số là chuỗi string, và hai số nguyên int.
        // Biến m_OnLineSelection được sử dụng để lưu trữ hàm được gán cho sự kiện onLineSelection. Nó là Serializable nên có thể được cấu hình trong trình chỉnh sửa Unity.
        /// <summary>
        /// Event delegate triggered when pointer is over a line.
        /// </summary>
        public LineSelectionEvent onLineSelection
        {
            get { return m_OnLineSelection; }
            set { m_OnLineSelection = value; }
        }
        [SerializeField]
        private LineSelectionEvent m_OnLineSelection = new LineSelectionEvent();

        // Đây là khai báo của sự kiện onLinkSelection trong script TMP_TextEventHandler.
        // Sự kiện này là một đối tượng đại diện cho một hàm có thể được gọi khi trỏ vào một đường dẫn trên đối tượng TMP_Text.
        // Sự kiện được kích hoạt bằng cách gán một hàm (delegate) cho sự kiện onLinkSelection.
        // Trong script này, thuộc tính onLinkSelection là một đối tượng LinkSelectionEvent, được sử dụng để định nghĩa một đối tượng hàm có tham số là chuỗi string.
        // Biến m_OnLinkSelection được sử dụng để lưu trữ hàm được gán cho sự kiện onLinkSelection. Nó là Serializable nên có thể được cấu hình trong trình chỉnh sửa Unity.
        /// <summary>
        /// Event delegate triggered when pointer is over a link.
        /// </summary>
        public LinkSelectionEvent onLinkSelection
        {
            get { return m_OnLinkSelection; }
            set { m_OnLinkSelection = value; }
        }
        [SerializeField]
        private LinkSelectionEvent m_OnLinkSelection = new LinkSelectionEvent();


        // Đây là khai báo của một số biến trong script TMP_TextEventHandler.
        // Biến m_TextComponent là đối tượng TMP_Text sẽ được sự kiện xử lý.
        // Biến m_Camera là đối tượng Camera được sử dụng để chuyển đổi tọa độ của con trỏ chuột.
        // Biến m_Canvas là đối tượng Canvas của đối tượng TMP_Text.
        // Biến m_selectedLink lưu trữ chỉ mục của liên kết được chọn. Nếu không có liên kết nào được chọn, giá trị này sẽ là -1.
        // Biến m_lastCharIndex lưu trữ chỉ số của ký tự cuối cùng được chọn bởi người dùng.
        // Biến m_lastWordIndex lưu trữ chỉ số của từ cuối cùng được chọn bởi người dùng.
        // Biến m_lastLineIndex lưu trữ chỉ số của dòng cuối cùng được chọn bởi người dùng.
        // Các biến này được sử dụng để lưu trữ thông tin về các tùy chọn được thực hiện bởi người dùng, và sẽ được sử dụng để thực hiện các hành động tương ứng trong quá trình xử lý sự kiện.
        private TMP_Text m_TextComponent;

        private Camera m_Camera;
        private Canvas m_Canvas;

        private int m_selectedLink = -1;
        private int m_lastCharIndex = -1;
        private int m_lastWordIndex = -1;
        private int m_lastLineIndex = -1;

        // Hàm Awake được gọi khi đối tượng của script được tạo ra hoặc kích hoạt.
        // Trong hàm Awake này:
        // Lấy tham chiếu đến đối tượng TMP_Text mà script được gắn vào.
        // Kiểm tra loại của đối tượng TMP_Text, nếu là TextMeshProUGUI thì lấy tham chiếu đến đối tượng Canvas bao ngoài để sử dụng. Trong trường hợp Canvas có RenderMode là ScreenSpaceOverlay, Camera sẽ không được sử dụng. Nếu không phải kiểu TextMeshProUGUI, Camera được lấy từ Camera.main của scene.
        // Gán tham chiếu đã lấy được vào các biến tương ứng của script để sử dụng trong truy vấn tọa độ con trỏ chuột.
        void Awake()
        {
            // Get a reference to the text component.
            m_TextComponent = gameObject.GetComponent<TMP_Text>();

            // Get a reference to the camera rendering the text taking into consideration the text component type.
            if (m_TextComponent.GetType() == typeof(TextMeshProUGUI))
            {
                m_Canvas = gameObject.GetComponentInParent<Canvas>();
                if (m_Canvas != null)
                {
                    if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                        m_Camera = null;
                    else
                        m_Camera = m_Canvas.worldCamera;
                }
            }
            else
            {
                m_Camera = Camera.main;
            }
        }

        // Hàm LateUpdate được gọi trong mỗi khung hình sau khi hàm Update được gọi.
        // Trong hàm này:
        // Kiểm tra xem con trỏ chuột có tiếp xúc với đối tượng TMP_Text không bằng cách sử dụng hàm IsIntersectingRectTransform của lớp TMP_TextUtilities.
        // Nếu có tiếp xúc, tiếp tục kiểm tra sự kiện người dùng đang thực hiện trên đối tượng TMP_Text (chọn ký tự, chọn từ, chọn dòng, chọn liên kết).
        // Nếu con trỏ chuột tiếp xúc với ký tự nào đó trên đối tượng TMP_Text, sử dụng hàm FindIntersectingCharacter để tìm chỉ số ký tự đó và gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện này.
        // Nếu con trỏ chuột tiếp xúc với từ nào đó trên đối tượng TMP_Text, sử dụng hàm FindIntersectingWord để tìm chỉ số từ đó và gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện này.
        // Nếu con trỏ chuột tiếp xúc với dòng nào đó trên đối tượng TMP_Text, sử dụng hàm FindIntersectingLine để tìm chỉ số dòng đó và gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện này.
        // Nếu con trỏ chuột tiếp xúc với liên kết nào đó trong đối tượng TMP_Text, sử dụng hàm FindIntersectingLink để tìm chỉ số liên kết đó và gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện này.
        // Nếu con trỏ chuột không tiếp xúc với đối tượng TMP_Text, đặt lại tất cả các lựa chọn.
        void LateUpdate()
        {
            if (TMP_TextUtilities.IsIntersectingRectTransform(m_TextComponent.rectTransform, Input.mousePosition, m_Camera))
            {
                #region Example of Character or Sprite Selection
                int charIndex = TMP_TextUtilities.FindIntersectingCharacter(m_TextComponent, Input.mousePosition, m_Camera, true);
                if (charIndex != -1 && charIndex != m_lastCharIndex)
                {
                    m_lastCharIndex = charIndex;

                    TMP_TextElementType elementType = m_TextComponent.textInfo.characterInfo[charIndex].elementType;

                    // Send event to any event listeners depending on whether it is a character or sprite.
                    if (elementType == TMP_TextElementType.Character)
                        SendOnCharacterSelection(m_TextComponent.textInfo.characterInfo[charIndex].character, charIndex);
                    else if (elementType == TMP_TextElementType.Sprite)
                        SendOnSpriteSelection(m_TextComponent.textInfo.characterInfo[charIndex].character, charIndex);
                }
                #endregion


                #region Example of Word Selection
                // Check if Mouse intersects any words and if so assign a random color to that word.
                int wordIndex = TMP_TextUtilities.FindIntersectingWord(m_TextComponent, Input.mousePosition, m_Camera);
                if (wordIndex != -1 && wordIndex != m_lastWordIndex)
                {
                    m_lastWordIndex = wordIndex;

                    // Get the information about the selected word.
                    TMP_WordInfo wInfo = m_TextComponent.textInfo.wordInfo[wordIndex];

                    // Send the event to any listeners.
                    SendOnWordSelection(wInfo.GetWord(), wInfo.firstCharacterIndex, wInfo.characterCount);
                }
                #endregion


                #region Example of Line Selection
                // Check if Mouse intersects any words and if so assign a random color to that word.
                int lineIndex = TMP_TextUtilities.FindIntersectingLine(m_TextComponent, Input.mousePosition, m_Camera);
                if (lineIndex != -1 && lineIndex != m_lastLineIndex)
                {
                    m_lastLineIndex = lineIndex;

                    // Get the information about the selected word.
                    TMP_LineInfo lineInfo = m_TextComponent.textInfo.lineInfo[lineIndex];

                    // Send the event to any listeners.
                    char[] buffer = new char[lineInfo.characterCount];
                    for (int i = 0; i < lineInfo.characterCount && i < m_TextComponent.textInfo.characterInfo.Length; i++)
                    {
                        buffer[i] = m_TextComponent.textInfo.characterInfo[i + lineInfo.firstCharacterIndex].character;
                    }

                    string lineText = new string(buffer);
                    SendOnLineSelection(lineText, lineInfo.firstCharacterIndex, lineInfo.characterCount);
                }
                #endregion


                #region Example of Link Handling
                // Check if mouse intersects with any links.
                int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextComponent, Input.mousePosition, m_Camera);

                // Handle new Link selection.
                if (linkIndex != -1 && linkIndex != m_selectedLink)
                {
                    m_selectedLink = linkIndex;

                    // Get information about the link.
                    TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];

                    // Send the event to any listeners.
                    SendOnLinkSelection(linkInfo.GetLinkID(), linkInfo.GetLinkText(), linkIndex);
                }
                #endregion
            }
            else
            {
                // Reset all selections given we are hovering outside the text container bounds.
                m_selectedLink = -1;
                m_lastCharIndex = -1;
                m_lastWordIndex = -1;
                m_lastLineIndex = -1;
            }
        }

        // Đây là các hàm gửi sự kiện tương ứng cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện tương ứng của đối tượng được sử dụng.
        // Hàm OnPointerEnter được triệu gọi khi con trỏ chuột đi vào đối tượng.
        // Hàm OnPointerExit được triệu gọi khi con trỏ chuột rời khỏi đối tượng.
        // Hàm SendOnCharacterSelection gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện chọn ký tự.
        // Hàm SendOnSpriteSelection gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện chọn Sprite.
        // Hàm SendOnWordSelection gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện chọn từ.
        // Hàm SendOnLineSelection gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện chọn dòng.
        // Hàm SendOnLinkSelection gửi sự kiện cho bất kỳ trình xử lý sự kiện nào lắng nghe sự kiện chọn liên kết.


        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("OnPointerEnter()");
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("OnPointerExit()");
        }


        private void SendOnCharacterSelection(char character, int characterIndex)
        {
            if (onCharacterSelection != null)
                onCharacterSelection.Invoke(character, characterIndex);
        }

        private void SendOnSpriteSelection(char character, int characterIndex)
        {
            if (onSpriteSelection != null)
                onSpriteSelection.Invoke(character, characterIndex);
        }

        private void SendOnWordSelection(string word, int charIndex, int length)
        {
            if (onWordSelection != null)
                onWordSelection.Invoke(word, charIndex, length);
        }

        private void SendOnLineSelection(string line, int charIndex, int length)
        {
            if (onLineSelection != null)
                onLineSelection.Invoke(line, charIndex, length);
        }

        private void SendOnLinkSelection(string linkID, string linkText, int linkIndex)
        {
            if (onLinkSelection != null)
                onLinkSelection.Invoke(linkID, linkText, linkIndex);
        }

    }
}
