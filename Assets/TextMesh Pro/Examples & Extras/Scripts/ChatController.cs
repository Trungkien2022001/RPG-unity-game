using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatController : MonoBehaviour {


    public TMP_InputField ChatInputField;

    public TMP_Text ChatDisplayOutput;

    public Scrollbar ChatScrollbar;

    void OnEnable()
    {
        ChatInputField.onSubmit.AddListener(AddToChatOutput);
    }

    void OnDisable()
    {
        ChatInputField.onSubmit.RemoveListener(AddToChatOutput);
    }


    void AddToChatOutput(string newText)
    {
        // Clear Input Field
        ChatInputField.text = string.Empty;

        var timeNow = System.DateTime.Now;

        string formattedInput = "[<#FFFF80>" + timeNow.Hour.ToString("d2") + ":" + timeNow.Minute.ToString("d2") + ":" + timeNow.Second.ToString("d2") + "</color>] " + newText;

        if (ChatDisplayOutput != null)
        {
            // No special formatting for first entry
            // Add line feed before each subsequent entries
            if (ChatDisplayOutput.text == string.Empty)
                ChatDisplayOutput.text = formattedInput;
            else
                ChatDisplayOutput.text += "\n" + formattedInput;
        }

        // Keep Chat input field active
        ChatInputField.ActivateInputField();

        // Set the scrollbar to the bottom when next text is submitted.
        ChatScrollbar.value = 0;
    }

}

// Đoạn code trên là một class được đặt tên là ChatController. Class này được sử dụng để quản lý các thành phần trong giao diện người dùng liên quan đến việc nhập và hiển thị tin nhắn trong một phòng chat.

// Các thành phần UI bao gồm:

// ChatInputField: là một đối tượng TMP_InputField để người dùng nhập tin nhắn vào phòng chat.
// ChatDisplayOutput: là một đối tượng TMP_Text để hiển thị các tin nhắn đã được gửi trong phòng chat.
// ChatScrollbar: là một đối tượng Scrollbar để người dùng cuộn trang khi số lượng tin nhắn quá nhiều.
// Hàm OnEnable() được gọi khi script được kích hoạt, và hàm OnDisable() được gọi khi script bị vô hiệu hóa. Trong hàm OnEnable(), hàm AddListener() được gọi trên sự kiện onSubmit của ChatInputField, và hàm RemoveListener() được gọi trong hàm OnDisable().

// Hàm AddToChatOutput() được gọi khi người dùng ấn Enter sau khi nhập tin nhắn vào ChatInputField. Hàm này sẽ lấy nội dung của ChatInputField và thêm nó vào ChatDisplayOutput. Trước khi thêm tin nhắn mới, hàm này cũng sẽ định dạng thời gian hiện tại và nối vào phía trước của nội dung tin nhắn.

// Nếu ChatDisplayOutput không null, hàm này sẽ kiểm tra xem nó có chứa bất kỳ tin nhắn nào chưa. Nếu không, hàm này sẽ thêm tin nhắn mới vào đó. Nếu có, hàm này sẽ thêm một ký tự xuống dòng và sau đó thêm tin nhắn mới.

// Cuối cùng, hàm AddToChatOutput() sẽ giữ ChatInputField hoạt động và đặt giá trị của ChatScrollbar về 0 để hiển thị tin nhắn mới nhất.
