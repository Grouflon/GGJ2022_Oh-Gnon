using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SayPhrase : MonoBehaviour
{
    //public GameObject TextOverHead;
    public TextMeshProUGUI TextOverHead_Phrase;
    public TextMeshProUGUI TextOverHead_Name;
    public GameObject Bubble;
    private Agent m_Agent;
    public PhraseArrays m_PhraseArrays;
    private bool isShowingPhrase = false;
    //private bool isShowingName = false;

    private float DelayBefore = 0;
    private float typeSpeed = 0.5f;
    private float duration = 3;

    // Start is called before the first frame update
    void Start()
    {
        TextOverHead_Phrase.text = "";
        TextOverHead_Name.text = "";

        m_Agent = gameObject.GetComponent<Agent>();

        m_Agent.OnAgentStateChanged += WhenPickedUp;

        Bubble.SetActive(false);

    }

    private void OnMouseEnter()
    {
        //if (!isShowingName)
        //{
            TextOverHead_Name.enabled = true;
            TextOverHead_Name.text = m_Agent.infos.Name;
        //}
    }

    private void OnMouseExit()
    {

        TextOverHead_Name.enabled = false;
        TextOverHead_Name.text = "";
    }

    public void SayPhraseAtBeginning()
    {
        if (!isShowingPhrase)
        {
            string PhraseToSay = "";

            if (Random.Range(0, 100) <= 50)
                PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhraseBonjour);

            DelayBefore = Random.Range(0.5f, 3.0f);
            typeSpeed = 0.2f;
            duration = 1.5f;
            StartCoroutine("ShowPhrase", PhraseToSay);
        }
    }

    public void WhenPickedUp(Agent _agent, AgentState _previousState, AgentState _currentState)
    {
        if (_currentState == AgentState.DRAGGED)
        {
            SayPhrase[] listOfOtherChara = GameObject.FindObjectsOfType<SayPhrase>();

            foreach (SayPhrase charaPhrase in listOfOtherChara)
            {
                if(charaPhrase != this)
                    charaPhrase.SayPhraseWhenOtherPickedUp();
            }



            string PhraseToSay = "";

            PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhraseWhenGrabbed);

            DelayBefore = 0;
            typeSpeed = 0.2f;
            duration = 1f;
            StartCoroutine("ShowPhrase", PhraseToSay);
        }
    }

    public void SayPhraseWhenOtherPickedUp()
    {
        if (!isShowingPhrase)
        {
            string PhraseToSay = "";

            if (Random.Range(0, 100) <= 20)
                PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhrasePanic);

            DelayBefore = Random.Range(0f, 0.5f);
            typeSpeed = 0.2f;
            duration = 1f;
            StartCoroutine("ShowPhrase", PhraseToSay);
        }
    }


    public void SayPhraseWhenACharaDie(int IDwhoDied)
    {
        //if (!isShowingPhrase)
        //{
        StopCoroutine("ShowPhrase");

        TextOverHead_Phrase.text = "";

        string PhraseToSay = "";

            if (IDwhoDied == 20) //ID de QUICHE
            {
                if (Random.Range(0, 100) <= 30)
                    PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhraseQuiche);
            }
            else
            {
                S_Relation_Params currentRelation = new S_Relation_Params { };

                bool checkrelation = false;

                foreach (var relation in m_Agent.infos.Relations)
                {
                    foreach (int target in relation.ID_Targets)
                    {
                        if (target == IDwhoDied)
                        {
                            currentRelation = relation;
                            checkrelation = true;
                            break;
                        }
                    }
                }

                if (checkrelation)
                {
                    switch (currentRelation.RelationType)
                    {
                        case E_relationType.Amoureux:
                            PhraseToSay = SelectRandomPhraseInArray(currentRelation.Unique_Phrases);
                            break;
                        case E_relationType.Business:
                            PhraseToSay = SelectRandomPhraseInArray(currentRelation.Unique_Phrases);
                            break;
                        case E_relationType.Deteste:
                            PhraseToSay = SelectRandomPhraseInArray(currentRelation.Unique_Phrases);
                            break;
                        case E_relationType.Epoux:
                            PhraseToSay = SelectRandomPhraseInArray(currentRelation.Unique_Phrases);
                            break;
                        case E_relationType.FanDe:
                            PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhraseDualipoire);
                            break;
                        case E_relationType.Jumeaux:
                            PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhraseJumeaux);
                            break;
                        case E_relationType.Obeis:
                            if(IDwhoDied == 16)
                            {
                                if(CheckIfCharaIsDead(17))
                                    PhraseToSay = currentRelation.Unique_Phrases[2];
                                else
                                    PhraseToSay = currentRelation.Unique_Phrases[1];
                            }
                            else if(IDwhoDied == 17)
                            {
                                if (CheckIfCharaIsDead(17))
                                    PhraseToSay = currentRelation.Unique_Phrases[2];
                                else
                                    PhraseToSay = currentRelation.Unique_Phrases[0];
                            }
                            break;
                        case E_relationType.Pote:
                            PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhrasePote);
                            break;
                        default:
                            break;
                    }
                }
            }

            DelayBefore = Random.Range(0f, 0.5f);
            typeSpeed = 0.5f;
            duration = 3f;
            StartCoroutine("ShowPhrase", PhraseToSay);
        //}
    }


    private S_Relation_Params CheckIfRelation(int IDtoCheck)
    {
        S_Relation_Params relation_temp = new S_Relation_Params {};

        foreach (var relation in m_Agent.infos.Relations)
        {
            foreach (int target in relation.ID_Targets)
            {
                if(target == IDtoCheck)
                {
                    relation_temp = relation;
                }
            }
        }

        return relation_temp;
    }

    private bool CheckIfCharaIsDead(int IDtoCheck)
    {
        Agent[] m_agentTemp = GameObject.FindObjectsOfType<Agent>();
        foreach (Agent agent in m_agentTemp)
        {
            if (agent.id == IDtoCheck)
                return false;
        }

        return true;
    }


    private string SelectRandomPhraseInArray(string[] Array)
    {
        return Array[Random.Range(0, Array.Length)];
    }

    IEnumerator ShowPhrase(string text)
    {
        char[] Text_Array = text.ToCharArray();
        string TempString = "";

        isShowingPhrase = true;

        yield return new WaitForSeconds(DelayBefore);

        
        Bubble.SetActive(text != "");
        TextOverHead_Phrase.text = "";
        //TextOverHead_Mesh.enabled = true;

        foreach (char letter in Text_Array)
        {
            TempString += letter;
            TextOverHead_Phrase.text = TempString;
            yield return new WaitForSeconds(typeSpeed/ Text_Array.Length);
        }


        yield return new WaitForSeconds(duration);


        TextOverHead_Phrase.text = "";
        Bubble.SetActive(false);

        isShowingPhrase = false;
    }
}
