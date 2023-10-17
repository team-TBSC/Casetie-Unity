using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Playables;

public class Button : MonoBehaviour
{
    public RuntimeAnimatorController high;
    public RuntimeAnimatorController middle;
    public RuntimeAnimatorController low;

    public Texture rock;
    public Texture ballad;
    public Texture indie;
    public Texture trot;
    public Texture hiphop;
    public Texture dance;
    public Texture pop;
    public Texture noGenre;
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject cassetti;
    public PlayableDirector timeline;
    public int num = 0;
    private Texture texture;
    private Vector2 offset;
    private RuntimeAnimatorController controller;

    [System.Serializable]
    public class Api
    {
        public string name;
        public string result;
    }
    // This script will simply instantiate the Prefab when the game starts.
    public void ButtonClick(){
        StartCoroutine(Create());
    }

    
    IEnumerator Create(){
        UnityWebRequest request = UnityWebRequest.Get("http://digger.works:12023/getLastDB");

        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            string data = request.downloadHandler.text;
            Api apiResult = JsonUtility.FromJson<Api>(data);

            //에니메이터 결정 / 에너지
            if(apiResult.result[0].Equals('0')) controller = low;           //low
            else if(apiResult.result[0].Equals('1')) controller = middle;   //medium
            else controller = high;                                         //high

            //표정 결정
            if(apiResult.result[1].Equals('0'))         //strong happy
                offset = new Vector2(0.5f, 0.5f);
            else if(apiResult.result[1].Equals('1'))    //week happy
                offset = new Vector2(0.0f, 0.5f);
            else if(apiResult.result[1].Equals('2'))    //week sad
                offset = new Vector2(0.0f, 0.0f);
            else                                        //strong sad
                offset = new Vector2(0.5f, 0.0f);

            //텍스처 결정
            if(apiResult.result[2].Equals('0')) texture = rock;
            else if(apiResult.result[2].Equals('1')) texture = ballad;
            else if(apiResult.result[2].Equals('2')) texture = indie;
            else if(apiResult.result[2].Equals('3')) texture = trot;
            else if(apiResult.result[2].Equals('4')) texture = hiphop;
            else if(apiResult.result[2].Equals('5')) texture = dance;
            else if(apiResult.result[2].Equals('6')) texture = pop;
            else texture = noGenre;
            Debug.Log(apiResult.result[2]);
            Debug.Log(texture);


            // 40마리 이상 시 가장 먼저 들어온 카세티 삭제
            if(num > 40){
                GameObject obj = GameObject.Find("Cassetti" + (num - 41).ToString());
                Destroy(obj);
            }

            //timeline 시작
            timeline.Play();

            //동적 생성
            GameObject newObject = Instantiate(cassetti, new Vector3(3.5f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));

            //애니메이터
            Animator animator = newObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = controller;
            if(apiResult.result[1].Equals('0'))         //strong happy
                animator.SetBool("strong_happy", true);
            else if(apiResult.result[1].Equals('1'))    //week happy
                animator.SetBool("week_happy", true);
            else if(apiResult.result[1].Equals('2'))    //week sad
                animator.SetBool("week_sad", true);
            else                                        //strong sad
                animator.SetBool("strong_sad", true);
            
            //표정
            Renderer renderer;  
            renderer = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Head_M/pasted__pPlane1").GetComponent<Renderer>();
            renderer.material.mainTextureOffset = offset;
            renderer = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Head_M/pasted__pPlane3").GetComponent<Renderer>();
            renderer.material.mainTextureOffset = offset;
            renderer = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Head_M/pasted__pPlane4").GetComponent<Renderer>();
            renderer.material.mainTextureOffset = offset;

            //텍스처
            renderer = newObject.transform.Find("Geometry/group1/pCube9").GetComponent<Renderer>();
            renderer.material.SetTexture("_MainTex", texture);
            renderer = newObject.transform.Find("Geometry/group1/pCylinder1").GetComponent<Renderer>();
            renderer.material.SetTexture("_MainTex", texture);
            renderer = newObject.transform.Find("Geometry/group1/pCylinder2").GetComponent<Renderer>();
            renderer.material.SetTexture("_MainTex", texture);
            renderer = newObject.transform.Find("Geometry/group1/pasted__pCube13").GetComponent<Renderer>();
            renderer.material.SetTexture("_MainTex", texture);
            renderer = newObject.transform.Find("Geometry/group1/pasted__pCube15").GetComponent<Renderer>();
            renderer.material.SetTexture("_MainTex", texture);

            GameObject custom;
            //커스텀 결정
            if(apiResult.result[3].Equals('0')){
                custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Rock").gameObject;
                custom.SetActive(true);
            }
            else if(apiResult.result[3].Equals('1')){
                custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Ballad").gameObject;
                custom.SetActive(true);
            }
            else if(apiResult.result[3].Equals('2')){
                custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Indie").gameObject;
                custom.SetActive(true);
            }
            else if(apiResult.result[3].Equals('4')){
                custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Hiphop").gameObject;
                custom.SetActive(true);
            }
            else if(apiResult.result[3].Equals('5')){
                custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Idol").gameObject;
                custom.SetActive(true);
            }
            else if(apiResult.result[3].Equals('6')){
                custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Pop").gameObject;
                custom.SetActive(true);
            }
            else{
                // int rand = Random.Range(0, 2);
                // if(rand == 0){
                //     custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_None").gameObject;
                //     custom.SetActive(true);
                // }
                custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_None").gameObject;
                custom.SetActive(true);
            }

            //이름
            newObject.name = "Cassetti" + num.ToString();
            num++;

            //머리 위 이름
            Renderer nickname = newObject.transform.Find("Name").GetComponent<Renderer>();
            TextMesh textMesh = nickname.GetComponent<TextMesh>();
            textMesh.text = apiResult.name;
            GameObject childObject = newObject.transform.Find("Name").gameObject;
            childObject.SetActive(false);

            yield return new WaitForSeconds(4.0f);

            childObject.SetActive(true);

            Debug.Log(apiResult.result);
        }
        else
        {
            Debug.Log(request.error);
        }
    }
}
