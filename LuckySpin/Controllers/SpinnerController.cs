using System;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;
using LuckySpin.Services;
using System.Numerics;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        //TODO: IMPORTANT: Run the application FIRST and play couple games before making any of these changes!
        //      In every case, use the TODO prompt to use the LuckySpin database in place of the Repository
        //      Check that the behavior of the application is the same at the end

        //TODO: Start here by inject a reference to the LuckySpinContext instead of the Repository

        //private Repository _repository;

        private LuckySpinDbc _dbc;

        /***
         * Controller Constructor //TODO: Use the LuckySpin Database Context instead of the repository
         */
        public SpinnerController(Models.LuckySpinDbc dbc) 
        {
            this._dbc = dbc; 

        }

        /***
         * Index Action - Gather Player info
         **/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexVM info)
        {
            if (!ModelState.IsValid) { return View(); }

            //Create a new Player object
            Player player = new Player
            {
                FirstName = info.FirstName,
                Luck = info.Luck,
                Balance = info.StartingBalance
            };
            //_repository.Player = player; //TODO: remove this line
            this._dbc.Players.Add(player);
            _dbc.SaveChanges();
            //TODO: Use _dbc to add the player to _dbc.Players and save changes to the database, instead of the repository


            return RedirectToAction("Spin", new {id = player.PlayerId}); //TODO: Pass the player Id to Spin, using RedirectToAction("Spin", new {id = player.PlayerId})
        }

        /***
         * Spin Action - Play one Spin
         **/  
         [HttpGet]      
         public IActionResult Spin(long id) //TODO: receive a id of type long as a parameter
        {
            //Player player = _repository.Player; //TODO: remove this line
            //TODO: Get your particular player object using the _dbc.Players.Find(id) method
            var myPlayer = _dbc.Players.Find(id);

            //Intialize a SpinVM with the player object from the data store
            SpinVM spinVM = new SpinVM() {
                PlayerName = myPlayer.FirstName,
                Luck = myPlayer.Luck,
                CurrentBalance = myPlayer.Balance
                
            };

            if (!spinVM.ChargeSpin())
            {
                return RedirectToAction("LuckList", new { id = myPlayer.PlayerId }); //TODO: Pass the player Id to LuckList, like you did for Spin
            }
 
            if (spinVM.Winner) { spinVM.CollectWinnings(); }

            // Update the Player record using value from the ViewModel
            myPlayer.Balance = spinVM.CurrentBalance;
            // TODO: Update the dbContext Players record 
            _dbc.SaveChanges();

            //Create a Spin using the logic from the SpinViewModel
            Spin spin = new Spin() {
                IsWinning = spinVM.Winner,
                Balance = spinVM.CurrentBalance
            };

            //Save the Spin to the data store
            //_repository.AddSpin(spin); // TODO: remove this line
            //TODO: Add the new spin to dbContext and save changes to the database
            this._dbc.Spins.Add(spin);
            _dbc.SaveChanges();
            return View("Spin", spinVM); //Sends the updated spin info to the Spin View
        }

        /***
         * ListSpins Action - Display Spin data
         **/
         [HttpGet]
         public IActionResult LuckList(long id) //TODO: receive a id of type long as a parameter
        {
            //TODO: Create the LuckListVm from the DbContext instead of the _repository
            LuckListVM luckListVM = new LuckListVM
            {
                Player = this._dbc.Players.Find(id),
                Spins = this._dbc.Spins
            };
            return View(luckListVM);
        }

    }
}

