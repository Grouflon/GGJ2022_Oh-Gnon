using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SayPhrase : MonoBehaviour
{
    //public GameObject TextOverHead;
    private TextMeshProUGUI TextOverHead_Mesh;
    private Agent m_Agent;
    public PhraseArrays m_PhraseArrays;

    // Start is called before the first frame update
    void Start()
    {
        TextOverHead_Mesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        m_Agent = gameObject.GetComponent<Agent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        //TextOverHead_Mesh.text = CharacterName;
        TextOverHead_Mesh.enabled = true;
    }

    private void OnMouseExit()
    {
        TextOverHead_Mesh.text = "";
        TextOverHead_Mesh.enabled = false;
    }

    public void SayPhraseAtBeginning()
    {
        string PhraseToSay = "sdojfoijf";

        if (Random.Range(0, 100) <= 20)
            PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhraseQuiche);
        else
            PhraseToSay = "";


    }

    public void SayPhraseWhenACharaDie(int IDwhoDied)
    {
        string PhraseToSay = "sdojfoijf";

        if (IDwhoDied == 21) //ID de QUICHE
        {
            if (Random.Range(0, 100) <= 20)
                PhraseToSay = SelectRandomPhraseInArray(m_PhraseArrays.PhraseQuiche);
            else
                PhraseToSay = "";
        }
        else
        {
            S_Relation_Params currentRelation = CheckIfRelation(IDwhoDied);
            if (currentRelation.validCheck)
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


    }


    private S_Relation_Params CheckIfRelation(int IDtoCheck)
    {
        S_Relation_Params relation_temp = new S_Relation_Params { };
        relation_temp.validCheck = false;

        foreach (var relation in m_Agent.infos.Relations)
        {
            foreach (int target in relation.ID_Targets)
            {
                if(target == IDtoCheck)
                {
                    relation_temp.validCheck = true;
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
        return Array[Random.Range(0, Array.Length - 1)];
    }


}
