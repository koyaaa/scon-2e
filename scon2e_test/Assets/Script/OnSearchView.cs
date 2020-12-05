using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;


public class OnSearchView : MonoBehaviour
{

    public event System.Action<GameObject> onFound = (obj) => { };
    public event System.Action<GameObject> onLost = (obj) => { };

    [SerializeField, Range(0.0f, 360.0f)]
    private float m_searchAngle = 0.0f;
    private float m_searchCosTheta = 0.0f;

    private SphereCollider m_sphereCollider = null;
    private List<FoundData> m_foundList = new List<FoundData>();

    public Color redColor;
    public Color yellowColor;
    
    //探す時間
    public float itime = 1.0f;

    private float SaveTime;

    public bool WANING=false;

    public int EnemyNumber = 0;//敵キャラの番号
    public GameObject Obj;
    public ObjectManager objectManager;
    public bool HideSearch = false;//主人公が隠れたときにtrueになる。
    public bool HunterFlg = false;
    public bool hypnflg = false;
    public float crowl_speed;
    public float chase_speed;
        
    NavMeshAgent agent;

    public GameObject soundObj;
    public Sound soundManager;

    private bool atmatm = false;


    //　SearchUI表示用
    //[SerializeField]
    private GameObject SearchUI;
    private GameObject UI_key;
    private GameObject UI_uzu;
    //　?マーク表示用
    private Image Deimage;   
    //見付けた場合true
    private bool De = false;
    //鍵マーク
    private Image keyimage;
    public bool keyflg = false;
    //渦
    private Image uzu2;
    public bool uzuflg = false;
    public float uzumetor;

    void Start()
    {

        Obj = GameObject.Find("ObjectManager");
        soundObj = GameObject.Find("Soundmanager");
        objectManager = Obj.GetComponent<ObjectManager>();
        soundManager = soundObj.GetComponent<Sound>();
        agent = GetComponent<NavMeshAgent>();
        SearchUI =  transform.Find("SearechUI/Image").gameObject;
        UI_key = transform.Find("SearechUI/kagi").gameObject;
        Deimage = SearchUI.transform.Find("DeImage").GetComponent<Image>();
        UI_uzu = transform.Find("SearechUI/uzu2").gameObject;
        uzu2 = UI_uzu.transform.Find("uzu").GetComponent<Image>();
        UI_uzu.SetActive(false);
        SearchUI.SetActive(false);
    }

    public float SearchAngle
    {
        get { return m_searchAngle; }
    }

    public float SearchRadius
    {
        get
        {
            if (m_sphereCollider == null)
            {
                m_sphereCollider = GetComponent<SphereCollider>();
            }
            return m_sphereCollider != null ? m_sphereCollider.radius : 0.0f;
        }
    }


    private void Awake()
    {
        m_sphereCollider = GetComponent<SphereCollider>();
        ApplySearchAngle();
    }

    private void OnDisable()
    {
        m_foundList.Clear();
    }

    // シリアライズされた値がインスペクター上で変更されたら呼ばれます。
    private void OnValidate()
    {
        ApplySearchAngle();
    }

    private void ApplySearchAngle()
    {
        float searchRad = m_searchAngle * 0.5f * Mathf.Deg2Rad;
        m_searchCosTheta = Mathf.Cos(searchRad);
    }

    private void Update()
    {
        if (hypnflg == false)
        {
            UpdateFoundObject();
        }

        //見つかっている
        if (De == true)
        {
            SearchUI.SetActive(true);
            UI_key.SetActive(false);
            //Deimage.fillAmount = 0f;
            Deimage.fillAmount = 1.0f  - ((float)(Time.time - SaveTime) / (float)itime);
        }else if(De == false)
        {

            SearchUI.SetActive(false);
            if (this.gameObject.tag == "Enemy_Key" && keyflg == false)
            {
                UI_key.SetActive(true);
            }
            else
            {
                UI_key.SetActive(false);
            }
        }

        if(uzuflg == true)
        {
            UI_uzu.SetActive(true);
            if (this.gameObject.tag == "Enemy")
            {
                uzu2.fillAmount = 1.0f - uzumetor;
            }
            //Debug.Log(uzumetor);
        }
        else
        {
            UI_uzu.SetActive(false);
        }

    }

    private void UpdateFoundObject()
    {
        foreach (var foundData in m_foundList)
        {
            //Debug.Log("主人公発見: ");

            WalkAround d1 = GetComponent<WalkAround>();
            

            GameObject targetObject = foundData.Obj;
            if (targetObject == null)
            {
                
                continue;
            }
            
            bool isFound = CheckFoundObject(targetObject);
            foundData.Update(isFound);

            //主人公が隠れたとき
            if (HideSearch == true && atmatm == false)
            {

                
                //if (foundData.IsLost())
                //{
                agent.speed = crowl_speed;
                Debug.Log("隠れた！");
                /**/
                De = false;
                /**/
                WANING = false;
                onLost(targetObject);
                Debug.Log("主人公いなくなった: ");
                GetComponent<NavMeshAgent>().isStopped = false;
                d1.inArea = false;
                GetComponent<Renderer>().material.color = d1.origColor;
                d1.chaspeed = 0;
                atmatm = true;
                //}
            }
            if (HideSearch == true)
            {
                continue;
            }
            if (atmatm ==true && HideSearch == false)
            {
                continue;
            }

            if (foundData.IsFound() && WANING == false)
            {
                soundManager.WhoSEflag = true;

                onFound(targetObject);
                    Debug.Log("誰: ？");
                /**/
                De = true;
                /**/
                //Debug.Log(" " + SaveTime);
                d1.inArea = true;
                    GetComponent<Renderer>().material.color = yellowColor;
                    GetComponent<NavMeshAgent>().isStopped = true;

                    SaveTime = Time.time;
                    //Debug.Log("更新　" + SaveTime);
                    //WANING = false;

            }

            else if (foundData.IsLost() && WANING == false)
            {

                //WANING == false
                onLost(targetObject);
                Debug.Log("主人公いなくなった: ");
                //Debug.Log(" " + SaveTime);
                GetComponent<NavMeshAgent>().isStopped = false;
                d1.inArea = false;
                GetComponent<Renderer>().material.color = d1.origColor;
                d1.chaspeed = 0;

                /**/
                De = false;
                /**/
            }

            if (d1.inArea == true)
            {

                if (WANING == false&& Time.time > SaveTime + itime)
                {
                    WANING = true;

                    soundManager.SearchSEflag = true;
                    agent.speed = chase_speed;

                    Debug.Log("主人公発見: !");
                    //Debug.Log(" " + SaveTime);
                    onFound(targetObject);
                    d1.inArea = true;
                    GetComponent<Renderer>().material.color = redColor;
                    GetComponent<NavMeshAgent>().isStopped = false;


                    for (int i = 0; i < 20; i++)
                    {
                        if (objectManager.EnemyNumber[i] != -99)
                        {
                            continue;
                        }
                        objectManager.EnemyNumber[i] = EnemyNumber;
                        objectManager.EnemyNumber[i + 1] = -99;
                        break;
                    }
                }

                if(HunterFlg == true)
                {
                    
                }

                if (WANING == true && HunterFlg == true )
                {
                    Hunter hunt = GetComponent<Hunter>();
                    if (hunt.IsLostFlg == true)
                    {


                        agent.speed = crowl_speed;
                        WANING = false;
                        onLost(targetObject);
                        Debug.Log("主人公いなくなった: ");
                        //Debug.Log(" " + SaveTime);
                        GetComponent<NavMeshAgent>().isStopped = false;
                        d1.inArea = false;
                        GetComponent<Renderer>().material.color = d1.origColor;
                        d1.chaspeed = 0;

                        hunt.LostFlg = false;
                        hunt.IsLostFlg = false;
                    }
                }
            }

            

        }
      
    }

    private bool CheckFoundObject(GameObject i_target)
    {
        Vector3 targetPosition = i_target.transform.position;
        Vector3 myPosition = transform.position;

        Vector3 myPositionXZ = Vector3.Scale(myPosition, new Vector3(1.0f, 0.0f, 1.0f));
        Vector3 targetPositionXZ = Vector3.Scale(targetPosition, new Vector3(1.0f, 0.0f, 1.0f));

        Vector3 toTargetFlatDir = (targetPositionXZ - myPositionXZ).normalized;
        Vector3 myForward = transform.forward;
        if (!IsWithinRangeAngle(myForward, toTargetFlatDir, m_searchCosTheta))
        {
            return false;
        }

        Vector3 toTargetDir = (targetPosition - myPosition).normalized;

        if (!IsHitRay(myPosition, toTargetDir, i_target))
        {
            return false;
        }

        return true;
    }

    private bool IsWithinRangeAngle(Vector3 i_forwardDir, Vector3 i_toTargetDir, float i_cosTheta)
    {
        // 方向ベクトルが無い場合は、同位置にあるものだと判断する。
        if (i_toTargetDir.sqrMagnitude <= Mathf.Epsilon)
        {
            return true;
        }

        float dot = Vector3.Dot(i_forwardDir, i_toTargetDir);
        return dot >= i_cosTheta;
    }

    private bool IsHitRay(Vector3 i_fromPosition, Vector3 i_toTargetDir, GameObject i_target)
    {
        // 方向ベクトルが無い場合は、同位置にあるものだと判断する。
        if (i_toTargetDir.sqrMagnitude <= Mathf.Epsilon)
        {
            return true;
        }

        RaycastHit onHitRay;
        if (!Physics.Raycast(i_fromPosition, i_toTargetDir, out onHitRay, SearchRadius))
        {
            return false;
        }

        if (onHitRay.transform.gameObject != i_target)
        {
            return false;
        }

        return true;
    }

    private void OnTriggerEnter(Collider i_other)
    {
        GameObject enterObject = i_other.gameObject;
        
            // 念のため多重登録されないようにする。
            if (m_foundList.Find(value => value.Obj == enterObject) == null)
        {
            m_foundList.Add(new FoundData(enterObject));
        }
    }

    private void OnTriggerExit(Collider i_other)
    {
        GameObject exitObject = i_other.gameObject;
   
        var foundData = m_foundList.Find(value => value.Obj == exitObject);
        if (foundData == null)
        {
            return;
        }

        if (foundData.IsCurrentFound() && WANING == false)
        {
            WalkAround d1 = GetComponent<WalkAround>();

            onLost(foundData.Obj);

            //Debug.Log("主人公いなくなった: ");

            GetComponent<NavMeshAgent>().isStopped = false;
            d1.inArea = false;
            GetComponent<Renderer>().material.color = d1.origColor;
            d1.chaspeed = 0;

            /**/
            De = false;
            /**/
        }

        m_foundList.Remove(foundData);
    }


    private class FoundData
    {
        public FoundData(GameObject i_object)
        {
            m_obj = i_object;
        }

        private GameObject m_obj = null;
        private bool m_isCurrentFound = false;
        private bool m_isPrevFound = false;

        public GameObject Obj
        {
            get { return m_obj; }
        }

        public Vector3 Position
        {
            get { return Obj != null ? Obj.transform.position : Vector3.zero; }
        }

        public void Update(bool i_isFound)
        {
            m_isPrevFound = m_isCurrentFound;
            m_isCurrentFound = i_isFound;
        }

        public bool IsFound()
        {
            return m_isCurrentFound && !m_isPrevFound;
        }

        public bool IsLost()
        {
            return !m_isCurrentFound && m_isPrevFound;
        }

        public bool IsCurrentFound()
        {
            return m_isCurrentFound;
        }
    }

    


} // class SearchingBehavior

