using System;
using System.Collections.Generic;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Services
{
    public class Repository
    {
        private List<Spin> spins = new List<Spin>();

        //Properties
        public Player Player { get; set; }


        public IEnumerable<Spin> Spins { // Read Only Property

            get { return spins; }
        }

        //Interaction method
        public void AddSpin(Spin s)
        {
            spins.Add(s);
        }
        
    }
}
