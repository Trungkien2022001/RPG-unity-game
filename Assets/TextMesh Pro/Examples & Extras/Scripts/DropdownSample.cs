using TMPro;
using UnityEngine;

public class DropdownSample: MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI text = null;

	[SerializeField]
	private TMP_Dropdown dropdownWithoutPlaceholder = null;

	[SerializeField]
	private TMP_Dropdown dropdownWithPlaceholder = null;

	public void OnButtonClick()
	{
		text.text = dropdownWithPlaceholder.value > -1 ? "Selected values:\n" + dropdownWithoutPlaceholder.value + " - " + dropdownWithPlaceholder.value : "Error: Please make a selection";
	}
}

/*
Đoạn code này là một script để xử lý sự kiện của nút button khi người dùng click vào trong một Dropdown Menu. 

Script này sử dụng các thành phần của TextMeshPro và TMP_Dropdown, một thư viện UI mở rộng cho Unity.

Các thành phần TextMeshProUGUI và TMP_Dropdown được định nghĩa như các biến thuộc tính SerializeField, nó cho phép các thành phần này có thể được kéo và thả vào trong trình chỉnh sửa Unity.

Phương thức OnButtonClick được gọi khi người dùng nhấn vào nút Button, 
phương thức này kiểm tra giá trị được chọn của hai Dropdown Menu, 
nếu có giá trị được chọn, nó sẽ hiển thị giá trị đã chọn trong một đoạn văn bản TextMeshProUGUI. 
Nếu không, nó sẽ hiển thị một thông báo lỗi.
*/