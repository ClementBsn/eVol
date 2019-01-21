using UnityEngine;
using FYFY;
using UnityEngine.UI;
using System.Collections.Generic;

// Classe inutile 
public class LevelSystem : FSystem {
	/*private Family _levelButtons = FamilyManager.getFamily(
        new AllOfComponents(typeof(LevelButton)));*/
    private Family _levelButtons = FamilyManager.getFamily(
        new AllOfComponents(typeof(Level)), new NoneOfComponents(typeof(ScrollingBG)));

    private int branchLength = 30;
    private int buttonNb = 1; 
    private int childrenLimit = 2;

	public LevelSystem()
    {
        foreach (GameObject go in _levelButtons)
        {
            setLevel(go);

        }

    }


    void addNewChild(GameObject GO){
        
        int childCount = GO.GetComponent<Level>().children.Count;
        // We have a limit of creation of levels possibles (if not tree will be too big)
        if(childCount<childrenLimit){
            buttonNb ++ ;
            //Creating new button
            GameObject newGO = Object.Instantiate<GameObject>(GO);
            GO.GetComponent<Level>().children.Add(buttonNb);
            //Place it left or right.
            newGO.transform.position += new Vector3((float)(branchLength * System.Math.Pow(-1,childCount)), branchLength,0);
            newGO.transform.SetParent(GO.GetComponent<Transform>().parent, false);
            newGO.GetComponent<Level>().number = buttonNb;

            newGO.GetComponent<Level>().children = new List<int>();
            LineRenderer childsLR =newGO.GetComponent<LineRenderer>();
            if (childsLR != null){
                Object.Destroy(childsLR);
            }
            
            setLevel(newGO);
            
            if (childCount < 1){
                LineRenderer lineRenderer = GO.AddComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.SetPosition(1,new Vector3( branchLength,branchLength,0));
                lineRenderer.startWidth=0.1f;
                lineRenderer.endWidth=0.1f;
                lineRenderer.useWorldSpace=false;
            }
            else {
                LineRenderer lineRenderer = GO.GetComponent<LineRenderer>();
                int initialPosCount = lineRenderer.positionCount;
                lineRenderer.positionCount += 2;
                lineRenderer.SetPosition(initialPosCount, new Vector3(0f,0f,0f));
                lineRenderer.SetPosition(initialPosCount + 1,new Vector3((float)((branchLength) * System.Math.Pow(-1,childCount)),branchLength,0));
                
            }
            GameObjectManager.bind (newGO);

            

         }
        
    }

    void displayPanel(GameObject go){

        foreach (GameObject but in _levelButtons)
        {
            but.transform.GetChild(1).gameObject.SetActive(false);
        }

        /*GameObject child2 = go.transform.GetChild(1).gameObject;
        child2.SetActive(!child2.activeSelf);*/
    }

    //Setting text & add listener
    void setLevel(GameObject go){


    	go.GetComponentInChildren<Text>().text = go.GetComponent<Level>().number.ToString();
    	Button bt = go.GetComponent<Button>();
        //bt.onClick.AddListener(delegate { addNewChild(go); });
        bt.onClick.AddListener(delegate { displayPanel(go); });
    }
    

    /*private void HideIfClickedOutside(GameObject panel) {
         if (Input.GetMouseButton(0) && panel.activeSelf && 
             !RectTransformUtility.RectangleContainsScreenPoint(
                 panel.GetComponent<RectTransform>(), 
                 Input.mousePosition, 
                 Camera.main)) 
         {
             panel.SetActive(false);
         }
     }*/


}