using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScreenUpdate : MonoBehaviour
{
    public GameObject[] screens = new GameObject[4];

    public Texture ballad;
    public Texture hiphop;
    public Texture dance;
    public Texture rock;
    public Texture trot;
    public Texture indie;
    public Texture pop;
    public Texture none;
    public Texture black;

    private Texture texture;

    [System.Serializable]
    public class Api
    {
        public string name;
        public string result;
    }

    public void screenUpda(){
        StartCoroutine(screenChange());
    }

    IEnumerator screenChange(){

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

            yield return new WaitForSeconds(0.1f);

            Renderer renderer;
            Material material;

            for(int i=0; i<4; i++){
                renderer = screens[i].GetComponent<Renderer>();
                material = renderer.material;
                material.mainTexture = black;
            }

            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));

            renderer = screens[0].GetComponent<Renderer>();
            material = renderer.material;
            material.mainTexture = texture;

            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));

            renderer = screens[1].GetComponent<Renderer>();
            material = renderer.material;
            material.mainTexture = texture;

            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));

            renderer = screens[2].GetComponent<Renderer>();
            material = renderer.material;
            material.mainTexture = texture;

            yield return new WaitForSeconds(Random.Range(0.3f, 0.95f));

            renderer = screens[3].GetComponent<Renderer>();
            material = renderer.material;
            material.mainTexture = texture;

            // for(int i=0; i<4; i++){
            //     renderer = screens[i].GetComponent<Renderer>();
            //     material = renderer.material;
            //     material.mainTexture = texture;
            // }
        }
    }
}
