using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NPC : MonoBehaviour
{
    public GameObject[] Npc = new GameObject[4];

    public Texture ballad;
    public Texture hiphop;
    public Texture dance;
    public Texture rock;
    public Texture trot;
    public Texture indie;
    public Texture pop;
    public Texture none;

     private Texture texture;

    [System.Serializable]
    public class Api
    {
        public string name;
        public string result;
        public string songName;
        public string text;
    }
    
    public void npcUpdate(){
        StartCoroutine(npcGenreUpdate());
    }

    IEnumerator npcGenreUpdate(){
        UnityWebRequest request = UnityWebRequest.Get("http://digger.works:12023/getLastDB");

        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            string data = request.downloadHandler.text;
            Api apiResult = JsonUtility.FromJson<Api>(data);

            if(apiResult.result[2].Equals('0')) texture = rock;
            else if(apiResult.result[2].Equals('1')) texture = ballad;
            else if(apiResult.result[2].Equals('2')) texture = indie;
            else if(apiResult.result[2].Equals('3')) texture = trot;
            else if(apiResult.result[2].Equals('4')) texture = hiphop;
            else if(apiResult.result[2].Equals('5')) texture = dance;
            else if(apiResult.result[2].Equals('6')) texture = pop;
            else texture = none;

            Renderer renderer;
            for(int i=0; i<4; i++){
                renderer = Npc[i].transform.Find("Geometry/group1/pCube9").GetComponent<Renderer>();
                renderer.material.mainTexture = texture;
                renderer = Npc[i].transform.Find("Geometry/group1/pCylinder1").GetComponent<Renderer>();
                renderer.material.mainTexture = texture;
                renderer = Npc[i].transform.Find("Geometry/group1/pCylinder2").GetComponent<Renderer>();
                renderer.material.mainTexture = texture;
                renderer = Npc[i].transform.Find("Geometry/group1/pasted__pCube13").GetComponent<Renderer>();
                renderer.material.mainTexture = texture;
                renderer = Npc[i].transform.Find("Geometry/group1/pasted__pCube15").GetComponent<Renderer>();
                renderer.material.mainTexture = texture;

                if(i==0 || i==1){
                    GameObject custom;
                    custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Rock").gameObject;
                    custom.SetActive(false);
                    custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Ballad").gameObject;
                    custom.SetActive(false);
                    custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Indie").gameObject;
                    custom.SetActive(false);
                    custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Hiphop").gameObject;
                    custom.SetActive(false);
                    custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Idol").gameObject;
                    custom.SetActive(false);
                    custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Pop").gameObject;
                    custom.SetActive(false);
                    custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_None").gameObject;
                    custom.SetActive(false);
            
            
                    //커스텀 결정
                    if(apiResult.result[3].Equals('0')){
                        custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Rock").gameObject;
                        custom.SetActive(true);
                    }
                    else if(apiResult.result[3].Equals('1')){
                        custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Ballad").gameObject;
                        custom.SetActive(true);
                    }
                    else if(apiResult.result[3].Equals('2')){
                        custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Indie").gameObject;
                        custom.SetActive(true);
                    }
                    else if(apiResult.result[3].Equals('4')){
                        custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Hiphop").gameObject;
                        custom.SetActive(true);
                    }
                    else if(apiResult.result[3].Equals('5')){
                        custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Idol").gameObject;
                        custom.SetActive(true);
                    }
                    else if(apiResult.result[3].Equals('6')){
                        custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_Pop").gameObject;
                        custom.SetActive(true);
                    }
                    else{
                        // int rand = Random.Range(0, 2);
                        // if(rand == 0){
                        //     custom = newObject.transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_None").gameObject;
                        //     custom.SetActive(true);
                        // }
                        custom = Npc[i].transform.Find("DeformationSystem/Root_M/Chest_M/Neck_M/Custom/Custom_None").gameObject;
                        custom.SetActive(true);
                    }
                }
            }
        }
    }
}
