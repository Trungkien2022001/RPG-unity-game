using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class TextMeshSpawner : MonoBehaviour
    {

        public int SpawnType = 0;
        public int NumberOfNPC = 12;

        public Font TheFont;

        private TextMeshProFloatingText floatingText_Script;

        void Awake()
        {

        }

        void Start()
        {

            for (int i = 0; i < NumberOfNPC; i++)
            {
                if (SpawnType == 0)
                {
                    // TextMesh Pro Implementation     
                    //go.transform.localScale = new Vector3(2, 2, 2);
                    GameObject go = new GameObject(); //"NPC " + i);
                    go.transform.position = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));

                    //go.transform.position = new Vector3(0, 1.01f, 0);
                    //go.renderer.castShadows = false;
                    //go.renderer.receiveShadows = false;
                    //go.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

                    TextMeshPro textMeshPro = go.AddComponent<TextMeshPro>();
                    //textMeshPro.FontAsset = Resources.Load("Fonts & Materials/LiberationSans SDF", typeof(TextMeshProFont)) as TextMeshProFont;
                    //textMeshPro.anchor = AnchorPositions.Bottom;
                    textMeshPro.fontSize = 96;

                    textMeshPro.text = "!";
                    textMeshPro.color = new Color32(255, 255, 0, 255);
                    //textMeshPro.Text = "!";


                    // Spawn Floating Text
                    floatingText_Script = go.AddComponent<TextMeshProFloatingText>();
                    floatingText_Script.SpawnType = 0;
                }
                else
                {
                    // TextMesh Implementation
                    GameObject go = new GameObject(); //"NPC " + i);
                    go.transform.position = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));

                    //go.transform.position = new Vector3(0, 1.01f, 0);

                    TextMesh textMesh = go.AddComponent<TextMesh>();
                    textMesh.GetComponent<Renderer>().sharedMaterial = TheFont.material;
                    textMesh.font = TheFont;
                    textMesh.anchor = TextAnchor.LowerCenter;
                    textMesh.fontSize = 96;

                    textMesh.color = new Color32(255, 255, 0, 255);
                    textMesh.text = "!";

                    // Spawn Floating Text
                    floatingText_Script = go.AddComponent<TextMeshProFloatingText>();
                    floatingText_Script.SpawnType = 1;
                }
            }
        }

    }
}

/*
Đoạn code trên tạo ra văn bản nổi lên (floating text) trong trò chơi Unity. 
Cụ thể hơn, nó sử dụng ba thành phần: TextMesh Pro, TextMesh, và TextMeshProFloatingText.

Đầu tiên, đoạn code khai báo các biến và thiết lập giá trị ban đầu cho chúng. 
Biến "SpawnType" và "NumberOfNPC" xác định loại văn bản nổi lên được tạo ra (TextMesh Pro hay TextMesh) 
và số lượng văn bản nổi lên được tạo ra. Biến "TheFont" chứa phông chữ được sử dụng cho văn bản TextMesh.

Sau đó, trong hàm Start(), vòng lặp for được sử dụng để tạo ra văn bản nổi lên. 
Nếu giá trị của "SpawnType" là 0, văn bản TextMesh Pro được tạo ra. Nếu không, văn bản TextMesh được tạo ra. 
Cả hai loại văn bản đều được đặt tại một vị trí ngẫu nhiên trên mặt phẳng và có kích thước chữ là 96. 
Màu sắc của văn bản được đặt thành màu vàng và được đặt thành dấu chấm than ("!").

Cuối cùng, "TextMeshProFloatingText" được thêm vào game object để làm cho văn bản nổi lên di chuyển lên trên và
 biến mất sau một khoảng thời gian ngắn.
*/
