using UnityEngine;
using System;
/*Đoạn code trên là một class được định nghĩa trong namespace TMPro của Unity. 
Nó mô tả một custom input validator được sử dụng để chỉ cho phép nhập các chữ số từ 0 đến 9.

Class này được kế thừa từ lớp abstract TMP_InputValidator, và ghi đè phương thức Validate. 
Phương thức Validate này được gọi khi một ký tự mới được nhập vào trong trường văn bản liên quan đến đối tượng TextMeshPro. 
Nếu ký tự này là một chữ số từ 0 đến 9, nó sẽ được thêm vào chuỗi văn bản và trả về chữ số đó. Nếu không phải, 
phương thức sẽ trả về ký tự null

Để sử dụng validator này, ta có thể gán nó cho đối tượng TextMeshPro trong Inspector của Unity. Khi đối tượng này được kích hoạt, 
nó sẽ kiểm tra tất cả các ký tự được nhập vào trong trường văn bản liên quan đến nó và chỉ cho phép các chữ số từ 0 đến 9.*/

namespace TMPro
{
    /// <summary>
    /// EXample of a Custom Character Input Validator to only allow digits from 0 to 9.
    /// </summary>
    [Serializable]
    //[CreateAssetMenu(fileName = "InputValidator - Digits.asset", menuName = "TextMeshPro/Input Validators/Digits", order = 100)]
    public class TMP_DigitValidator : TMP_InputValidator
    {
        // Custom text input validation function
        public override char Validate(ref string text, ref int pos, char ch)
        {
            if (ch >= '0' && ch <= '9')
            {
                text += ch;
                pos += 1;
                return ch;
            }

            return (char)0;
        }
    }
}
