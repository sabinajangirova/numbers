﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Events;

namespace warehouse.Patterns.Command
{
    class AddCommand : ICommand
    {
        private Warehouse W;
        private Product P;
        private long Amount;
        private delegate void AddHandler(Warehouse sender, OnAddCommandArg e);
        private event AddHandler Notify;
        public bool IsCompleted { get; set; }
        public AddCommand(Warehouse w, Product p, long amount)
        {
            W = w;
            P = p;
            Amount = amount;
            IsCompleted = false;
        }
        public void Execute()
        {
            W.Notify += Program.OnAdding;
            Notify?.Invoke(W, new OnAddCommandArg(DateTime.Now, Amount, P, $"Adding product {P.Name} in amount of {Amount} {P.Unit} via AddCommand class at time {DateTime.Now}"));
            try
            {
                W.AddProduct(P, Amount);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            W.Notify -= Program.OnAdding;
        }

        public void Unexecute()
        {
            Notify?.Invoke(W, new OnAddCommandArg(DateTime.Now, Amount, P, $"Removing product {P.Name} in amount of {Amount} {P.Unit} via AddCommand class at time {DateTime.Now}"));
            W.ProductListing[P] -= Amount;
            if(W.ProductListing[P] <= 0)
            {
                W.ProductListing.Remove(P);
            }
        }
    }
}