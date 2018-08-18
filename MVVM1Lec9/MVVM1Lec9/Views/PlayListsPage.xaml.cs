using MVVM1Lec9.Models;
using MVVM1Lec9.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM1Lec9.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayListsPage : ContentPage
	{
        //Como la ListView no tiene Command para asignar el evento directamente con DataBinding, tenemos que crear un atributo que referencie el modelo para 
        //poder acceder a el desde la vista mediante DataBinding
        private PlayListsViewModel ViewModel
        {
            //En el Get retornamos el BindingContext para el Databinding y en el Set lo asignamos por el valor que le llega de la vista.
            get { return BindingContext as PlayListsViewModel; }
            set { BindingContext = value; }
        }

		public PlayListsPage ()
		{
            //Fijamos el BindingContext con el ViewModel para habilitar el DataBinding, pasandole un objeto PageService para que tenga acceso a los metodos de Xamarin
            //Mientras dejamos la vista desligada de Xamarin para los Unit Test.
            ViewModel = new PlayListsViewModel(new PageService());
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void OnPlayListSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Utilizamos el BindingContext de ViewModel para llamar al metodo SelectPlayListCommand.Execute y le pasamos el objeto seleccionado.
            ViewModel.SelectPlayListCommand.Execute(e.SelectedItem);
        }
    }
}