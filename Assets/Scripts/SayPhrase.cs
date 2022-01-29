using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SayPhrase : MonoBehaviour
{
    //public GameObject TextOverHead;
    private TextMeshProUGUI TextOverHead_Mesh;

    //Quiche phrase
    private string[] quichePhrase = new string[] 
    {
        "Ah le batard, il me devait 10 balles.",
        "Bon débarras!",
        "C'est qui QUICHE lol ?",
        "On est mieux sans QUICHE hein ?",
        "Mieux vaut lui que moi.",
        "Une QUICHE en mois !",
    };



    // Start is called before the first frame update
    void Start()
    {
        TextOverHead_Mesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        //TextOverHead_Mesh = TextOverHead.GetComponent<TextMeshPro>();
        print(TextOverHead_Mesh);
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


    public string CheckWhoDied(int IDwhoDied)
    {
        string PhraseToSay = "sdojfoijf";

        if (IDwhoDied == 21) //ID de QUICHE
        {
            if (Random.Range(0, 100) <= 20)
                PhraseToSay = quichePhrase[Random.Range(0, quichePhrase.Length - 1)];
            else
                PhraseToSay = "";
        }
        else
        {
            //Relation_Params currentRelation = CheckIfRelation(IDwhoDied);
               /*if (currentRelation.validCheck)
            {
                switch (currentRelation.RelationType)
                {
                    case E_relationType.Amoureux:
                        break;
                    case E_relationType.Business:
                        break;
                    case E_relationType.Deteste:
                        break;
                    case E_relationType.Epoux:
                        break;
                    case E_relationType.FanDe:
                        break;
                    case E_relationType.Jumeaux:
                        break;
                    case E_relationType.Obeis:
                        break;
                    case E_relationType.Pote:
                        break;
                    default:
                        break;
                }
            }*/
        }

        return PhraseToSay;
    }


    /*private Relation_Params CheckIfRelation(int IDtoCheck)
    {
        Relation_Params relation_temp = new Relation_Params { };
        relation_temp.validCheck = false;

        foreach (var relation in Relations)
        {
            foreach (var target in relation.Targets)
            {
                if(target.Character_ID == IDtoCheck)
                {
                    relation_temp.validCheck = true;
                    relation_temp = relation;
                }
            }
        }

        return relation_temp;
    }*/
}
