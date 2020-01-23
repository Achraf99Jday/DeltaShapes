using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{


    public float initY = 0.5f;
    public float initX = 2.5f;
    public Sprite RedSprite;
    public Sprite BlueSprite;
    public Sprite GreenSprite;
    public Sprite GreySprite;
    Vector3 touchPosWorld;
    public int Sizeoflist;
    public bool same;
    public Text scores, comment;
    public float score = 0f;
    public enum triangleType { normal, iso, rect };
    public triangleType cc;
    public List<Sprite> sprites = new List<Sprite>();
    public Sprite SelectedSprite;
    public List<GameObject> selected = new List<GameObject>();
    public List<Sprite> selectedSprites = new List<Sprite>();
    int[,] grrid = new int[6, 6];





    //Use this for initialization
    void Start()
    {




        sprites.Add(RedSprite);
        sprites.Add(GreenSprite);
        sprites.Add(BlueSprite);
        sprites.Add(GreySprite);




        for (int i = 0; i < 6; i++)
        {

            for (int j = 0; j < 6; j++)
            {


                grrid[i, j] = Random.Range(1, 5);
                Debug.Log(grrid[i, j]);

            }

        }


        for (int i = 0; i < 6; i++)
        {


            for (int j = 0; j < 6; j++)
            {

                GameObject tmp = new GameObject();
                tmp.transform.position = new Vector2(initX, initY);
                tmp.AddComponent<SpriteRenderer>().sprite = sprites[grrid[i, j] - 1];
                tmp.AddComponent<BoxCollider2D>();

                initX = initX + 1;

            }
            initX = initX - 6;
            initY = initY - 1;
        }


    }

    // Update is called once per frame
    void Update()
    {

        switch (cc)
        {
            case triangleType.normal: normale(); break;
            case triangleType.iso: isocele(); break;
            case triangleType.rect: rectangle(); break;
        }

    }

    void normale()
    {




        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.mousePosition)), Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Touched it" + hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite.name);
                comment.text = "";




                if (selected.Count == 0)
                {
                    selected.Add(hit.transform.gameObject);
                    selectedSprites.Add(hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite);
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = SelectedSprite;

                }
                else
                {

                    addCircle(hit);



                }



            }









            if (selected.Count == 3)


            {

                if (selectedSprites[0] == selectedSprites[1] && selectedSprites[0] == selectedSprites[2])
                {
                    //Debug.Log("same");
                    if (selected[0].transform.position.y == selected[1].transform.position.y && selected[2].transform.position.y == selected[1].transform.position.y)
                    {
                        selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                        selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                        selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];
                        Debug.Log("same column");
                        selected.Clear();
                        selectedSprites.Clear();
                        comment.text = "Same Line";

                    }
                    else if (selected[0].transform.position.x == selected[1].transform.position.x && selected[2].transform.position.x == selected[1].transform.position.x)
                    {
                        selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                        selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                        selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];
                        Debug.Log("same line");
                        selected.Clear();
                        selectedSprites.Clear();
                        comment.text = "Same Column";

                    }
                    else
                    {



                        score = score + Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y))) + Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y))) + Mathf.Sqrt(((selected[0].transform.position.x - selected[2].transform.position.x) * (selected[0].transform.position.x - selected[2].transform.position.x)) + ((selected[2].transform.position.y - selected[1].transform.position.y) * (selected[2].transform.position.y - selected[1].transform.position.y)));
                        double scoree = Mathf.Round(score);
                        Debug.Log(scoree);

                        string haya = ((double)scoree).ToString();
                        scores.text = haya;



                        Vector3 pos1 = selected[0].transform.position;
                        Vector3 pos2 = selected[1].transform.position;
                        Vector3 pos3 = selected[2].transform.position;

                        GameObject tmp;


                        Destroy(selected[0]);
                        Destroy(selectedSprites[0]);
                        tmp = new GameObject();
                        tmp.transform.position = pos1;
                        tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                        tmp.AddComponent<BoxCollider2D>();

                        Destroy(selected[1]);
                        Destroy(selectedSprites[1]);
                        tmp = new GameObject();
                        tmp.transform.position = pos2;
                        tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                        tmp.AddComponent<BoxCollider2D>();

                        Destroy(selected[2]);
                        Destroy(selectedSprites[2]);
                        tmp = new GameObject();
                        tmp.transform.position = pos3;
                        tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                        tmp.AddComponent<BoxCollider2D>();
                        selected.Clear();
                        selectedSprites.Clear();
                    }


                }


                else
                {
                    selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                    selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                    selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];

                    Debug.Log("nope");
                    selected.Clear();
                    selectedSprites.Clear();
                    comment.text = "Different colors";
                }


            }

        }


    }

    public void addCircle(RaycastHit2D hit)
    {
        for (int i = 0; i <= selected.Count - 1; i++)
        {
            if (selected[i].transform.position == hit.transform.gameObject.transform.position)
            {

                Debug.Log("yar7em bouk selectioni haja o5ra");

                return;


            }




        }

        selected.Add(hit.transform.gameObject);
        selectedSprites.Add(hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite);
        hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = SelectedSprite;


    }


    void isocele()
    {


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.mousePosition)), Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Touched it" + hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite.name);
                comment.text = "";




                if (selected.Count == 0)
                {
                    selected.Add(hit.transform.gameObject);
                    selectedSprites.Add(hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite);
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = SelectedSprite;

                }
                else
                {

                    addCircle(hit);



                }



            }









            if (selected.Count == 3)


            {

                if (selectedSprites[0] == selectedSprites[1] && selectedSprites[0] == selectedSprites[2])
                {
                    //Debug.Log("same");
                    if (selected[0].transform.position.y == selected[1].transform.position.y && selected[2].transform.position.y == selected[1].transform.position.y)
                    {
                        selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                        selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                        selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];
                        Debug.Log("same column");
                        selected.Clear();
                        selectedSprites.Clear();
                        comment.text = "Same Line";

                    }
                    else if (selected[0].transform.position.x == selected[1].transform.position.x && selected[2].transform.position.x == selected[1].transform.position.x)
                    {
                        selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                        selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                        selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];
                        Debug.Log("same line");
                        selected.Clear();
                        selectedSprites.Clear();
                        comment.text = "Same Column";

                    }
                    else
                    {
                        float dist1 = Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y)));
                        float dist2 = Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y)));
                        float dist3 = Mathf.Sqrt(((selected[0].transform.position.x - selected[2].transform.position.x) * (selected[0].transform.position.x - selected[2].transform.position.x)) + ((selected[2].transform.position.y - selected[1].transform.position.y) * (selected[2].transform.position.y - selected[1].transform.position.y)));

                        if (((int)(dist1 * 100)) == ((int)(dist2 * 100)) || ((int)(dist2 * 100)) == ((int)(dist3 * 100)) || ((int)(dist3 * 100)) == ((int)(dist1 * 100)))
                        {


                            score = score + Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y))) + Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y))) + Mathf.Sqrt(((selected[0].transform.position.x - selected[2].transform.position.x) * (selected[0].transform.position.x - selected[2].transform.position.x)) + ((selected[2].transform.position.y - selected[1].transform.position.y) * (selected[2].transform.position.y - selected[1].transform.position.y)));
                            double scoree = Mathf.Round(score);
                            Debug.Log(scoree);

                            string haya = ((double)scoree).ToString();
                            scores.text = haya;



                            Vector3 pos1 = selected[0].transform.position;
                            Vector3 pos2 = selected[1].transform.position;
                            Vector3 pos3 = selected[2].transform.position;

                            GameObject tmp;


                            Destroy(selected[0]);
                            Destroy(selectedSprites[0]);
                            tmp = new GameObject();
                            tmp.transform.position = pos1;
                            tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                            tmp.AddComponent<BoxCollider2D>();

                            Destroy(selected[1]);
                            Destroy(selectedSprites[1]);
                            tmp = new GameObject();
                            tmp.transform.position = pos2;
                            tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                            tmp.AddComponent<BoxCollider2D>();

                            Destroy(selected[2]);
                            Destroy(selectedSprites[2]);
                            tmp = new GameObject();
                            tmp.transform.position = pos3;
                            tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                            tmp.AddComponent<BoxCollider2D>();
                            selected.Clear();
                            selectedSprites.Clear();
                        }
                        else {
                            selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                            selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                            selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];

                            Debug.Log("nope");
                            selected.Clear();
                            selectedSprites.Clear();
                            comment.text = "Not iso";
                        }
                    }
                }


                else
                {
                    selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                    selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                    selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];

                    Debug.Log("nope");
                    selected.Clear();
                    selectedSprites.Clear();
                    comment.text = "Different colors";
                }


            }

        }

    }

    void rectangle()
    {





        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.mousePosition)), Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Touched it" + hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite.name);
                comment.text = "";




                if (selected.Count == 0)
                {
                    selected.Add(hit.transform.gameObject);
                    selectedSprites.Add(hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite);
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = SelectedSprite;

                }
                else
                {

                    addCircle(hit);



                }



            }









            if (selected.Count == 3)


            {

                if (selectedSprites[0] == selectedSprites[1] && selectedSprites[0] == selectedSprites[2])
                {
                    //Debug.Log("same");
                    if (selected[0].transform.position.y == selected[1].transform.position.y && selected[2].transform.position.y == selected[1].transform.position.y)
                    {
                        selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                        selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                        selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];
                        Debug.Log("same column");
                        selected.Clear();
                        selectedSprites.Clear();
                        comment.text = "Same Line";

                    }
                    else if (selected[0].transform.position.x == selected[1].transform.position.x && selected[2].transform.position.x == selected[1].transform.position.x)
                    {
                        selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                        selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                        selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];
                        Debug.Log("same line");
                        selected.Clear();
                        selectedSprites.Clear();
                        comment.text = "Same Column";

                    }
                    else
                    {
                        float dist1 = Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y)));
                        float dist2 = Mathf.Sqrt(((selected[2].transform.position.x - selected[1].transform.position.x) * (selected[2].transform.position.x - selected[1].transform.position.x)) + ((selected[2].transform.position.y - selected[1].transform.position.y) * (selected[2].transform.position.y - selected[1].transform.position.y)));
                        float dist3 = Mathf.Sqrt(((selected[0].transform.position.x - selected[2].transform.position.x) * (selected[0].transform.position.x - selected[2].transform.position.x)) + ((selected[0].transform.position.y - selected[2].transform.position.y) * (selected[0].transform.position.y - selected[2].transform.position.y)));

                        Debug.Log(dist1);
                        Debug.Log(dist2);
                        Debug.Log(dist3);
                        //Debug.Log(dist2 * dist2 + dist1 * dist1);
                        //Debug.Log(dist1 * dist1 + dist3*dist3);
                        //Debug.Log(dist2 * dist2 + dist3*dist3);


                        if ((((int)(((dist1 * dist1) + (dist3 * dist3)) * 100)) == (int)((dist2 * dist2) * 100)) || ((int)(((dist2 * dist2) + (dist3 * dist3)) * 100) == (int)((dist1 * dist1) * 100)) || ((int)(((dist1 * dist1) + (dist2 * dist2)) * 100) == (int)((dist3 * dist3) * 100)))
                        {



                            score = score + Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y))) + Mathf.Sqrt(((selected[0].transform.position.x - selected[1].transform.position.x) * (selected[0].transform.position.x - selected[1].transform.position.x)) + ((selected[0].transform.position.y - selected[1].transform.position.y) * (selected[0].transform.position.y - selected[1].transform.position.y))) + Mathf.Sqrt(((selected[0].transform.position.x - selected[2].transform.position.x) * (selected[0].transform.position.x - selected[2].transform.position.x)) + ((selected[2].transform.position.y - selected[1].transform.position.y) * (selected[2].transform.position.y - selected[1].transform.position.y)));
                            double scoree = Mathf.Round(score);
                            Debug.Log(scoree);

                            string haya = ((double)scoree).ToString();
                            scores.text = haya;



                            Vector3 pos1 = selected[0].transform.position;
                            Vector3 pos2 = selected[1].transform.position;
                            Vector3 pos3 = selected[2].transform.position;

                            GameObject tmp;


                            Destroy(selected[0]);
                            Destroy(selectedSprites[0]);
                            tmp = new GameObject();
                            tmp.transform.position = pos1;
                            tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                            tmp.AddComponent<BoxCollider2D>();

                            Destroy(selected[1]);
                            Destroy(selectedSprites[1]);
                            tmp = new GameObject();
                            tmp.transform.position = pos2;
                            tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                            tmp.AddComponent<BoxCollider2D>();

                            Destroy(selected[2]);
                            Destroy(selectedSprites[2]);
                            tmp = new GameObject();
                            tmp.transform.position = pos3;
                            tmp.AddComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

                            tmp.AddComponent<BoxCollider2D>();
                            selected.Clear();
                            selectedSprites.Clear();
                        }
                    
                    else {
                        selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                        selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                        selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];

                        Debug.Log("nope");
                        selected.Clear();
                        selectedSprites.Clear();
                        comment.text = "Not rect";
                    }
}
                }


                else
                {
                    selected[0].GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
                    selected[1].GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                    selected[2].GetComponent<SpriteRenderer>().sprite = selectedSprites[2];

                    Debug.Log("nope");
                    selected.Clear();
                    selectedSprites.Clear();
                    comment.text = "Different colors";
                }


            }

        }
    }
}

