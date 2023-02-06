using System;
using LuckySpin.Models;
namespace LuckySpin.ViewModels
{
	public class LuckListVM
	{
		public IEnumerable<Spin> Spins { get; set; }
		public Player Player { get; set; }

	}
}

