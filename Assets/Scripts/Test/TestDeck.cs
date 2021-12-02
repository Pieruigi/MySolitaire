using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Logic;

namespace Zoca.Test
{

    class TestChild
    {
        
        public static TestChild GetInstance()
        {
            return new TestChild();
        }

        public virtual void Test()
        {
            Debug.Log("TestChild:" + this.GetType());
        }
    }

    class TestParent: TestChild
    {
        public new static TestParent GetInstance()
        {
            return new TestParent();
        }

        public override void Test()
        {
            Debug.Log("TestParent:" + this.GetType());
        }
        public  void Test2()
        {
            Debug.Log("TestParent-2:" + this.GetType());
        }
    }

    public class TestDeck : MonoBehaviour
    {
        CardPile deck;

        private void Awake()
        {
            deck = ItalianCardUtility.CreateDeck();
            //deck = FrenchCardUtility.CreateDeck(1, true);

            // Shuffle
            deck.Shuffle();

            // Log
            while (!deck.IsEmpty())
            {
                Card card = deck.PopCard();
                
                Debug.Log(card);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.LogFormat("[GameLogic type:{0}]", GameLogic.Instance.GetType());
            TestChild.GetInstance().Test();
            TestParent.GetInstance().Test();
            TestParent.GetInstance().Test2();
        }

        // Update is called once per frame
        void Update()
        {

        }

       


    }

}
