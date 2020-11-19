using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;


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
    
    public float itime = 1.0f;

    private float SaveTime;

    public bool WANING=false;

    public int EnemyNumber = 0;//敵キャラの番号
    public GameObject Obj;
    public ObjectManager objectManager;
    public bool HideSearch = false;//主人公が隠れたときにtrueになる。
    public bool HunterFlg = false;
    public bool hypnflg = false;

    public GameObject soundObj;
    public Sound soundManager;

    void Start()
    {

        Obj = GameObject.Find("ObjectManager");
        soundObj = GameObject.Find("Soundmanager");
        objectManager = Obj.GetComponent<ObjectManager>();
        soundManager = soundObj.GetComponent<Sound>();
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
           
            if (foundData.IsFound() && WANING == false)
            {
               
                    onFound(targetObject);
                    Debug.Log("誰: ？");
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
            }
            
            if (d1.inArea == true)
            {

                if (WANING == false&& Time.time > SaveTime + itime)
                {
                    WANING = true;

                    soundManager.SearchSEflag = true;

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

            //主人公が隠れたとき
            if (HideSearch == true)
            {
                if (foundData.IsLost())
                {
                    Debug.Log("隠れた！");
                    WANING = false;
                    onLost(targetObject);
                    Debug.Log("主人公いなくなった: ");
                    GetComponent<NavMeshAgent>().isStopped = false;
                    d1.inArea = false;
                    GetComponent<Renderer>().material.color = d1.origColor;
                    d1.chaspeed = 0;
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

