using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;

public struct sResult
{
    string result;
}

public struct Signup
{
    public string username;
    public string password;
} 
public struct UserData
{
    public string username;
    public string password;
}

public class Manager : MonoBehaviour {

    public Text bt;
    public static Manager man;
    int chk = 0;
    public List<GameObject> listb;
    

	void Awake ()
    {
        if(man == null)
        {
            man = this;
        }
        if(man != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        Addsome();
             
    }

    public void Addsome()
    {
        listb = new List<GameObject>();

        for(int i = 1; i < 5; i++)
        {
            string si = i.ToString();//"1"
            GameObject bt = GameObject.Find(si);//"1"

            listb.Add(bt);
        }
    }
        
    private void Update()
    {
        if(chk <= 0)
        {
            return;
        }
        else
        {
            listb[0].transform.Translate(0, -chk*Time.deltaTime, 0);
        }
        
    }

    

    public void asign()
    {
        if(chk <= 0)
        {
            chk+= 10;
            Text tt = listb[0].GetComponentInChildren<Text>();
            tt.text = "";

            listb[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); 
            listb[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); 
            listb[1].SetActive(false);

        }
        else {
            chk++;
        }
    }
    public string un;
    public string pw;

    public void click()
    {
        string username = listb[2].GetComponent<InputField>().text;
        un = listb[2].GetComponent<InputField>().text;
        string password = listb[3].GetComponent<InputField>().text;
        pw = listb[3].GetComponent<InputField>().text;

        if(chk < 20)
        {

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return;
            }
            else
            {
                Signup signupData = new Signup();
                signupData.username = un;
                signupData.password = pw;
                chk += 20;
                
                StartCoroutine(uppass(signupData));

            }
        }
        else
        {
            
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return;
            }
            else
            {
                UserData loginData = new UserData();
                loginData.username = username;
                loginData.password = password;
                StartCoroutine(inpass(loginData));
            }
        }
    }


    IEnumerator uppass(Signup from)
    {
        string postData = JsonUtility.ToJson(from);
        byte[] sendData = Encoding.UTF8.GetBytes(postData);
        using(UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/users/Add", sendData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.Send();
            
            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                setlogin();
            }
        }



    }
    string setwww = "http://localhost:3000/users/signin";
    string loginwww = "http://localhost:3000/users/signin";
    string nickwww = "http://localhost:3000/users/Nickname";
    string emptywww = "";


    IEnumerator inpass(UserData logindata)
    {
        string postData = JsonUtility.ToJson(logindata);
        byte[] sendData = Encoding.UTF8.GetBytes(postData);
        using(UnityWebRequest www = UnityWebRequest.Put(setwww, sendData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.Send();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string resultst = www.downloadHandler.text;
                var reuslt = JsonUtility.FromJson<sResult>(resultst);
                Debug.Log(resultst);
                setwww = nickwww;
                SceneManager.LoadScene("2");
                click();
                chk = 30;
            }
        }
    }
    

    public void setlogin()
    {
        Text tt = listb[0].GetComponentInChildren<Text>();
        tt.text = "로그인하세요";
        bt.text = "로그인하세요";
    }
}
