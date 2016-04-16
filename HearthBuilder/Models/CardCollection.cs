﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HearthBuilder.Models
{
    //Singleton Deign
    public sealed class Cards
    {
        private static volatile Cards instance;
        private static object syncRoot = new Object();

        public static Cards Instance 
        {
            get 
            {
                lock (syncRoot) 
                {
                    if (instance == null)
                    {
                        instance = new Cards();
                    }
                }
                return instance;
            }
        }

        public List<Card> AllCards { get; private set; }

        private Cards()
        {
            //import the data from the db, 
            Dictionary<string, HearthDb.Card> dbCards = HearthDb.Cards.Collectible;

            AllCards = new List<Card>();
            foreach (KeyValuePair<string, HearthDb.Card> card in dbCards){
                AllCards.Add(new Card(card.Value));
            }
        }

        public Card getByName(string name)
        {
            foreach (Card card in AllCards)
            {
                if (card.Name == name)
                    return card;
            }
            return null;
        }

        public Card getById(string id)
        {
            foreach (Card card in AllCards)
            {
                if (card.Id == id)
                    return card;
            }
            return null;
        }
        
    }
}