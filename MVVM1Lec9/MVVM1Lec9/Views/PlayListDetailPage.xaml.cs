using MVVM1Lec9.Models;
using MVVM1Lec9.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM1Lec9.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayListDetailPage : ContentPage
	{

        private PlayListViewModel playList;

        public PlayListDetailPage(PlayListViewModel playList)
        {
            this.playList = playList;//Asignamos objeto

            InitializeComponent();

            title.Text = playList.Title;
        }

        public PlayListDetailPage ()
		{
			InitializeComponent ();
		}
	}
}