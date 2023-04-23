using UnityEngine;

/*Đoạn code trên định nghĩa một lớp script được gọi là TMP_TextEventCheck 
dùng để kiểm tra các sự kiện xảy ra trên một đối tượng TMP_TextEventHandler được tham chiếu trong biến TextEventHandler*/
namespace TMPro.Examples
{
    public class TMP_TextEventCheck : MonoBehaviour
    {

        public TMP_TextEventHandler TextEventHandler;

        private TMP_Text m_TextComponent;
/*Trong phương thức OnEnable(), nếu TextEventHandler đã được cấu hình, đối tượng m_TextComponent sẽ được gán bằng đối tượng 
TMP_Text được tham chiếu bởi TextEventHandler. Các sự kiện onCharacterSelection, onSpriteSelection, onWordSelection, onLineSelection, 
và onLinkSelection của TextEventHandler được đăng ký với các phương thức tương ứng trong lớp TMP_TextEventCheck*/
        void OnEnable()
        {
            if (TextEventHandler != null)
            {
                // Get a reference to the text component
                m_TextComponent = TextEventHandler.GetComponent<TMP_Text>();
                
                TextEventHandler.onCharacterSelection.AddListener(OnCharacterSelection);
                TextEventHandler.onSpriteSelection.AddListener(OnSpriteSelection);
                TextEventHandler.onWordSelection.AddListener(OnWordSelection);
                TextEventHandler.onLineSelection.AddListener(OnLineSelection);
                TextEventHandler.onLinkSelection.AddListener(OnLinkSelection);
            }
        }

//Trong phương thức OnDisable(), các sự kiện đã được đăng ký trước đó sẽ bị gỡ bỏ.
        void OnDisable()
        {
            if (TextEventHandler != null)
            {
                TextEventHandler.onCharacterSelection.RemoveListener(OnCharacterSelection);
                TextEventHandler.onSpriteSelection.RemoveListener(OnSpriteSelection);
                TextEventHandler.onWordSelection.RemoveListener(OnWordSelection);
                TextEventHandler.onLineSelection.RemoveListener(OnLineSelection);
                TextEventHandler.onLinkSelection.RemoveListener(OnLinkSelection);
            }
        }

/*Các phương thức OnCharacterSelection(), OnSpriteSelection(), OnWordSelection(), OnLineSelection(), và OnLinkSelection() 
được gọi khi các sự kiện tương ứng được kích hoạt trên TextEventHandler. Mỗi phương thức sẽ ghi lại thông tin về sự kiện đã xảy ra 
bằng cách sử dụng phương thức Debug.Log(). Trong phương thức OnLinkSelection(), thông tin về liên kết được lấy từ đối tượng 
m_TextComponent (nếu nó không null).*/
        void OnCharacterSelection(char c, int index)
        {
            Debug.Log("Character [" + c + "] at Index: " + index + " has been selected.");
        }

        void OnSpriteSelection(char c, int index)
        {
            Debug.Log("Sprite [" + c + "] at Index: " + index + " has been selected.");
        }

        void OnWordSelection(string word, int firstCharacterIndex, int length)
        {
            Debug.Log("Word [" + word + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
        }

        void OnLineSelection(string lineText, int firstCharacterIndex, int length)
        {
            Debug.Log("Line [" + lineText + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
        }

        void OnLinkSelection(string linkID, string linkText, int linkIndex)
        {
            if (m_TextComponent != null)
            {
                TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];
            }
            
            Debug.Log("Link Index: " + linkIndex + " with ID [" + linkID + "] and Text \"" + linkText + "\" has been selected.");
        }

    }
}
