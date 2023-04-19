using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{

    public class Benchmark02 : MonoBehaviour
    {

        public int SpawnType = 0;
        public int NumberOfNPC = 12;

        public bool IsTextObjectScaleStatic;
        private TextMeshProFloatingText floatingText_Script;


        void Start()
        {

            for (int i = 0; i < NumberOfNPC; i++)
            {


                if (SpawnType == 0)
                {
                    // TextMesh Pro Implementation
                    GameObject go = new GameObject();
                    go.transform.position = new Vector3(Random.Range(-95f, 95f), 0.25f, Random.Range(-95f, 95f));

                    TextMeshPro textMeshPro = go.AddComponent<TextMeshPro>();

                    textMeshPro.autoSizeTextContainer = true;
                    textMeshPro.rectTransform.pivot = new Vector2(0.5f, 0);

                    textMeshPro.alignment = TextAlignmentOptions.Bottom;
                    textMeshPro.fontSize = 96;
                    textMeshPro.enableKerning = false;

                    textMeshPro.color = new Color32(255, 255, 0, 255);
                    textMeshPro.text = "!";
                    textMeshPro.isTextObjectScaleStatic = IsTextObjectScaleStatic;

                    // Spawn Floating Text
                    floatingText_Script = go.AddComponent<TextMeshProFloatingText>();
                    floatingText_Script.SpawnType = 0;
                    floatingText_Script.IsTextObjectScaleStatic = IsTextObjectScaleStatic;
                }
                else if (SpawnType == 1)
                {
                    // TextMesh Implementation
                    GameObject go = new GameObject();
                    go.transform.position = new Vector3(Random.Range(-95f, 95f), 0.25f, Random.Range(-95f, 95f));

                    TextMesh textMesh = go.AddComponent<TextMesh>();
                    textMesh.font = Resources.Load<Font>("Fonts/ARIAL");
                    textMesh.GetComponent<Renderer>().sharedMaterial = textMesh.font.material;

                    textMesh.anchor = TextAnchor.LowerCenter;
                    textMesh.fontSize = 96;

                    textMesh.color = new Color32(255, 255, 0, 255);
                    textMesh.text = "!";

                    // Spawn Floating Text
                    floatingText_Script = go.AddComponent<TextMeshProFloatingText>();
                    floatingText_Script.SpawnType = 1;
                }
                else if (SpawnType == 2)
                {
                    // Canvas WorldSpace Camera
                    GameObject go = new GameObject();
                    Canvas canvas = go.AddComponent<Canvas>();
                    canvas.worldCamera = Camera.main;

                    go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    go.transform.position = new Vector3(Random.Range(-95f, 95f), 5f, Random.Range(-95f, 95f));

                    TextMeshProUGUI textObject = new GameObject().AddComponent<TextMeshProUGUI>();
                    textObject.rectTransform.SetParent(go.transform, false);

                    textObject.color = new Color32(255, 255, 0, 255);
                    textObject.alignment = TextAlignmentOptions.Bottom;
                    textObject.fontSize = 96;
                    textObject.text = "!";

                    // Spawn Floating Text
                    floatingText_Script = go.AddComponent<TextMeshProFloatingText>();
                    floatingText_Script.SpawnType = 0;
                }



            }
        }
    }
}


// Đây là một đoạn mã trong Unity Engine, được sử dụng để tạo ra các vật thể chữ (text objects) nổi lên và di chuyển trong môi trường game.

// Dưới đây là phân tích chi tiết đoạn mã này:

// Dòng đầu tiên: using UnityEngine; là cách để khai báo thư viện Unity Engine được sử dụng trong đoạn mã.

// Tiếp theo, đoạn mã tiếp tục với khai báo namespace TMPro.Examples cho phép sử dụng lớp (class) Benchmark02 trong nó.

// public class Benchmark02 : MonoBehaviour là lớp (class) Benchmark02 được kế thừa từ lớp (class) MonoBehaviour, là một lớp cơ bản của Unity Engine, cung cấp các phương thức để tạo ra các vật thể trong game.

// Tiếp theo là khai báo các biến số (variable) như public int SpawnType = 0; và public int NumberOfNPC = 12;. Biến SpawnType sẽ quyết định loại vật thể chữ được tạo ra (0: TextMeshPro, 1: TextMesh, 2: Canvas WorldSpace Camera), và biến NumberOfNPC quy định số lượng vật thể chữ sẽ được tạo ra.

// Biến IsTextObjectScaleStatic được sử dụng để quyết định xem kích thước của vật thể chữTrong hàm Start(), chúng ta thấy một vòng lặp for được sử dụng để tạo ra các đối tượng văn bản dạng "Floating Text" trên màn hình. Đầu tiên, vòng lặp này kiểm tra giá trị của biến SpawnType để xác định loại đối tượng văn bản sẽ được tạo. Nếu giá trị của SpawnType là 0, đối tượng TextMeshPro (thuộc thư viện TextMesh Pro) sẽ được tạo ra.

// Trong lệnh tạo đối tượng, chúng ta thấy một GameObject được tạo ra và gán vào biến go. Sau đó, thuộc tính position của đối tượng được thiết lập bằng một Vector3 ngẫu nhiên, sử dụng phương thức Random.Range() để tạo giá trị trong khoảng -95 đến 95. Điều này sẽ đặt văn bản ở một vị trí ngẫu nhiên trên màn hình.

// Sau đó, một thành phần TextMeshPro được thêm vào đối tượng go sử dụng phương thức go.AddComponent<TextMeshPro>(). Các thuộc tính của TextMeshPro được thiết lập trong đoạn mã tiếp theo.

// Trong đoạn mã tiếp theo, thuộc tính autoSizeTextContainer được thiết lập để văn bản tự động điều chỉnh kích thước của mình để phù hợp với nội dung. rectTransform.pivot được thiết lập để đặt trục quay của văn bản ở giữa dưới của đối tượng. alignment được thiết lập để đặt văn bản ở phía dưới cùng của đối tượng. fontSize được thiết lập để đặt kích thước văn bản là 96. enableKerning được thiết lập để vô hiệu hóa Kerning trong văn bản, điều này có thể giúp tăng hiệu suất.

// Một màu sắc mới được thiết lập cho văn bản bằng cách tạo một đối tượng Color32 mới và gán cho thuộc tính color. text được thiết lập để hiển thị một dấu chấm than màu và isTextObjectScaleStatic được thiết lập để vô hiệu hóa tự động thay đổi kích thước văn bản.

// Sau khi văn bản được tạo, một thành phần TextMeshProFloatingText cũng được thêm vào đối tượng go, được gán vào biến floatingText_Script. Thuộc tính SpawnType của floatingText_Script được thiết lập để xác định loại đối tượng Floating Text sẽ được tạo.

// Điều tương tự xảy ra khi SpawnType được đặt thành 1 hoặc 2. Nếu giá trị SpawnTypeTrong hàm Start(), vòng lặp for sẽ tạo ra NumberOfNPC đối tượng game object với vị trí x, y, z ngẫu nhiên trong khoảng giá trị (-95, 95) và tạo ra một văn bản nổi lên từ đối tượng game object này.
// Nếu SpawnType được thiết lập là 0, go sẽ được tạo ra là đối tượng game object mới và văn bản sẽ được thêm vào đó bằng cách sử dụng TextMeshPro. Sau đó, văn bản nổi lên sẽ được tạo ra bằng cách gắn TextMeshProFloatingText Component vào đối tượng game object này.
// Nếu SpawnType được thiết lập là 1, go sẽ được tạo ra là đối tượng game object mới và văn bản sẽ được thêm vào đó bằng cách sử dụng TextMesh. Sau đó, văn bản nổi lên sẽ được tạo ra bằng cách gắn TextMeshProFloatingText Component vào đối tượng game object này.
// Nếu SpawnType được thiết lập là 2, go sẽ được tạo ra là đối tượng game object mới và văn bản sẽ được thêm vào đó bằng cách sử dụng TextMeshProUGUI. Sau đó, văn bản nổi lên sẽ được tạo ra bằng cách gắn TextMeshProFloatingText Component vào đối tượng game object này.
// Biến IsTextObjectScaleStatic được sử dụng để đặt thuộc tính isTextObjectScaleStatic của textMeshPro. Nếu thuộc tính này được đặt là true, kích thước của văn bản sẽ không thay đổi khi đối tượng chứa nó được phóng to hoặc thu nhỏ.
// Biến floatingText_Script lưu trữ tham chiếu đến component TextMeshProFloatingText được thêm vào đối tượng game object đang được xử lý, cho phép cập nhật và quản lý các hiệu ứng của văn bản nổi lên.Kế tiếp, trong hàm Start(), một vòng lặp for được thực hiện, với i được khởi tạo bằng 0 và tăng lên cho đến khi i đạt đến giá trị của biến NumberOfNPC. Vòng lặp này được sử dụng để tạo ra các vật thể văn bản và các đối tượng văn bản nổi.

// Trong vòng lặp, một câu lệnh if-else được sử dụng để kiểm tra giá trị của biến SpawnType. Nếu giá trị của biến này là 0, thì một đối tượng văn bản được tạo ra sử dụng TextMeshPro. Nếu giá trị của biến là 1, thì một đối tượng văn bản được tạo ra sử dụng TextMesh. Nếu giá trị của biến là 2, thì một đối tượng văn bản được tạo ra sử dụng Canvas WorldSpace Camera.

// Đối với mỗi loại đối tượng văn bản, các thuộc tính được thiết lập để tạo ra một đối tượng văn bản đầy đủ, bao gồm vị trí, kích thước, kiểu chữ, màu sắc và nội dung. Sau đó, một đối tượng TextMeshProFloatingText được tạo ra và gắn vào đối tượng văn bản để tạo ra các đối tượng văn bản nổi.

// Tóm lại, đoạn mã này thực hiện tạo ra các vật thể văn bản và các đối tượng văn bản nổi sử dụng ba loại văn bản khác nhau (TextMeshPro, TextMesh và TextMeshProUGUI) và các thuộc tính khác nhau được thiết lập để tạo ra đối tượng văn bản đầy đủ.