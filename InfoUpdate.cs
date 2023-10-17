using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class InfoUpdate : MonoBehaviour
{
    public TMP_Text cassettiName;
    public TMP_Text songName;
    public TMP_Text energyText;
    public TMP_Text emotionText;
    public TMP_Text genreText1;
    public TMP_Text genreText2;
    public TMP_Text text;
    public GameObject say;
    public GameObject sayBack;

    public Sprite high;
    public Sprite middle;
    public Sprite low;

    public Sprite strongHappy;
    public Sprite weekHappy;
    public Sprite strongSad;
    public Sprite weekSad;

    public Sprite ballad;
    public Sprite hiphop;
    public Sprite dance;
    public Sprite rock;
    public Sprite trot;
    public Sprite indie;
    public Sprite pop;
    public Sprite none;

    public Image energyImage;
    public Image emotionImage;
    public Image genre1Image;
    public Image genre2Image;

    public Texture rockTex;
    public Texture balladTex;
    public Texture indieTex;
    public Texture trotTex;
    public Texture hiphopTex;
    public Texture danceTex;
    public Texture popTex;
    public Texture noGenreTex;

    public GameObject newObject;

    private Texture texture;
    private Vector2 offset;
    private RuntimeAnimatorController controller;

    [System.Serializable]
    public class Api
    {
        public string name;
        public string result;
        public string songName;
        public string text;
    }

    public void ButtonClick(){
        StartCoroutine(imageUpdate());
    }

    IEnumerator imageUpdate(){
        UnityWebRequest request = UnityWebRequest.Get("http://digger.works:12023/getLastDB");

        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            string data = request.downloadHandler.text;
            Api apiResult = JsonUtility.FromJson<Api>(data);

            cassettiName.text = apiResult.name;
            songName.text = apiResult.songName;
            text.text = apiResult.text;

            if(apiResult.result[0].Equals('0')){        //low
                energyText.text = "낮아요";
                energyImage.sprite = low;
            }
            else if(apiResult.result[0].Equals('1')){   //medium
                energyText.text = "중간";
                energyImage.sprite = middle;
            }
            else{                                       //high
                energyText.text = "높아요";
                energyImage.sprite = high;
            }

            //표정 결정
            if(apiResult.result[1].Equals('0')){        //strong happy 
                emotionText.text = "매우 신나";
                emotionImage.sprite = strongHappy;
                offset = new Vector2(0.5f, 0.5f);
            }
            else if(apiResult.result[1].Equals('1')){   //week happy
                emotionText.text = "조금 신나"; 
                emotionImage.sprite = weekHappy;
                offset = new Vector2(0.0f, 0.5f);
            }
            else if(apiResult.result[1].Equals('2')){   //week sad
                emotionText.text = "조금 우울";
                emotionImage.sprite = weekSad;
                offset = new Vector2(0.0f, 0.0f);
            } 
            else{                                        //strong sad          
                emotionText.text = "매우 우울";
                emotionImage.sprite = strongSad;
                offset = new Vector2(0.5f, 0.0f);
            }

            
            if(apiResult.result[2].Equals('0')){         
                genreText1.text = "락";
                genre1Image.sprite = rock;
                texture = rockTex;
            }
            else if(apiResult.result[2].Equals('1')){         
                genreText1.text = "발라드";
                genre1Image.sprite = ballad;
                texture = balladTex;
            }
            else if(apiResult.result[2].Equals('2')){               
                genreText1.text = "인디&어쿠스틱";
                genre1Image.sprite = indie;
                texture = indieTex;
            }
            else if(apiResult.result[2].Equals('3')){          
                genreText1.text = "트로트";
                genre1Image.sprite = trot;
                texture = trotTex;
            }
            else if(apiResult.result[2].Equals('4')){           
                genreText1.text = "힙합&알앤비";
                genre1Image.sprite = hiphop;
                texture = hiphopTex;
            }
            else if(apiResult.result[2].Equals('5')){           
                genreText1.text = "댄스";
                genre1Image.sprite = dance;
                texture = danceTex;
            }
            else if(apiResult.result[2].Equals('6')){           
                genreText1.text = "POP";
                genre1Image.sprite = pop;
                texture = popTex;
            }
            else{                                               
                genreText1.text = "기타";
                genre1Image.sprite = none;
                texture = noGenreTex;
            }

            //텍스처 결정
            if(apiResult.result[3].Equals('0')){         
                genreText2.text = "락";
                genre2Image.sprite = rock;
            }
            else if(apiResult.result[3].Equals('1')){       
                genreText2.text = "발라드";
                genre2Image.sprite = ballad;
            }
            else if(apiResult.result[3].Equals('2')){         
                genreText2.text = "인디&어쿠스틱";
                genre2Image.sprite = indie;
            }
            else if(apiResult.result[3].Equals('3')){         
                genreText2.text = "트로트";
                genre2Image.sprite = trot;
            }
            else if(apiResult.result[3].Equals('4')){        
                genreText2.text = "힙합&알앤비";
                genre2Image.sprite = hiphop;
            }
            else if(apiResult.result[3].Equals('5')){               
                genreText2.text = "댄스";
                genre2Image.sprite = dance;
            }
            else if(apiResult.result[3].Equals('6')){               
                genreText2.text = "POP";
                genre2Image.sprite = pop;
            }
            else{                                               
                genreText2.text = "기타";
                genre2Image.sprite = none;
            }

            //애니메이터
            Animator animator = newObject.GetComponent<Animator>();
            if(apiResult.result[1].Equals('0'))      //strong happy
                animator.SetTrigger("strong_happy");
            else if(apiResult.result[1].Equals('1'))    //week happy
                animator.SetTrigger("week_happy");
            else if(apiResult.result[1].Equals('2'))    //week sad
                animator.SetTrigger("week_sad");
            else                                        //strong sad
                animator.SetTrigger("strong_sad");

            
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
            renderer.material.mainTexture = texture;
            renderer = newObject.transform.Find("Geometry/group1/pCylinder1").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = newObject.transform.Find("Geometry/group1/pCylinder2").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = newObject.transform.Find("Geometry/group1/pasted__pCube13").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = newObject.transform.Find("Geometry/group1/pasted__pCube15").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;

            GameObject custom;
            custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Rock").gameObject;
            custom.SetActive(false);
            custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Ballad").gameObject;
            custom.SetActive(false);
            custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Indie").gameObject;
            custom.SetActive(false);
            custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Hiphop").gameObject;
            custom.SetActive(false);
            custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Idol").gameObject;
            custom.SetActive(false);
            custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Pop").gameObject;
            custom.SetActive(false);
            custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_None").gameObject;
            custom.SetActive(false);
            
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

            say.SetActive(false);
            sayBack.SetActive(false);

            yield return new WaitForSeconds(4.0f);
            say.SetActive(true);
            sayBack.SetActive(true);

        }
    }
}
