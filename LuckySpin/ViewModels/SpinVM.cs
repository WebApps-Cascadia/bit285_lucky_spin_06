using System;
using System.Linq;
namespace LuckySpin.ViewModels
{
    public class SpinVM
    {
        
        /*
         * Instance variables and constants
         */
        private const decimal costForOnePlay = 0.50m;
        private const decimal winningSpinValue = 1.00m;
        private System.Random random = new System.Random();

        //Instance variables used for computations
        private decimal _balance;
        private int[] _numbers;
        private int _luck;

        /*
         * Constructor - initializes the VM
         */
        public SpinVM()
        {
            _numbers = new int[] { random.Next(1, 10), random.Next(1, 10), random.Next(1, 10) };
        }

        /*
         * Simple Properties - used only to shuttle data, no instance variable
         */
        public string PlayerName { get; set; }


        // More complex Properties used in VM logic need an instance varialble backing
        public int Luck
        {
            get { return _luck; }
            set { _luck = value; }
        }
        public decimal CurrentBalance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        //Read-Only Properties
        public int[] Numbers
        {
            get { return _numbers; }
        }
        public bool Winner
        {
            get { return _numbers.Contains(_luck); }
        }


        /*
         * Game Play Methods 
         */
        public bool ChargeSpin() //returns true if the CurrentBalance is enough to play
        {
            if (_balance >= costForOnePlay)
            {
                _balance -= costForOnePlay;
                return true;
            }
            return false;
        }
        public void CollectWinnings() //adds the winner payout to the balance
        {
            _balance += winningSpinValue;
        }
        
    }
}
